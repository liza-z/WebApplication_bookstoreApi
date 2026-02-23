namespace WebApplication_bookstoreApi.Models.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Pages { get; set; }
        public float Price { get; set; }
        public string CoverImg { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsInStock { get; set; }
    }
}
