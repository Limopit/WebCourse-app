namespace CourseAppUserService_Domain.Entities;

public class UserTakenCourses
{
    public Guid RecordId { get; set; }
    public required string UserId { get; set; }
    public required string CourseId { get; set; }
    public required string Status { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateFinished { get; set; }
    public bool IsFavourite { get; set; }
    
    public User? User { get; set; }
}