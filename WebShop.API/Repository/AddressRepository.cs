using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Responses;

namespace WebShop.API.Repository
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAddresses();
        Task<Address> GetAddressById(int addressId);
        Task<Address> CreateAddress(Address address);
        Task<Address> UpdateAddress(int addressId, Address address);
        Task<Address> DeleteAddress(int addressId);
    }
    public class AddressRepository : IAddressRepository
    {
        private readonly WebShopContext _context;

        public AddressRepository(WebShopContext context)
        {
            _context = context;

        }

        public async Task<List<Address>> GetAllAddresses()
        {
            return await _context.Address
                .Include(a => a.ZipCity)
                .ToListAsync();
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            return await _context.Address
                .Include(a => a.ZipCity)
                .FirstOrDefaultAsync(a => a.Id == addressId);
        }

        public async Task<Address> CreateAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> DeleteAddress(int addressId)
        {
            Address address = await _context.Address.FirstOrDefaultAsync(a => a.Id == addressId);
            if (address != null)
            {
                _context.Address.Remove(address);
                await _context.SaveChangesAsync();
            }
            return address;
        }

        public async Task<Address> UpdateAddress(int addressId, Address address)
        {
            Address updateAddress = await _context.Address.FirstOrDefaultAsync(a => a.Id == addressId);
            if (updateAddress != null)
            {
                updateAddress.StreetName = address.StreetName;
                updateAddress.Number = address.Number;
                updateAddress.Floor = address.Floor;
                updateAddress.ZipCityId = address.ZipCityId;
                updateAddress.Country = address.Country;
                await _context.SaveChangesAsync();

            }
            return updateAddress;
        }
    }
}
