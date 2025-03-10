using Hangfire.Dashboard;

namespace CourseAppNotificationService_Infrastructure.Hangfire;

public class HangfireDashboardAuthorization: IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}