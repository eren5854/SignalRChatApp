using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Repositories;

public interface IChatRepository
{
    IQueryable<Chat?> GetChat(Guid userId, Guid toUserId, CancellationToken cancellationToken);
}
