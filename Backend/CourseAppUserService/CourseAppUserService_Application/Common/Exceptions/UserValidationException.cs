using FluentValidation;
using FluentValidation.Results;

namespace CourseAppUserService_Application.Common.Exceptions;

public class UserValidationException: ValidationException
{
    public List<string> ErrorList { get; set; }

    public UserValidationException(IEnumerable<ValidationFailure> failures)
        : base("Validation failed")
    {
        ErrorList = new List<string>();
        foreach (var failure in failures)
        {
            ErrorList.Add($"Property {failure.PropertyName} failed validation. Error {failure.ErrorMessage}");
        }
    }
}