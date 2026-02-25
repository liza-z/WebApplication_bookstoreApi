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

        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService service)
        {
            _cartItemService = service;
        }


        /// <summary>
        /// Get all books from the cart
        /// </summary>
        /// <returns>List of books</returns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCart()
        {
            var cartItems = await _cartItemService.GetCartInfoAsync();
            return Ok(cartItems);
        }


        /// <summary>
        /// Add a book to the cart
        /// </summary>
        /// <param name="cartItemDto">Book Data</param>
        /// <returns>Created book</returns>
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddToCart([FromBody] CartItemDto cartItemDto)
        {
            var result = await _cartItemService.AddToTheCartAsync(cartItemDto);
            return Ok(result);
        }


        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Deletion confirmation</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _cartItemService.DeleteBookAsync(id);
            return NoContent();
        }

    }
}
