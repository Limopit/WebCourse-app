using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserData;

public class UpdateUserDataMapper: IMapWith<User>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDataCommand, User>()
            .ForMember(user => user.FirstName, opt => opt.MapFrom(command => command.FirstName))
            .ForMember(user => user.LastName, opt => opt.MapFrom(command => command.LastName))
            .ForMember(user => user.Email, opt => opt.MapFrom(command => command.Email))
            .ForMember(user => user.UserName, opt => opt.MapFrom(command => command.Username));
    }
}