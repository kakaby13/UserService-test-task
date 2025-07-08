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
        
        CreateMap<UpdateUserDto, User>()
            .ForMember(c => c.UserRole, opt => opt.Ignore())
            .ForMember(c => c.PasswordHash, opt => opt.Ignore())
            .ForMember(c => c.Id, opt => opt.Ignore());

        CreateMap<User, UserDto>()
            .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
            .ForMember(u => u.Name, opt => opt.MapFrom(u => u.Name))
            .ForMember(u => u.Email, opt => opt.MapFrom(u => u.Email))
            .ForMember(u => u.Role, opt => opt.MapFrom(u => u.UserRole.Name));
    }
}