using WebApplication_bookstoreApi.Models.DTOs;

namespace WebApplication_bookstoreApi.Services
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetCartInfoAsync();
        Task<CartItemDto> AddToTheCartAsync(AddToCartDto addToCartDto);
        Task DeleteBookAsync(int id);

    }
}
