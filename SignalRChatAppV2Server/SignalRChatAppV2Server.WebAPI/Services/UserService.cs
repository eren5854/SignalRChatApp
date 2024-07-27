using AutoMapper;
using ED.Result;
using GenericFileService.Files;
using Microsoft.AspNetCore.Identity;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Services;

public sealed class UserService(
    UserManager<AppUser> userManager,
    IMapper mapper) : IUserService
{
    public async Task<Result<string>> ChangeUserStatus(UserStatusDto request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        user.Status = "offline";

        await userManager.UpdateAsync(user);

        return Result<string>.Succeed("Status is change");
    }

    public async Task<Result<string>> CreateUser(CreateUserDto request, CancellationToken cancellationToken)
    {
        var isEmailExists = await userManager.FindByEmailAsync(request.Email);
        if (isEmailExists is not null)
        {
            return Result<string>.Failure("Email already exists");
        }

        string profilePicture = "";
        var response = request.ProfilePicture;
        if (response is null)
        {
            profilePicture = "";
        }
        else
        {
            profilePicture = FileService.FileSaveToServer(request.ProfilePicture, "wwwroot/ProfilePictures/");
        }

        AppUser user = mapper.Map<AppUser>(request);
        user.ProfilePicture = profilePicture;
        user.CreatedBy = request.UserName;
        user.CreatedDate = DateTime.Now;

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Record could not be created.");
        }

        return Result<string>.Succeed("User create successful");
    }

    public async Task<Result<AppUser?>> GetUserById(GetUserByIdDto request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());
        return Result<AppUser?>.Succeed(user);
    }

    public async Task<Result<string>> UpdateUser(UpdateUserDto request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        mapper.Map(request, user);
        user.UpdatedBy = user.Email;
        user.UpdatedDate = DateTime.Now;
        
        await userManager.UpdateAsync(user);
        return Result<string>.Succeed("User update successful");
    }
}
