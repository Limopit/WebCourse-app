using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;

public record GetQuizListQuery: IRequest<QuizVm>;