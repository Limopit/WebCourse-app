using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;

public class GetQuizListQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<GetQuizListQuery, QuizVm>
{
    
    public async Task<QuizVm> Handle(GetQuizListQuery request, CancellationToken cancellationToken)
    {
        var quizzes = await unitOfWork.Quizzes.GetAllEntitiesAsync(cancellationToken);
        
        return await mapper.MapAsync<List<Quiz>, QuizVm>(quizzes);
    }
}