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
        IUserRepository Users { get; }
        ICustomerRepository Customers { get; }
        IRepository<Role> Roles { get; }
        IRepository<RefreshToken> RefreshTokens { get; }


        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
