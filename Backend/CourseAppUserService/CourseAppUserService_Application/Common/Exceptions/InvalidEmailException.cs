namespace CourseAppUserService_Application.Common.Exceptions;

public class InvalidEmailException: Exception
{
    public InvalidEmailException(): base("Invalid email."){}
}