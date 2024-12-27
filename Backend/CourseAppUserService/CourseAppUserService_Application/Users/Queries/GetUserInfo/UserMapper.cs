using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public class UserMapper: IMapWith<User>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(user => user.Id, opt => opt.MapFrom(u => u.Id))
            .ForMember(user => user.FirstName, opt => opt.MapFrom(u => u.FirstName))
            .ForMember(user => user.LastName, opt => opt.MapFrom(u => u.LastName))
            .ForMember(user => user.Email, opt => opt.MapFrom(u => u.Email));
    }
}