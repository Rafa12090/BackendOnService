using AutoMapper;
using BackEnd_SocialE.Security.Domain.Models;
using BackEnd_SocialE.Security.Domain.Services.Communication;
using BackEnd_SocialE.Security.Resources;
using BackEnd_SocialE.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_SocialE.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController: ControllerBase {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UsersController(IUserService userService, IMapper mapper) {
        _userService = userService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model) {
        var response = await _userService.Authenticate(model);
        return Ok(response);
    }
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest model) {
        await _userService.RegisterAsync(model);
        return Ok(new { message = "Registration successful" });
    }
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var users = await _userService.ListAsync();
        var resources =
            _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserResource>(user);
        return Ok(resource);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest model)
    {
        await _userService.UpdateAsync(id, model);
        return Ok(new { message = "User updated successfully" });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }
}