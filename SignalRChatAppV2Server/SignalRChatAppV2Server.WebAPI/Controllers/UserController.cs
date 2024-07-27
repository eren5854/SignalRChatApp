using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRChatAppV2Server.WebAPI.Abstractions;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Services;

namespace SignalRChatAppV2Server.WebAPI.Controllers;
//[Authorize(AuthenticationSchemes = "Bearer")]
//[AllowAnonymous]
public sealed class UserController(
    IUserService userService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm]CreateUserDto request, CancellationToken cancellationToken)
    {
        var result = await userService.CreateUser(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Update(UpdateUserDto request, CancellationToken cancellationToken)
    {
        var result = await userService.UpdateUser(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetById(GetUserByIdDto request, CancellationToken cancellationToken)
    {
        var result = await userService.GetUserById(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ChangeUserStatus(UserStatusDto request, CancellationToken cancellationToken)
    {
        var result = await userService.ChangeUserStatus(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}