using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCourseCreator;

public record GetUserCourseCreatorQuery: IRequest<string>
{
    public string CourseId { get; init; }
}