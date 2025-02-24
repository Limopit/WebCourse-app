using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CourseAppNotificationService_Infrastructure.Hubs;

[Authorize]
public class NotificationHub: Hub;