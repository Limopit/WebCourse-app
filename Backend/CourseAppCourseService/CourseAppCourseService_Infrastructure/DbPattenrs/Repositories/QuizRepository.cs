using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public class QuizRepository(ICourseDbContext context)
: BaseRepository<Quiz>(context, "Quizzes"), IQuizRepository;