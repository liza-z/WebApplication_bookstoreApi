using WebApplication_bookstoreApi.Models.Entities;

namespace WebApplication_bookstoreApi.Models.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
