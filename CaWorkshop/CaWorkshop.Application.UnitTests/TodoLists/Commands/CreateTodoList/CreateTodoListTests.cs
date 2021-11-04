using System.Threading;
using System.Threading.Tasks;
using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandTests : IClassFixture<TestFixture>
{
    private readonly ApplicationDbContext _context;

    public CreateTodoListCommandTests(TestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public async Task Handle_ShouldPersistTodoList()
    {
        var command = new CreateTodoListCommand
        {
            Title = "Bucket List"
        };

        var handler = new CreateTodoListCommandHandler(_context);

        var id = await handler.Handle(command,
            CancellationToken.None);

        var entity = await _context.TodoLists
            .FirstAsync(tl => tl.Id == id);

        entity.Should().NotBeNull();
        entity.Title.Should().Be(command.Title);
    }
}
