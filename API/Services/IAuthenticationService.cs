using API.Entities;
using API.Models;
namespace API.Services;

public interface IAuthenticationService
{
    public IEnumerable<Account> Get();
    public Task<Account> CreateAsync(AccountCreate account);
    public Task<AuthenticationResponse> SignIn(AccountLogin login);
}