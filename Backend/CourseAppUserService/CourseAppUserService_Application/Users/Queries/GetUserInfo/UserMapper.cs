using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public class UserMapper: IMapWith<User>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(user => user.Id, config => config.MapFrom(u => u.Id))
            .ForMember(user => user.FirstName, config => config.MapFrom(u => u.FirstName))
            .ForMember(user => user.LastName, config => config.MapFrom(u => u.LastName))
            .ForMember(user => user.Email, config => config.MapFrom(u => u.Email));
    }
}