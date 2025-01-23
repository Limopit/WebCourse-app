using CourseAppUserService_Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController(IMediator mediator, ILoggerService logger) : ControllerBase
{
    protected readonly IMediator Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    protected readonly ILoggerService Logger = logger ?? throw new ArgumentNullException(nameof(logger));
}