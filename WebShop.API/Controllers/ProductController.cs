using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Services;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        
        

        public ProductController(IProductService productService)
        {
            _productService = productService;
           
        }
        #region Get All Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<ProductResponse> products = await _productService.GetAllProducts();
                if (products == null)
                {
                    return Problem("No Data is available to products, not even an empty list!");
                }
                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Get All Categories
        [HttpGet("/api/Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                List<CategoryResponse> categories = await _productService.GetAllCategories();
                if (categories == null)
                {
                    return Problem("No Data is available to categories, not even an empty list!");
                }
                if (categories.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Get Product By Id
        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            try
            {
                ProductResponse product = await _productService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Create Product
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] NewProduct newProduct)
        {
            try
            {
                ProductResponse product = await _productService.CreateProduct(newProduct);

                if (product == null)
                {
                    return Problem("Product was not created, something went wrong");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Create Image
        [HttpPost("image/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductImage([FromBody] NewProductImage newProductImage, [FromRoute] int productId)
        {
            try
            {
                ProductImageResponse image = await _productService.CreateProductImage(newProductImage, productId);

                if (image == null)
                {
                    return Problem("Image was not created, something went wrong");
                }
                return Ok(image);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Update Product
        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProduct updateProduct)
        {
            try
            {
                ProductResponse product = await _productService.UpdateProduct(productId, updateProduct);

                if (product == null)
                {
                    return Problem("Product was not updated, something went wrong");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Delete Product
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            try
            {
                bool result = await _productService.DeleteProduct(productId);
                if (!result)
                {
                    return Problem("Product was not deleted, something went wrogn!");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
        #region Delete Image
        [HttpDelete("image/{imageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductImage([FromRoute] int imageId)
        {
            try
            {
                bool result = await _productService.DeleteProductImage(imageId);
                if (!result)
                {
                    return Problem("image was not deleted, something went wrong!");
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
