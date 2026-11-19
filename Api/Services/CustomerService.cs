using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;

namespace Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRepository<Address> _address;

        public CustomerService(ICustomerRepository customerRepository, IRepository<Address> address)
        {
            _customerRepository = customerRepository;
            _address = address;
        }

        public async Task<Customer?> CreateCustomer(CUCustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                PhoneNumber = customerDTO.PhoneNumber,
                CountryCode = customerDTO.CountryCode,
                UserId = customerDTO.UserId
            };
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveAsync();
            customer = await _customerRepository.GetCustomerDetailsById(customer.Id);
            return customer;
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return false;

            _customerRepository.Delete(customer);
            return await _customerRepository.ExistAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetCustomersDetails();
        }

        public Task<Customer?> GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer?> GetCustomerDetails(Guid id)
        {
            return await _customerRepository.GetCustomerDetailsById(id);
        }

        public async Task<StatusRecord> UpdateCustomer(Guid customerId, CUCustomerDTO customerDTO)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                return new StatusRecord(StatusEnum.CustomerNotFound, "Customer not found.");

            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            _customerRepository.Update(customer);
            await _customerRepository.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Customer was updated successfully.");
        }

        public async Task<StatusRecord> AddShippingAddressToCustomer(Guid customerId, AddressDTO addressDTO)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                return new StatusRecord(StatusEnum.CustomerNotFound, "Customer not found.");

            var shippingAddress = new Address
            {
                StreetAddress = addressDTO.StreetAddress,
                City = addressDTO.City,
                State = addressDTO.State,
                Country = addressDTO.Country,
                PostalCode = addressDTO.PostalCode
            };
            await _address.AddAsync(shippingAddress);
            await _address.SaveAsync();
            customer.ShippingAddressId = shippingAddress.Id;
            _customerRepository.Update(customer);
            await _customerRepository.SaveAsync();

            return new StatusRecord(StatusEnum.Updated, "Customer details have been updated.");
        }
    }
}
