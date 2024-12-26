namespace CourseAppUserService_Domain;

public class UserCreatedCourses
{
    public Guid RecordId { get; set; }
    public string UserId { get; set; }
    public string CourseId { get; set; }
    public string ApprovementStatus { get; set; }
    public DateTime? ApprovementDate { get; set; }
    
    public User User { get; set; }
}