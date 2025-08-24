using AutoMapper;
using IdentityUserRegistration.DTO;
using IdentityUserRegistration.Entities;

namespace IdentityUserRegistration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map Email from registration DTO to UserName in User entity
        CreateMap<UserForRegistrationDto, User>()
            .ForMember(u => u.UserName, o => o.MapFrom(x => x.Email));
    }
}