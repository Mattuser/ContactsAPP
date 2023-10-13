using API.Models;
namespace API.Services;

public interface IAccountService
{
    public IEnumerable<Account> Get();
    public Task<Account> CreateAsync(AccountCreate account);
    public bool SignIn(AccountLogin login);
}