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
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (e)
        {
            case UserValidationException сve:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    Errors = сve.errors
                });
                break;
            case NotFoundException nfe:
                code = HttpStatusCode.NotFound;
                break;
            case AlreadyExistsException aee:
                code = HttpStatusCode.BadRequest;
                break;
            case RoleAssignmentException rae:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { err = e.Message });
        }

        await context.Response.WriteAsync(result);
    }
}