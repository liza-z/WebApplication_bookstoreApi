using Microsoft.EntityFrameworkCore;
using WebApplication_bookstoreApi.Data;
using WebApplication_bookstoreApi.Models.DTOs;
using WebApplication_bookstoreApi.Models.Entities;

namespace WebApplication_bookstoreApi.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly BookstoreContext _context;

        public CartItemService(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartInfoAsync()
        {
            var items = await _context.CartItems
            .Select(item => new CartItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                ItemPrice = item.ItemPrice,
                Quantity = item.Quantity,
                TotalPrice = item.ItemPrice * item.Quantity,
                CoverImg = item.CoverImg
            })
            .ToListAsync();
            return (items);
        }

        public async Task<CartItemDto> AddToTheCartAsync(AddToCartDto addToCartDto)
        {
            var book = await _context.Books.FindAsync(addToCartDto.BookId);

            if (book == null)
                throw new Exception("Book not found");

            var cartItem = new CartItem
            {
                BookId = book.Id,
                Quantity = addToCartDto.Quantity
            };

            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return new CartItemDto
            {
                Id = cartItem.Id,
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author,
                ItemPrice = book.Price,
                Quantity = cartItem.Quantity,
                TotalPrice = book.Price * cartItem.Quantity,
                CoverImg = book.CoverImg
            };
        }

        public async Task DeleteBookAsync(int id)
        {
            var item = await _context.CartItems.FindAsync(id);

            if (item == null)
                throw new Exception("Item not found");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
