using FluentValidation;
using FluentValidation.Results;

namespace CourseAppCourseService_Application.Common.Exceptions;

public class CourseValidationException: ValidationException
{
    public List<string> ErrorList { get; set; }

    public CourseValidationException(IEnumerable<ValidationFailure> failures)
        : base("Validation failed")
    {
        ErrorList = new List<string>();
        foreach (var failure in failures)
        {
            ErrorList.Add($"Property {failure.PropertyName} failed validation. Error {failure.ErrorMessage}");
        }
    }
}