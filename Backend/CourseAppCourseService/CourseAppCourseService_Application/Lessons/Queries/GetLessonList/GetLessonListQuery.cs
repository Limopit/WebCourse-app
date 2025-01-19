using MediatR;

namespace CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

public record GetLessonListQuery: IRequest<LessonVm>;