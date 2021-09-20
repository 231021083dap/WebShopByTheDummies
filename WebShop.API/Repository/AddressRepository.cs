﻿using Microsoft.EntityFrameworkCore;
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
    public class AuthorRepository : IAddressRepository
    {
        private readonly WebShopContext _context;

        public AuthorRepository(WebShopContext context)
        {
            _context = context;

        }
        #region Get All Addresses
        public async Task<List<Address>> GetAllAddresses()
        {
            return await _context.Address
                .Include(a => a.Zipcode)
                .ToListAsync();
        }
        #endregion
        #region Get Address By Id
        public async Task<Address> GetAddressById(int addressId)
        {
            return await _context.Address
                .Include(a => a.Zipcode)
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
                updateAddress.Zipcode = address.Zipcode;
                updateAddress.County = address.County;
                await _context.SaveChangesAsync();

            }
            return updateAddress;
        }
        #endregion
    }
}