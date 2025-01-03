using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public class GetUserInfoCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    : IRequestHandler<GetUserInfoCommand, UserDto> 
{

    public async Task<UserDto> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        return await mapper.MapAsync<User, UserDto>(user);
    }
}