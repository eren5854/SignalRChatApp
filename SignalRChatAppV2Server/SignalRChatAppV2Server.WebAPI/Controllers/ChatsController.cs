using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRChatAppV2Server.WebAPI.Abstractions;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Services;

namespace SignalRChatAppV2Server.WebAPI.Controllers;
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class ChatsController(
    IChatService chatService) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> GetChats(GetChatDto request, CancellationToken cancellationToken)
    {
        var result = await chatService.GetChats(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromForm]SendMessageDto request, CancellationToken cancellationToken)
    {
        var result = await chatService.SendMessage(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var result = await chatService.GetAllUsers(cancellationToken);
        return Ok(result);
    }
}
