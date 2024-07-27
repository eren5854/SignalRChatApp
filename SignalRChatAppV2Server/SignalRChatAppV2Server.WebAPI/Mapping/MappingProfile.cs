using AutoMapper;
using SignalRChatAppV2Server.WebAPI.DTOs;
using SignalRChatAppV2Server.WebAPI.Models;

namespace SignalRChatAppV2Server.WebAPI.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, AppUser>();
        CreateMap<UpdateUserDto, AppUser>();
        CreateMap<SendMessageDto, Chat>();
    }
}
