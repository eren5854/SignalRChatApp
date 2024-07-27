using Microsoft.AspNetCore.Mvc;

namespace SignalRChatAppV2Server.WebAPI.Abstractions;
[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
}
