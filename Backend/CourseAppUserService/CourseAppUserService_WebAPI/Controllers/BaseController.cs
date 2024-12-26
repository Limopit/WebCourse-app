using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected BaseController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
}