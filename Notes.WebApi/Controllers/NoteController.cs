﻿namespace Notes.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;

    public NoteController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Gets the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note
    /// </remarks>
    /// <returns>Returns NoteListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery() { UserId = UserId };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Gets the note by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note/A13040C3-135F-41ED-9C0D-42360B188C87
    /// </remarks>
    /// <param name="id">Note Id (guid)</param>
    /// <returns>Returns NoteDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery() { UserId = UserId, Id = id };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Creates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// Post /note
    /// {
    ///     title: "note title",
    ///     details: "note details
    /// }
    /// </remarks>
    /// <param name="createNoteDto">CreateNoteDto object</param>
    /// <returns>Returns Id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var noteId = await Mediator.Send(command);
        return Created(noteId.ToString(), noteId);
    }

    /// <summary>
    /// Update the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /note
    /// {
    ///     title: "update note title"
    /// }
    /// </remarks>
    /// <param name="updateNoteDto">UpdateNoteDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.Id = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes the note by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE note/39225E92-08EC-466C-9388-31ED38455445
    /// </remarks>
    /// <param name="id">Id of the note (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand() { UserId = UserId, Id = id };
        await Mediator.Send(command);
        return NoContent();
    }
}
