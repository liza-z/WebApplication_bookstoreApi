using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_bookstoreApi.Models.DTOs;
using WebApplication_bookstoreApi.Services;

namespace WebApplication_bookstoreApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _service;

        public CartItemsController(ICartItemService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get all books from the cart
        /// </summary>
        /// <returns>List of books</returns
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var result = await _service.GetCartInfoAsync();
            return Ok(result);
        }


        /// <summary>
        /// Add a book to the cart
        /// </summary>
        /// <param name="addToCartDto">Book Data</param>
        /// <returns>Created book</returns>
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {

            await _service.AddToTheCartAsync(addToCartDto);
            return Ok("Item added to cart successfully");
        }


        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Deletion confirmation</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _service.DeleteBookAsync(id);
            return Ok("Item removed successfully");
        }
    }
}
