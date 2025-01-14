using UserServiceRpc;

namespace CourseAppCourseService_Infrastructure.Services.UserService;

public class GrpcUserServiceClient(UserServiceRpc.UserService.UserServiceClient grpcClient)
{
    public CreateRecordResponse CreateUserCreatedCourseRecord(string email, string courseId)
    {
        return grpcClient.CreateUserCreatedCourseRecord(new CreateCourseRequest
        {
            Email = email,
            CourseId = courseId
        });
    }

    public DeleteCourseRecordResponse DeleteUserCourseRecord(string courseId)
    {
        return grpcClient.DeleteCourseRecords(new DeleteCourseRecordRequest()
        {
            CourseId = courseId
        });
    }
}