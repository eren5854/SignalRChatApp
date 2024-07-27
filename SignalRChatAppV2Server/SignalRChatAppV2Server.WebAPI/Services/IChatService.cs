using ED.Result;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Services;

public interface IChatService
{
    Task<List<Chat?>> GetChats(GetChatDto request, CancellationToken cancellationToken);
    Task<Chat> SendMessage(SendMessageDto request, CancellationToken cancellationToken);
    Task<List<AppUser>> GetAllUsers(CancellationToken cancellationToken);
}
