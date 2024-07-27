namespace SignalRChatAppV2Server.WebAPI.DTOs;

public sealed record CreateUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    IFormFile ProfilePicture);
