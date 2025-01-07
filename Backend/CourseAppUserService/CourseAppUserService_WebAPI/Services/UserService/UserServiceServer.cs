using System.Security.Claims;
using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;
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
            var httpContext = context.GetHttpContext();
            var email = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
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

        public override async Task<DeleteCourseRecordResponse> DeleteCourseRecords(DeleteCourseRecordRequest request, ServerCallContext context)
        {
            var deleteCreatedCourseCommand = new DeleteUserCreatedCourseCommand
            {
                CourseId = request.CourseId
            };
            var deleteUserTakenCoursesCommand = new DeleteUserTakenCourseCommand
            {
                CourseId = request.CourseId
            };
            
            await mediator.Send(deleteCreatedCourseCommand);
            await mediator.Send(deleteUserTakenCoursesCommand);

            return new DeleteCourseRecordResponse()
            {
                Result = true
            };
        }
    }
}
