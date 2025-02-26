using System.Security.Claims;
using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetApprovedCourseList;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetPendingCourseList;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteEachUserTakenCourse;
using CourseAppUserService_Domain.Enums;
using Grpc.Core;
using MediatR;
using UserServiceRpc;

namespace CourseAppUserService.Services.UserService
{
    public class UserService(IMediator mediator) : UserServiceRpc.UserService.UserServiceBase
    {
        public override async Task<CreateRecordResponse> CreateUserCreatedCourseRecord(CreateCourseRequest request,
            ServerCallContext context)
        {
            var command = new CreateUserCreatedCourseCommand
            {
                Email = request.Email,
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
                Id = request.CourseId
            };
            var deleteUserTakenCoursesCommand = new DeleteEachUserTakenCourseCommand
            {
                Id = request.CourseId
            };
            
            await mediator.Send(deleteCreatedCourseCommand);
            await mediator.Send(deleteUserTakenCoursesCommand);

            return new DeleteCourseRecordResponse()
            {
                Result = true
            };
        }

        public override async Task<GetApprovedCourseListResponse> GetApprovedCourseList(EmptyMessage request, ServerCallContext context)
        {
            var query = new GetApprovedCourseListQuery();
            
            var result = await mediator.Send(query);

            return new GetApprovedCourseListResponse()
            {
                CourseList = { result }
            };
        }
        
        public override async Task<GetPendingCourseListResponse> GetPendingCourseList(EmptyMessage request, ServerCallContext context)
        {
            var query = new GetPendingCourseListQuery();
            
            var result = await mediator.Send(query);

            return new GetPendingCourseListResponse()
            {
                CourseList = { result }
            };
        }
    }
}
