using Api.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IRepository<Category> Categories {  get; }
        ICartRepository Carts { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
