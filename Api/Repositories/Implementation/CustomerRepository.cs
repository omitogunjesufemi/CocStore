using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementation
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public CustomerRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Customer?> GetCustomerDetailsById(Guid customerId)
        {
            return await _apiDbContext.Set<Customer>().AsNoTracking().Include(c => c.User).Include(c => c.ShippingAddress).FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<IEnumerable<Customer>> GetCustomersDetails()
        {
            return await _apiDbContext.Set<Customer>().AsNoTracking().Include(c => c.User).Include(c => c.ShippingAddress).ToListAsync();
        }
    }
}
