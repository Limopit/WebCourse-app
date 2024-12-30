namespace CourseAppUserService_Application.Common.Exceptions;

public class RoleAssignmentException: Exception
{
    public RoleAssignmentException(string name, string message)
        : base($"Role \"{name}\" {message}"){}
}