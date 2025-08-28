using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Entities;
using UserService.JwtFeatures;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly JwtHandler _jwtHandler;

    public AccountsController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] UserForRegistrationDto userForRegistration)
    {
        if (userForRegistration is null)
        {
            return BadRequest();
        }

        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
            return BadRequest(new RegistrationResponseDto { Errors = errors });
        }

        return StatusCode(201);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] UserForAuthenticationDto userForAuthentication)
    {
        var user = await _userManager.FindByNameAsync(userForAuthentication.Email!);
        if (user is null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password!))
        {
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
        }

        var token = _jwtHandler.CreateToken(user);

        return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
    }
}