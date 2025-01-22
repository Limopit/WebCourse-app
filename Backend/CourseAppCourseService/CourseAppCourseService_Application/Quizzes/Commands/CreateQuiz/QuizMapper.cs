using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class QuizMapper: IMapWith<Quiz>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateQuizCommand, Quiz>()
            .ForMember(quiz =>  quiz.Question, opt => opt.MapFrom(command => command.Question))
            .ForMember(quiz => quiz.Options, opt => opt.MapFrom(command => command.Options))
            .ForMember(quiz => quiz.Answer, opt => opt.MapFrom(command => command.Answer));
    }
}