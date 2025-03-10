using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Services;

public interface IRabbitMqService
{
    Task PublishAsync(Notification notification);
}