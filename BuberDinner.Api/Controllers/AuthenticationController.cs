using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using FluentResults;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        Result<AuthenticationResult> registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        if (registerResult.IsSuccess)
        {
            return Ok(MapAuthResult(registerResult.Value));
        }

        foreach (var error in registerResult.Errors)
        {
            if (error is DuplicateEmailError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already exists.");
            }
        }
        
        return Problem();
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName,
                                                  authResult.User.LastName, authResult.User.Email, authResult.Token);
        return Ok(response);
    }
}