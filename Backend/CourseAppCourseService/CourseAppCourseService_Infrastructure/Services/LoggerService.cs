using CourseAppCourseService_Application.Interfaces.Services;
using Serilog;
using Serilog.Context;

namespace CourseAppCourseService_Infrastructure.Services;

public class LoggerService(ILogger logger): ILoggerService
{
    public void Information(string message)
    {
        using (LogContext.PushProperty("IsManual", true))
        {
            logger.Information(message);
        }
    }

    public void Error(string message)
    {
        using (LogContext.PushProperty("IsManual", true))
        {
            logger.Error(message);
        }
    }
}