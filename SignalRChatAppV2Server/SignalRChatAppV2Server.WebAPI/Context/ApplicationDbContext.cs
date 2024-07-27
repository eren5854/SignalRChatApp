using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Context;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Chat> Chats { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();

        builder.Entity<AppUser>().HasQueryFilter(filter => !filter.IsDeleted);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }


}

