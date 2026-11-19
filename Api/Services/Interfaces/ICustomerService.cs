using Api.DTOs;
using Api.Models;
using Api.Services.Results;

namespace Api.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerById(Guid id);
        Task<Customer?> GetCustomerDetails(Guid id);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerByEmail(string email);
        Task<Customer?> CreateCustomer(CUCustomerDTO customerDTO);
        Task<Customer?> UpdateCustomer(Guid customerId, CUCustomerDTO customerDTO);
        Task<bool> DeleteCustomer(Guid id);
        Task<StatusRecord> AddShippingAddressToCustomer(Guid customerId, AddressDTO addressDTO);
    }
}
