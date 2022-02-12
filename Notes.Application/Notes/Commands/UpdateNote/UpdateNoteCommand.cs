﻿namespace Notes.Application.Notes.Commands.UpdateNote;
public class UpdateNoteCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Details { get; set; } = null!;
}
