namespace CourseAppUserService_Application.Common.Exceptions;

public class InvalidPasswordException: Exception
{
    public InvalidPasswordException(): base("Invalid password."){}
}