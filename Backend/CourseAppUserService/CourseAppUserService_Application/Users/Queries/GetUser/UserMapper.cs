using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Users.Queries.GetUser;

public class UserMapper: IMapWith<User>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(dto => dto.Id, config => config.MapFrom(user => user.Id))
            .ForMember(dto => dto.FirstName, config => config.MapFrom(user => user.FirstName))
            .ForMember(dto => dto.LastName, config => config.MapFrom(user => user.LastName))
            .ForMember(dto => dto.Email, config => config.MapFrom(user => user.Email));
    }
}