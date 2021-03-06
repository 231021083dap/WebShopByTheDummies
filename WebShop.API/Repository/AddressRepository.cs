using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IAddressRepository
    {
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

        #region Get Address By Id
        public async Task<Address> GetAddressById(int addressId)
        {
            return await _context.Address
                .Include(a => a.ZipCity)
                .FirstOrDefaultAsync(a => a.Id == addressId);
        }
        #endregion

        #region Create Address
        public async Task<Address> CreateAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        #endregion

        #region Delete Address
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
        #endregion

        #region Update Address
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
        #endregion
    }
}
