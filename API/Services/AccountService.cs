using System.Text;
using API.Data;
using API.Models;

namespace API.Services;

using System.Collections.Generic;
using BCrypt.Net;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _dbContext;
    public AccountService(ApplicationDbContext dbContext)
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
        var username = entity.Username;

        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public IEnumerable<Account> Get()
    {
        var accounts = _dbContext.Accounts.OrderBy(a => a.Id);
        return accounts.ToList();
    }

    public bool SignIn(AccountLogin login)
    {
        var user = _dbContext.Accounts.FirstOrDefault(x => x.Email == login.Email);
        if (user == null) throw new Exception("Usuário não existe");

        return BCrypt.Verify(login.Password, user.PasswordHash);

    }
}