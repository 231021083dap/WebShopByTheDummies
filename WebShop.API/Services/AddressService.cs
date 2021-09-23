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
        Task<AddressResponse> UpdateAddress(int addressId, UpdateAddress updateAddress);
        Task<bool> Delete(int addressId);
    }
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IZipCityRepository _zipCityRepository;

        public AddressService(IAddressRepository addressRepository, ICustomerRepository customerRepository, IZipCityRepository zipCityRepository)
        {
            _addressRepository = addressRepository;
            _zipCityRepository = zipCityRepository;
            _customerRepository = customerRepository;
        }

        //USER - CREATE - Address
        public async Task<AddressResponse> Create(NewAddress newAddress)
        {
            Address address = new Address
            {
                CustomerId = newAddress.CustomerId,
                StreetName = newAddress.StreetName,
                Number = newAddress.Number,
                Floor = newAddress.Floor,
                ZipCityId = newAddress.Zipcode,
                Country = newAddress.Country
            };

            address = await _addressRepository.CreateAddress(address);
            if (address != null)
            {
                Customer customer = await _customerRepository.GetCustomerById(address.CustomerId);
                if (customer != null)
                {
                    address.ZipCity = await _zipCityRepository.GetZipCityById(address.ZipCityId);
                    return new AddressResponse
                    {
                        Id = address.Id,
                        Customer = new AddresseCustomerResponse
                        {
                            FirstName = customer.FirstName,
                            MiddleName = customer.MiddleName,
                            LastName = customer.LastName,
                            User = new CustomerUserResponse
                            {
                                Email = customer.User.Email,
                                Id = customer.User.Id
                            }
                        },
                        StreetName = address.StreetName,
                        Number = address.Number,
                        Floor = address.Floor,
                        Zipcode = address.ZipCity.Id,
                        City = address.ZipCity.City,
                        Country = address.Country
                    };
                }
            }
            return null;
        }

        //USER - UPDATE - Address
        public async Task<AddressResponse> UpdateAddress(int addressId, UpdateAddress updateAddress)
        {
            Address address = new Address
            {
                CustomerId = updateAddress.CustomerId,
                StreetName = updateAddress.StreetName,
                Number = updateAddress.Number,
                Floor = updateAddress.Floor,
                ZipCityId = updateAddress.Zipcode,
                Country = updateAddress.Country
            };

            address = await _addressRepository.UpdateAddress(addressId, address);

            //if (address != null)
            //{
            //    Address address1 = await _addressRepository.GetAddressById(addressId);
            //    return new BookResponse
            //    {
            //        Id = book.Id,
            //        Title = book.Title,
            //        Pages = book.Pages,
            //        Author = new BookAuthorResponse
            //        {
            //            Id = book.Author.Id,
            //            FirstName = book.Author.FirstName,
            //            MiddleName = book.Author.MiddleName,
            //            LastName = book.Author.LastName
            //        }
            //    };
            //}
            return null;
        }

        //USER - DELETE - Address
        public async Task<bool> Delete(int addressId)
        {
            var result = await _addressRepository.DeleteAddress(addressId);
            return true;
        }
    }
}
