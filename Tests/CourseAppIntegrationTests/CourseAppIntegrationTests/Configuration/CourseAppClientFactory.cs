namespace CourseAppIntegrationTests.Configuration;

public class CourseAppClientFactory: IDisposable
{
    private readonly HttpClient _userServiceClient = new() { BaseAddress = new Uri("https://localhost:5002") };
    private readonly HttpClient _courseServiceClient = new() { BaseAddress = new Uri("https://localhost:5000") };

    public HttpClient GetUserServiceClient() => _userServiceClient;
    public HttpClient GetCourseServiceClient() => _courseServiceClient;

    public void Dispose()
    {
        _userServiceClient.Dispose();
        _courseServiceClient.Dispose();
    }
}