using AutoMapper;
using IdentityUserRegistration.DTO;
using IdentityUserRegistration.Entities;

namespace IdentityUserRegistration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // When mapping from the User for registration object to the User object
        // I want to populate the UserName property inside the User class
        // from Email property that resides inside the User for registration class
        CreateMap<UserForRegistrationDto, User>()
            .ForMember(u => u.UserName, o => o.MapFrom(x => x.Email));
    }
}