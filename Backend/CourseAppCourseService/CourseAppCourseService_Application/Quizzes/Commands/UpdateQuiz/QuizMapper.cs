using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;

public class QuizMapper: IMapWith<Quiz>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateQuizCommand, Quiz>()
            .ForMember(quiz => quiz.Id, opt => opt.MapFrom(command => command.Id))
            .ForMember(quiz => quiz.Question, opt => opt.MapFrom(command => command.Question))
            .ForMember(quiz => quiz.Answer, opt => opt.MapFrom(command => command.Answer))
            .ForMember(quiz => quiz.Options, opt => opt.MapFrom(command => command.Options));
    }
}