namespace CourseAppUserService_Tests.Tests;

public abstract class BaseTest
{
    protected readonly UserServiceDbContextFactory Context = new();
}