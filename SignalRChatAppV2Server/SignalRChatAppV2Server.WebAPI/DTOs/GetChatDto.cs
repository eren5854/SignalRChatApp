namespace SignalRChatAppV2Server.WebAPI.DTOs;

public sealed record GetChatDto(
    Guid UserId,
    Guid ToUserId);
