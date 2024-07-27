namespace SignalRChatAppV2Server.WebAPI.DTOs;

public sealed record LoginDto(
    string EmailOrUserName,
    string Password);
