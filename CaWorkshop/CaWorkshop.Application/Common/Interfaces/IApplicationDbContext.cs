using CaWorkshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; }

        DbSet<TodoList> TodoLists { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
