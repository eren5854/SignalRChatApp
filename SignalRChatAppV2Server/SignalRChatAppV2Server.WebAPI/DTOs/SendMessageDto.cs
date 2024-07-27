namespace SignalRChatAppV2Server.WebAPI.DTOs;

public sealed record SendMessageDto(
    Guid UserId,
    Guid ToUserId,
    string? Message,
    IFormFile? Image);
