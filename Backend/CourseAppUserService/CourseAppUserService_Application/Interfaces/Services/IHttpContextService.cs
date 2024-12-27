namespace CourseAppUserService_Application.Interfaces.Services;

public interface IHttpContextService
{
    Task<string?> GetCurrentUserEmail();
}