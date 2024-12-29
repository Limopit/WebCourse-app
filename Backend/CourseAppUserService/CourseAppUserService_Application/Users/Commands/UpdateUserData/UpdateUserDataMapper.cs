using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserData;

public class UpdateUserDataMapper: IMapWith<User>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDataCommand, User>()
            .ForMember(user => user.FirstName, config => config.MapFrom(command => command.FirstName))
            .ForMember(user => user.LastName, config => config.MapFrom(command => command.LastName))
            .ForMember(user => user.Email, config => config.MapFrom(command => command.Email))
            .ForMember(user => user.UserName, config => config.MapFrom(command => command.Username));
    }
}