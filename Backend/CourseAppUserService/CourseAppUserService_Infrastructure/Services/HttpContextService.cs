using System.Security.Claims;
using CourseAppUserService_Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CourseAppUserService_Persistance.Services;

public class HttpContextService(IHttpContextAccessor httpContextAccessor) : IHttpContextService
{
    public async Task<string?> GetCurrentUserEmailAsync()
    {
        var result = await Task.Run(() => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email));
        return result;
    }
}