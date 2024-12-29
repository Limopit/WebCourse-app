namespace CourseAppUserService_Domain.Entities;

public class UserTakenCourses
{
    public Guid RecordId { get; set; }
    public string UserId { get; set; }
    public string CourseId { get; set; }
    public string Status { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateFinished { get; set; }
    public bool IsFavourite { get; set; }
    
    public User User { get; set; }
}