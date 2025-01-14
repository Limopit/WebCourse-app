namespace CourseAppCourseService_Domain;

public class Quiz
{
    public Guid Id { get; set; }
    public string Question {get; set;}
    public List<string> Options {get; set;}
    public string Answer {get; set;}
}