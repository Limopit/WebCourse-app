using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class QuizMapper: IMapWith<Quiz>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateQuizCommand, Quiz>()
            .ForMember(quiz =>  quiz.QuizQuestion, opt => opt.MapFrom(command => command.QuizQuestion))
            .ForMember(quiz => quiz.QuizOptions, opt => opt.MapFrom(command => command.QuizOptions))
            .ForMember(quiz => quiz.QuizAnswer, opt => opt.MapFrom(command => command.QuizAnswer));
    }
}