using ED.Result;
using SignalRChatAppV2Server.WebAPI.DTOs;

namespace SignalRChatAppV2Server.WebAPI.Services;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginDto request, CancellationToken cancellationToken);
}
