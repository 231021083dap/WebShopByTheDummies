using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Authorization;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Services;

//Husk at slå auth til igen når alt er færdigt
namespace WebShop.API.Controllers
{        
    //[Authorize]
    [Route("API/")]
    [ApiController]
    public class LoginSectionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;
        public LoginSectionController(IUserService userService, ICustomerService customerService, IAddressService addressService)
        {
            _userService = userService;
            _customerService = customerService;
            _addressService = addressService;
        }

        #region Authenticate (LOGIN)
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate(LoginRequest login)
        {
            try
            {
                LoginResponse response = await _userService.Authenticate(login);

                if (response == null)
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #region Get All Users
        /*[Authorize(Role.Admin)]*/ // only admins are allowed entry to this endpoint
        [HttpGet("User/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllUsers();

                if (users == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Get All Customers
        [HttpGet("User/Customer/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                List<CustomerResponse> customers = await _customerService.GetAllCustomers();

                if (customers == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (customers.Count == 0)
                {
                    return NoContent();
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        #endregion

        #region Get User By Id
        //[Authorize(Role.User, Role.Admin)]
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] int userId)
        {
            try
            {
                // only admins can access other user records
                //var currentUser = (UserResponse)HttpContext.Items["User"];
                //if (userId != currentUser.Id && currentUser.Role != Role.Admin)
                //{
                //    return Unauthorized(new { message = "Unauthorized" });
                //}

                UserResponse user = await _userService.GetByUserId(userId);

                //if (user == null)
                //{
                //    return NoContent();
                //}

                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Get Customer By Id
        //[Authorize(Role.User, Role.Admin)]
        [HttpGet("User/Customer/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCustomerId([FromRoute] int userId)
        {
            try
            {
                // only admins can access other user records
                //var currentUser = (UserResponse)HttpContext.Items["User"];
                //if (userId != currentUser.Id && currentUser.Role != Role.Admin)
                //{
                //    return Unauthorized(new { message = "Unauthorized" });
                //}

                CustomerResponse user = await _customerService.GetCustomerById(userId);

                //if (user == null)
                //{
                //    return NoContent();
                //}

                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        
        #region Register (SIGNUP)
        [AllowAnonymous]
        [HttpPost("Register/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterUser newUser)
        {
            try
            {
                UserResponse user = await _userService.Register(newUser);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Create Address
        [AllowAnonymous]
        [HttpPost("User/Customer/{customerId}/Address")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAddress([FromBody] NewAddress newAddress)
        {
            try
            {
                AddressResponse address = await _addressService.Create(newAddress);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #region Update Customer
        [HttpPut("User/Customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int customerId, [FromBody] UpdateCustomer updateCustomer)
        {
            try
            {
                CustomerResponse Customer = await _customerService.UpdateCustomer(customerId, updateCustomer);

                if (Customer == null)
                {
                    return Problem("Customer was not updated, something went wrong");
                }

                return Ok(Customer);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Update Address
        [HttpPut("User/Customer/Address/{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int addressId, [FromBody] UpdateAddress updateAddress)
        {
            try
            {
                AddressResponse Address = await _addressService.UpdateAddress(addressId, updateAddress);

                if (Address == null)
                {
                    return Problem("Address was not updated, something went wrong");
                }
                return Ok(Address);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #region Delete User
        [HttpDelete("User/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            try
            {
                bool result = await _userService.Delete(userId);

                if (!result)
                {
                    return Problem("User was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Delete Address
        [HttpDelete("User/Customer/Address/{addressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddress([FromRoute] int addressId)
        {
            try
            {
                bool result = await _addressService.Delete(addressId);

                if (!result)
                {
                    return Problem("Address was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
    }
}
