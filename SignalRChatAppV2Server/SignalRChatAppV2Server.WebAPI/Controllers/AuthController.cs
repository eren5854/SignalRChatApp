using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChatAppV2Server.WebAPI.Abstractions;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Services;

namespace SignalRChatAppV2Server.WebAPI.Controllers;
public sealed class AuthController(
    IAuthService authService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
