namespace Notes.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    internal Guid UserId => User.Identity.IsAuthenticated
        ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
        : Guid.Empty;
}
