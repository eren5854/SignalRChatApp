using DefaultCorsPolicyNugetPackage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SignalRChatAppV2Server.WebAPI.Context;
using SignalRChatAppV2Server.WebAPI.Hubs;
using SignalRChatAppV2Server.WebAPI.Mapping;
using SignalRChatAppV2Server.WebAPI.Middlewares;
using SignalRChatAppV2Server.WebAPI.Models;
using SignalRChatAppV2Server.WebAPI.Options;
using SignalRChatAppV2Server.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureOptions<JwtTokenSetupConfiguration>();
builder.Services.AddAuthentication()
    .AddJwtBearer();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ExtensionsMiddleware.CreateFirstUser(app);

app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat-hub");

app.Run();
