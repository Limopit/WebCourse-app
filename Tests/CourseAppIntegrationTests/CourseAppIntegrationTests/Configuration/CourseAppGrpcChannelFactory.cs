using Grpc.Net.Client;

namespace CourseAppIntegrationTests;

public class CourseAppGrpcChannelFactory: IDisposable
{
    private readonly GrpcChannel _userServiceChannel = GrpcChannel.ForAddress("https://localhost:5002");
    private readonly GrpcChannel _courseServiceChannel = GrpcChannel.ForAddress("https://localhost:5000");


    public GrpcChannel GetUserServiceChannel() => _userServiceChannel;
    public GrpcChannel GetCourseServiceChannel() => _courseServiceChannel;

    public void Dispose()
    {
        _userServiceChannel.Dispose();
        _courseServiceChannel.Dispose();
    }
}