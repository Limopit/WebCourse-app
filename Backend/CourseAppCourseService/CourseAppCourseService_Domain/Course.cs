namespace CourseAppCourseService_Domain;

public class Course
{
    public Guid Id { get; set; }
    public string Title {get; set;}
    public string Description {get; set;}
    public string Logo {get; set;}
    public string Level {get; set;}
    public string Category {get; set;}
    public int Volume {get; set;}
    public string Creator {get; set;}
    public float Rating {get; set;}
    public string Popularity {get; set;}
    public string Language {get; set;}
    public DateTime CreationDate {get; set;}
    public DateTime UpdateDate {get; set;}
    public List<Lesson> Lessons {get; set;} = new();
    public List<Quiz> Quizzes {get; set;} = new();
    public string Requierments {get; set;}
}