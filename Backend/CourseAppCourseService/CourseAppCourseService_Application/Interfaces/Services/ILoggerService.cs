namespace CourseAppCourseService_Application.Interfaces.Services;

public interface ILoggerService
{
    void Information(string message);
    void Error(string message);
}