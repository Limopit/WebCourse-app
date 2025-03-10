using Bogus;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Tests.Fakers;

public class FakerContext
{
    public Faker<Course> CourseFaker => new Faker<Course>()
        .RuleFor(c => c.Id, f => Guid.NewGuid())
        .RuleFor(c => c.Title, f => f.Commerce.ProductName())
        .RuleFor(c => c.Description, f => f.Lorem.Sentence());
    
    
    public Faker<Lesson> LessonFaker => new Faker<Lesson>()
        .RuleFor(c => c.Id, f => Guid.NewGuid())
        .RuleFor(c => c.Title, f => f.Commerce.ProductName())
        .RuleFor(c => c.Description, f => f.Lorem.Sentence())
        .RuleFor(c => c.Duration, f => f.Random.Int(1, 100));
    
    
    public Faker<Quiz> QuizFaker => new Faker<Quiz>()
        .RuleFor(c => c.Id, f => Guid.NewGuid())
        .RuleFor(c => c.Question, f => f.Lorem.Sentence())
        .RuleFor(c => c.Answer, f => f.Lorem.Word())
        .RuleFor(c => c.Options, f => f.Lorem.Words(f.Random.Int(1, 5)).ToList());
    
}