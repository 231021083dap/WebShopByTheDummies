using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAllCustomers();
        Task<CustomerResponse> GetCustomerById(int customerId);
        Task<CustomerResponse> UpdateCustomer(int customerId, UpdateCustomer updateCustomer);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        //ADMIN
        public async Task<List<CustomerResponse>> GetAllCustomers()
        {
            List<Customer> customers = await _customerRepository.GetAllCustomers();
            return customers == null ? null : customers.Select(customer => new CustomerResponse
            {
                User = new CustomerUserResponse
                {
                    Id = customer.User.Id,
                    Email = customer.User.Email
                },
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
            }).ToList();
        }

        //ADMIN
        public async Task<CustomerResponse> GetCustomerById(int userId)
        {
            Customer customer = await _customerRepository.GetCustomerById(userId);
            return customer == null ? null : new CustomerResponse
            {
                User = new CustomerUserResponse
                {
                    Id = customer.User.Id,
                    Email = customer.User.Email
                },

                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,

                Addresses = customer.Addresses.Select(address => new CustomerAddressResponse
                {
                    Id = address.Id,
                    StreetName = address.StreetName,
                    Number = address.Number,
                    Floor = address.Floor,
                    Zipcode = address.ZipCity.Id,
                    City = address.ZipCity.City,
                    Country = address.Country
                }).ToList()
            };
        }

        //USER - UPDATE - Name & LoginInfo
        public async Task<CustomerResponse> UpdateCustomer(int customerId, UpdateCustomer updateCustomer)
        {
            //Name
            Customer customer = new Customer
            {
                FirstName = updateCustomer.FirstName,
                MiddleName = updateCustomer.MiddleName,
                LastName = updateCustomer.LastName
            };
            customer = await _customerRepository.UpdateCustomer(customerId, customer);

            //Login
            if (customer != null)
            {
                User user = new User
                {
                    Email = updateCustomer.User.Email,
                    Password = updateCustomer.User.Password
                };
                user = await _userRepository.UpdateUser(customerId, user);
            }
            return null;
        }
    }
}
