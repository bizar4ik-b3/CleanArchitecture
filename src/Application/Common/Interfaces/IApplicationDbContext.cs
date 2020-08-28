using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<CleanArchitecture.Domain.Entities.Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
