namespace CourseAppUserService_Domain.Entities;

public class UserCreatedCourses
{
    public Guid RecordId { get; set; }
    public required string UserId { get; set; }
    public required string CourseId { get; set; }
    public required string ApprovementStatus { get; set; }
    public DateTime? ApprovementDate { get; set; }
    
    public User? User { get; set; }
}