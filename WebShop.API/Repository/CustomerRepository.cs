using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(int costumerId, Customer customer);
        Task<Customer> DeleteCustomer(int costumerId);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly WebShopContext _context;

        public CustomerRepository(WebShopContext context)
        {
            _context = context;

        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customer
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int bookId)
        {
            return await _context.Customer
                .FirstOrDefaultAsync(a => a.Id == bookId);
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomer(int customerId)
        {
            Customer customer = await _context.Customer.FirstOrDefaultAsync(a => a.Id == customerId);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return customer;
        }

        public async Task<Customer> UpdateCustomer(int customerId, Customer customer)
        {
            Customer updateCustomer = await _context.Customer.FirstOrDefaultAsync(a => a.Id == customerId);
            if (updateCustomer != null)
            {
                updateCustomer.FirstName = customer.FirstName;
                updateCustomer.MiddleName = customer.MiddleName;
                updateCustomer.LastName = customer.LastName;
                await _context.SaveChangesAsync();

            }
            return updateCustomer;
        }
    }
}
