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
            return await _context.CartItems
                .Include(c => c.Book)
                .Select(c => new CartItemDto
                {
                    Id = c.Id,
                    Book = c.Book,
                    Quantity = c.Quantity
                })
                .ToListAsync();
        }

        public async Task<CartItemDto> AddToTheCartAsync(CartItemDto cartItemDto)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemDto.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = cartItemDto.Id,
                    Quantity = cartItemDto.Quantity
                };

                await _context.CartItems.AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();

            var book = await _context.Books.FindAsync(cartItemDto.Id);

            return new CartItemDto
            {
                Id = cartItemDto.Id,
                Book = book!,
                Quantity = cartItemDto.Quantity
            };
        }

        public async Task DeleteBookAsync(CartItemDto cartItemDto)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemDto.Id);

            if (cartItem == null)
                throw new KeyNotFoundException("Book not found in cart.");

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
