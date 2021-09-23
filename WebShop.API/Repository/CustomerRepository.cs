using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<Customer> UpdateCustomer(int costumerId, Customer customer);
        Task<Customer> CreateCustomer(Customer customer);
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
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _context.Customer
                .Include(a => a.User)
                .Include(a => a.Addresses)
                .ThenInclude(a => a.ZipCity)
                .FirstOrDefaultAsync(a => a.Id == customerId);
        }

        public async Task<Customer> UpdateCustomer(int customerId, Customer customer)
        {
            Customer updateCustomer = await _context.Customer.FirstOrDefaultAsync(a => a.Id == customerId);
            if (updateCustomer != null)
            {
                updateCustomer.FirstName = customer.FirstName;
                updateCustomer.MiddleName = customer.MiddleName;
                updateCustomer.LastName = customer.LastName;

                //Mangler at gå igennem user informationen som vi ændre...

                await _context.SaveChangesAsync();
            }
            return updateCustomer;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
