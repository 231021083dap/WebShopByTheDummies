using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.API.Controllers
{
    public class CategoryController
    {
        #region rodemappe (SLET EFTER BRUG)
        //private readonly IUserService _userService;

        //public UserController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        #region AUTHENTICATE LOGIN REQUEST

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Authenticate(LoginRequest login)
        //{
        //    try
        //    {
        //        LoginResponse response = await _userService.Authenticate(login);

        //        if (response == null)
        //        {
        //            return Unauthorized();
        //        }

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        #region SIGNUP / REGISTER

        //[AllowAnonymous]
        //[HttpPost("register")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Register([FromBody] RegisterUser newUser)
        //{
        //    try
        //    {
        //        UserResponse user = await _userService.Register(newUser);
        //        return Ok(user);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        #region GET ALL USERS - (ADMIN USE ONLY)

        //[Authorize(Role.Admin)] // only admins are allowed entry to this endpoint
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        List<UserResponse> users = await _userService.GetAll();

        //        if (users == null)
        //        {
        //            return Problem("Got no data, not even an empty list, this is unexpected");
        //        }

        //        if (users.Count == 0)
        //        {
        //            return NoContent();
        //        }

        //        return Ok(users);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        #region GET USER BY ID - (ADMIN USE ONLY)

        //[Authorize(Role.User, Role.Admin)]
        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetById([FromRoute] int userId)
        //{
        //    try
        //    {
        //        // only admins can access other user records
        //        var currentUser = (UserResponse)HttpContext.Items["User"];
        //        if (userId != currentUser.Id && currentUser.Role != Role.Admin)
        //        {
        //            return Unauthorized(new { message = "Unauthorized" });
        //        }

        //        UserResponse user = await _userService.GetById(userId);

        //        if (user == null)
        //        {
        //            return NoContent();
        //        }

        //        return Ok(user);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        //This would include admin and customers
        #region GET ALL USERS 

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        List<CustomerResponse> Customers = await _customerService.GetAllAuthors();

        //        if (Customers == null)
        //        {
        //            return Problem("Got no data, not een an empty list, this is unexpected");
        //        }

        //        if (Customers.Count == 0)
        //        {
        //            return NoContent();
        //        }

        //        return Ok(Customers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        //This would includes admin and customers
        #region GET USER BY ID

        //[HttpGet("{customerId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetById([FromRoute] int authorId)
        //{
        //    try
        //    {
        //        AuthorResponse Author = await _authorService.GetById(authorId);

        //        if (Author == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(Author);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        //Should only allow customer signup - (Admin role should only be added from inside the system)
        //Role "user" HAVE! to be added by default
        #region CREATE USER

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Create([FromBody] NewUser newUser)
        //{
        //    try
        //    {
        //        UserResponse Author = await _userService.Create(newUser);

        //        if (Author == null)
        //        {
        //            return Problem("User was not created, something went wrong");
        //        }

        //        return Ok(User);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        //Should be possibel by both customer and admin as long as they CAN'T change role
        #region UPDATE/EDIT USER

        //[HttpPut("{customerId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] NewCustomer updateCustomer)
        //{
        //    try
        //    {
        //        CustomerResponse Customer = await _customerService.Update(customerId, updateCustomer);

        //        if (Customer == null)
        //        {
        //            return Problem("Author was not updated, something went wrong");
        //        }

        //        return Ok(Customer);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion

        //This should only be useable by admin user
        #region DELETE USER

        //[HttpDelete("{customerId}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Delete([FromRoute] int customerId)
        //{
        //    try
        //    {
        //        bool result = await _customerService.Delete(customerId);

        //        if (!result)
        //        {
        //            return Problem("Customer was not deleted, something went wrong");
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        #endregion
        #endregion
    }
}
