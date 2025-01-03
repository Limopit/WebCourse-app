using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
}