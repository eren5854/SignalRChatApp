using ED.Result;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Services;

public interface IUserService
{
    Task<Result<string>> CreateUser(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateUser(UpdateUserDto request, CancellationToken cancellationToken);
    Task<Result<AppUser?>> GetUserById(GetUserByIdDto request, CancellationToken cancellationToken);
    Task<Result<string>> ChangeUserStatus(UserStatusDto request, CancellationToken cancellationToken);
}
