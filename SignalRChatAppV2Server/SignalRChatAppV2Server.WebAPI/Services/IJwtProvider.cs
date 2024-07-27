using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Services;

public interface IJwtProvider
{
    Task<LoginResponseDto> CreateToken(AppUser user);

}
