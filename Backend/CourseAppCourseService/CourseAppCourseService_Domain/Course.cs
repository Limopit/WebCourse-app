namespace CourseAppCourseService_Domain;

public class Course
{
    public Guid Id { get; set; }
    public string CourseTitle {get; set;}
    public string CourseDescription {get; set;}
    public string CourseLogo {get; set;}
    public string CourseLevel {get; set;}
    public string CourseCategory {get; set;}
    public int CourseVolume {get; set;}
    public string CourseCreator {get; set;}
    public float CourseRating {get; set;}
    public string CoursePopularity {get; set;}
    public string CourseLanguage {get; set;}
    public DateTime CreationDate {get; set;}
    public DateTime UpdateDate {get; set;}
    public List<Lesson> Lessons {get; set;} = new();
    public List<Quiz> Quizzes {get; set;} = new();
    public string CourseRequierments {get; set;}
}