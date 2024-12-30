using CourseAppCourseService_Application.Interfaces.Repositories;

namespace CourseAppCourseService_Application.Interfaces;

public interface IUnitOfWork
{
    IDisposable Session { get; }
    void AddOperation(Action operation);
    void CleanOperations();
    Task CommitChangesAsync();
}