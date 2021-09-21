using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Authorization;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Helpers;
using WebShop.API.Services;

//Husk at slå auth til igen når alt er færdigt
namespace WebShop.API.Controllers
{        
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginSectionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;

        public LoginSectionController(IUserService userService, ICustomerService customerService)
        {
            _userService = userService;
            _customerService = customerService;
        }

        #region User

        #region Authenticate (LOGIN)
        [AllowAnonymous]
        [HttpPost("authenticate")]
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

        #region Register (SIGNUP)
        [AllowAnonymous]
        [HttpPost("register")]
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

        #region GetAllUsers (ADMIN ONLY)
        /*[Authorize(Role.Admin)]*/ // only admins are allowed entry to this endpoint
        [HttpGet]
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

        #region GetByUserId (ADMIN ONLY)
        //[Authorize(Role.User, Role.Admin)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] int userId)
        {
            try
            {
                // only admins can access other user records
                var currentUser = (UserResponse)HttpContext.Items["User"];
                if (userId != currentUser.Id && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                UserResponse user = await _userService.GetByUserId(userId);

                if (user == null)
                {
                    return NoContent();
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #region Delete (User limited)
        [HttpDelete("{userId}")]
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

        #endregion

        #region Customer

        [Route("/api/customer")]
        #region GetAllCustomers (ADMIN ONLY)

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        #region GetCustomerById (ADMIN ONLY)
        //[Authorize(Role.User, Role.Admin)]
        [Route("/api/customer2")]
        //[HttpGet("{userId}")]
        [HttpGet]

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

        #endregion
    }
}
