using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;

namespace Api.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _apiDbContext;
        private IDbContextTransaction? _transaction;
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }
        public IRepository<Category> Categories { get; }
        public ICartRepository Carts { get; }


        public UnitOfWork(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
            Products = new ProductRepository(_apiDbContext);
            Orders = new OrderRepository(_apiDbContext);
            Categories = new GenericRepository<Category>(_apiDbContext);
            Carts = new CartRepository(_apiDbContext);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
                return _transaction;

            _transaction = await _apiDbContext.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to commit.");

            await _apiDbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
            await DisposeTransactionAsync();
        }

        public async Task DisposeTransactionAsync()
        {
            if ( _transaction != null )
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _apiDbContext?.Dispose();
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to rollback.");

            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }

        public int SaveChanges()
        {
            return _apiDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _apiDbContext.SaveChangesAsync();
        }
    }
}
