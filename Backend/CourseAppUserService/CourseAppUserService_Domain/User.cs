using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Domain;

public class User: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<UserCreatedCourses> CreatedCourses { get; set; }
    public ICollection<UserTakenCourses> TakenCourses { get; set; }
}