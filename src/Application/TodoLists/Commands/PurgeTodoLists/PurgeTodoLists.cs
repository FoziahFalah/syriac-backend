﻿using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Security;
using SyriacSources.Backend.Domain.Constants;

namespace SyriacSources.Backend.Application.TodoLists.Commands.PurgeTodoLists;


[Authorize(Policy = Policies.CanPurge)]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.TodoLists.RemoveRange(_context.TodoLists);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
