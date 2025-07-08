using TestTask.Models.Dto;
using TestTask.Models.Entities;

namespace TestTask.MappingProfiles;

public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<AddUserDto, User>()
            .ForMember(c => c.UserRole, opt => opt.Ignore())
            .ForMember(c => c.PasswordHash, opt => opt.Ignore())
            .ForMember(c => c.Id, opt => opt.Ignore());
    }
}