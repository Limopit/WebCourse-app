namespace CourseAppCourseService_Domain;

public class Quiz
{
    public Guid Id { get; set; }
    public string QuizQuestion {get; set;}
    public List<string> QuizOptions {get; set;}
    public string QuizAnswer {get; set;}
}