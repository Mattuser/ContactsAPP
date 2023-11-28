using API.Data;
using API.Models;
using API.Entities;

namespace API.Services;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _dbContext;
    public AuthenticationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Account> CreateAsync(AccountCreate account)
    {
        string passwordHash =  BCrypt.HashPassword(account.Password);
        var user =  await _dbContext.Accounts.AddAsync(new Account{
            Name = account.Name,
            Email = account.Email,
            Username = account.Username,
            PasswordHash = passwordHash,

        });

        var entity = user.Entity;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public IEnumerable<Account> Get()
    {
        var accounts = _dbContext.Accounts.OrderBy(a => a.Id);
        return accounts.ToList();
    }

    public async Task<AuthenticationResponse> SignIn(AccountLogin login)
    {
        var user = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Email == login.Email);
        if (user == null) return null;

        return Authenticate(user, login.Password);
    }


    private AuthenticationResponse Authenticate(Account account, string passwordHash)
    {
        var passwordIsValid = BCrypt.Verify(passwordHash, account.PasswordHash);
        
        if(passwordIsValid)
        {
            return new AuthenticationResponse()
            {
                UserName = account.Username,
                Email = account.Email
            };
        }

        return new AuthenticationResponse()
        {
            UserName = string.Empty,
            Email = string.Empty
        };
    }
}