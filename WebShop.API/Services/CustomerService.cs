﻿using System.Collections.Generic;
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
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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

                    ZipCity = new ZipCityResponse
                    {
                        Zipcode = address.ZipCity.Zipcode,
                        City = address.ZipCity.City
                    },

                    County = address.County
                }).ToList()
            };
        }

        //USER - Update account
        public async Task<CustomerResponse> UpdateCustomer(int customerId, UpdateCustomer updateCustomer)
        {
            Customer customer = new Customer
            {
                //Email
                //Password
                FirstName = updateCustomer.FirstName,
                MiddleName = updateCustomer.MiddleName,
                LastName = updateCustomer.LastName
            };

            customer = await _customerRepository.UpdateCustomer(customerId, customer);

            return customer == null ? null : new CustomerResponse
            {
                ////Skal være det samme som vist øverst!
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName
            };
        }

        //Update address

        //Delete address

        //Create address 
    }
}
