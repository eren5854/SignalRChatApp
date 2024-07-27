using AutoMapper;
using GenericFileService.Files;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChatAppV2Server.WebAPI.Context;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Hubs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Services;

public sealed class ChatService(
    ApplicationDbContext context,
    IHubContext<ChatHub> hubContext,
    IMapper mapper) : IChatService
{
    public async Task<List<AppUser>> GetAllUsers(CancellationToken cancellationToken)
    {
        List<AppUser> user = await context.Users.OrderBy(p => p.UserName).ToListAsync(cancellationToken);
        return user;
    }

    public async Task<List<Chat?>> GetChats(GetChatDto request, CancellationToken cancellationToken)
    {
        List<Chat> chat = await context
                                .Chats
                                .Where(p => p.UserId == request.UserId &&
                                            p.ToUserId == request.ToUserId ||
                                            p.UserId == request.ToUserId &&
                                            p.ToUserId == request.UserId)
                                .OrderBy(p => p.Date)
                                .ToListAsync(cancellationToken);
        return chat;
    }

    public async Task<Chat> SendMessage(SendMessageDto request, CancellationToken cancellationToken)
    {
        string image = "";
        var response = request.Image;
        if (response is null)
        {
            image = "";
        }
        else
        {
            image = FileService.FileSaveToServer(request.Image, "wwwroot/ImageMessages/");
        }

        Chat chat = mapper.Map<Chat>(request);
        chat.Image = image;
        chat.Date = DateTime.Now;

        await context.AddAsync(chat, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        //string connectionId = ChatHub.Users.FirstOrDefault(p => p.Value == chat.ToUserId).Key;
        //await hubContext.Clients.Client(connectionId).SendAsync("Messages", chat);

        await hubContext.Clients.All.SendAsync("Messages", chat);
        return chat;

    }
}
