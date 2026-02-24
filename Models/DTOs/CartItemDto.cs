namespace WebApplication_bookstoreApi.Models.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public float ItemPrice { get; set; }
        public float TotalPrice { get; set; }
        public string CoverImg { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
