using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebShop.API.DTO.Responses;

namespace WebShop.API.Controllers
{
    //In this controller will setup everything related customers and Address
    public class CustomerController
    {
        //[Route("api/[controller]")]
        //[ApiController]
        //public class AuthorController : ControllerBase
        //{
        //    //private readonly ICustomerService _customerService;

        //    //public AuthorController(ICustomerService customerService)
        //    //{
        //    //    _customerService = customerService;
        //    //}

        //    #region UPDATE

        //    //[HttpPut("{customerId}")]
        //    //[ProducesResponseType(StatusCodes.Status200OK)]
        //    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //    //public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] UpdateCustomer updateCustomer)
        //    //{
        //    //    try
        //    //    {
        //    //        CustomerResponse Customer = await _customerService.Update(customerId, updateCustomer);

        //    //        if (Customer == null)
        //    //        {
        //    //            return Problem("Customer was not updated, something went wrong");
        //    //        }

        //    //        return Ok(Customer);
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return Problem(ex.Message);
        //        }
        //    }

        //    #endregion
        //}
    }
}
