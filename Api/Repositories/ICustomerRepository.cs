using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetCustomerDetailsById(Guid customerId);
        Task<IEnumerable<Customer>> GetCustomersDetails();
    }
}
