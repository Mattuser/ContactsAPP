using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result =  _accountService.Get();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]AccountCreate account)
    {
       var result = await _accountService.CreateAsync(account);
        return Created("", result);
    
    }

    [HttpPost("/login")]
    public IActionResult SignIn([FromBody]AccountLogin account)
    {
       var result = _accountService.SignIn(account);
       return Ok(result);
    
    }

}