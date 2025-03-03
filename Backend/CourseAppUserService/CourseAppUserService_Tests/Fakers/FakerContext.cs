using Bogus;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Domain.Enums;

namespace CourseAppUserService_Tests.Fakers;

public class FakerContext
{
    public Faker<User> Users => new Faker<User>()
        .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
        .RuleFor(u => u.Email, f => f.Internet.Email());

    public Faker<UserTakenCourses> UserTakenCourses => new Faker<UserTakenCourses>()
        .RuleFor(c => c.RecordId, f => Guid.NewGuid())
        .RuleFor(c => c.UserId, (f, c) => c.UserId)
        .RuleFor(c => c.CourseId, f => Guid.NewGuid().ToString())
        .RuleFor(c => c.Status, f => CompletionStatus.InProgress.ToString())
        .RuleFor(c => c.DateStart, f => DateTime.UtcNow);

    public Faker<UserTakenCourseDto> UserTakenCourseDtos => new Faker<UserTakenCourseDto>()
        .RuleFor(dto => dto.Id, (f, dto) => dto.Id)
        .RuleFor(dto => dto.Status, f => CompletionStatus.InProgress.ToString())
        .RuleFor(dto => dto.StartDate, f => DateTime.UtcNow);
}