using System.Net;
using System.Text.Json;
using CourseAppUserService_Application.Common.Exceptions;

namespace CourseAppUserService.Middleware;

public class CustomExceptionHandler(RequestDelegate request)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await request(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case UserValidationException userValidationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    errors = userValidationException.ErrorList
                });
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case RoleAssignmentException:
                code = HttpStatusCode.BadRequest;
                break;
            case InvalidEmailException:
                code = HttpStatusCode.BadRequest;
                break;
            case InvalidPasswordException:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        await context.Response.WriteAsync(result);
    }
}