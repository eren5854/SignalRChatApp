using ED.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Services;

public sealed class AuthService(
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider) : IAuthService
{
    public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto request, CancellationToken cancellationToken)
    {
        AppUser? user =
           await userManager
           .Users
           .FirstOrDefaultAsync(p => p.Email == request.EmailOrUserName ||
                                p.UserName == request.EmailOrUserName);
        if (user is null)
        {
            return (500, "User not found");
        }

        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!checkPassword)
        {
            return (500, "Password is incorrect");
        }

        user.Status = "online";

        var loginResponse = await jwtProvider.CreateToken(user);

        return loginResponse;
    }
}
