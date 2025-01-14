using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;

public class QuizMapper: IMapWith<Quiz>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IList<Quiz>, QuizVm>().ForMember(quizVm => quizVm.Quizzes, opt => opt.MapFrom(quiz => quiz));
    }
}