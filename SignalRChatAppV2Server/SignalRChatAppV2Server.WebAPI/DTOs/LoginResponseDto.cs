namespace SignalRChatAppV2Server.WebAPI.DTOs;

public sealed record LoginResponseDto(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires);
