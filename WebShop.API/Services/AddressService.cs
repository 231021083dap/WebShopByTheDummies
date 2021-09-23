using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public interface IAddressService
    {
        Task<AddressResponse> Create(NewAddress newAddress);
        //Task<AddressResponse> UpdateAddress(int customerId, UpdateAddress updateAddress);
        Task<bool> Delete(int addressId);
    }
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        //USER - CREATE - Address
        public async Task<AddressResponse> Create(NewAddress newAddress)
        {
            Address address = new Address
            {
                StreetName = newAddress.StreetName,
                Number = newAddress.Number,
                Floor = newAddress.Floor,
                ZipCityId = newAddress.Zipcode,
                Country = newAddress.Country
            };

            address = await _addressRepository.CreateAddress(address);

            return address == null ? null : new AddressResponse
            {
                Id = address.CustomerId,
                StreetName = address.StreetName,
                Number = address.Number,
                Floor = address.Floor,
                //Zipcode = address.ZipCityId,
                Country = address.Country
            };
        }

        //USER - UPDATE - Address
        //public async Task<AddressResponse> UpdateAddress(int customerId, UpdateAddress updateAddress)
        //{
        //}

        //USER - DELETE - Address
        public async Task<bool> Delete(int addressId)
        {
            var result = await _addressRepository.DeleteAddress(addressId);
            return true;
        }
    }
}
