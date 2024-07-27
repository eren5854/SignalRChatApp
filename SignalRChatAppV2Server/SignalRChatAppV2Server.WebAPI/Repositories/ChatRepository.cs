using SignalRChatAppV2Server.WebAPI.Context;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;
using System;

namespace SignalRChatAppV2Server.WebAPI.Repositories;

public sealed class ChatRepository(
    ApplicationDbContext context) : IChatRepository
{
    public IQueryable<Chat?> GetChat(Guid userId, Guid toUserId, CancellationToken cancellationToken)
    {
        return context
            .Chats
            .Where(p => p.UserId == userId && 
            p.ToUserId == toUserId || 
            p.UserId == toUserId && 
            p.ToUserId == userId);
    }
}
