using FluentValidation;
using FluentValidation.Results;

namespace CourseAppUserService_Application.Common.Exceptions;

public class UserValidationException: ValidationException
{
    public List<string> errors { get; set; }

    public UserValidationException(IEnumerable<ValidationFailure> failures)
        : base("Validation failed")
    {
        errors = new List<string>();
        foreach (var failure in failures)
        {
            errors.Add($"Property {failure.PropertyName} failed validation. Error {failure.ErrorMessage}");
        }
    }
}