using Microsoft.AspNetCore.Identity;

namespace SignalRChatAppV2Server.WebAPI.Models;

public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string? ProfilePicture {  get; set; }
    public string Status { get; set; } = string.Empty;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }

    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

