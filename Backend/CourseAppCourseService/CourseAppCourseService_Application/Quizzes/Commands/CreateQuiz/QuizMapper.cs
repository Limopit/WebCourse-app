using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class QuizMapper: IMapWith<Quiz>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateQuizCommand, Quiz>()
            .ForMember(dest => dest.QuizQuestion, opt => opt.MapFrom(command => command.QuizQuestion))
            .ForMember(dest => dest.QuizOptions, opt => opt.MapFrom(command => command.QuizOptions))
            .ForMember(dest => dest.QuizAnswer, opt => opt.MapFrom(command => command.QuizAnswer));
    }
}