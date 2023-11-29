using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Services;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AccountsController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _authenticationService.Get();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SignUpAsync([FromBody] AccountCreate account)
    {
        var result = await _authenticationService.CreateAsync(account);
        return Created("", result);

    }

    [HttpPost("/login")]
    public async Task<IActionResult> SignInAsync([FromBody] AccountLogin account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _authenticationService.SignIn(account);
            if (response == null)
                return NotFound(new { Message = "Not Found" });

            if (string.IsNullOrEmpty(response.UserName) && string.IsNullOrEmpty(response.Email))
                return BadRequest("You have entered an invalid username or password");

            return Ok(response);
        }
        catch
        {
            return StatusCode(500, new { message = "Internal Server Error" });
        }
    }

}