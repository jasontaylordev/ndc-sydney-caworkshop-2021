﻿using CaWorkshop.Application.Common.Security;

namespace CaWorkshop.Application.TodoLists.Commands.DeleteTodoList;

[Authorise]
public class DeleteTodoListCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoListCommandHandler : AsyncRequestHandler<DeleteTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(nameof(TodoList), request.Id);

        _context.TodoLists.Remove(entity!);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
