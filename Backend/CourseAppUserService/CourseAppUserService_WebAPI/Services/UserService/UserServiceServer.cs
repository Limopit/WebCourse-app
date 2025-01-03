using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;
using CourseAppUserService_Domain.Enums;
using Grpc.Core;
using MediatR;
using UserServiceRpc;

namespace CourseAppUserService.Services.UserService
{
    public class UserService(IMediator mediator, IHttpContextService contextService) : UserServiceRpc.UserService.UserServiceBase
    {
        public override async Task<CreateRecordResponse> CreateUserCreatedCourseRecord(CreateCourseRequest request,
            ServerCallContext context)
        {
            var command = new CreateUserCreatedCourseCommand
            {
                Email = "admin@gmail.com", //await contextService.GetCurrentUserEmailAsync(),
                CourseId = request.CourseId,
                ApprovementStatus = ApprovementStatus.Pending,
            };

            var recordId = await mediator.Send(command);

            return new CreateRecordResponse
            {
                RecordId = recordId.ToString()
            };
        }
    }
}
