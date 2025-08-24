using AutoMapper;
using IdentityUserRegistration.DTO;
using IdentityUserRegistration.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserRegistration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AccountsController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
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
}