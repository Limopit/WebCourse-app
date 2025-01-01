using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;

public class QuizVm
{
    public IList<Quiz> Quizzes { get; set; }
}