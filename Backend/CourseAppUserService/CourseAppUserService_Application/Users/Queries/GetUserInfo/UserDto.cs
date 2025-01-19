namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public record UserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}