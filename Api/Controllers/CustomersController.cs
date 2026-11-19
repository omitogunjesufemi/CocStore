using Api.DTOs;
using Api.Models;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CUCustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerService.CreateCustomer(customerDTO);
            if (customer == null)
                return BadRequest("Failed to create customer.");

            var dto = DTOConverter(customer);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            var dtos = customers.Select(c => DTOConverter(c));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetCustomerDetails(id);
            if (customer == null)
                return NotFound("Customer not found.");

            var dto = DTOConverter(customer);
            return Ok(dto);
        }

        private CustomerDTO DTOConverter(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                UserId = customer.UserId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.User?.Email,
                CountryCode = customer.CountryCode,
                PhoneNumber = customer.PhoneNumber,
                Address = String.Concat([customer.ShippingAddress?.StreetAddress, customer.ShippingAddress?.City, customer.ShippingAddress?.State, customer.ShippingAddress?.Country]) ?? "N/A",
            };
        }
    }
}
