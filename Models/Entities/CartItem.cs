using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_bookstoreApi.Models.Entities
{
    [Table("cart")]
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Column("title")]
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Author { get; set; } = string.Empty;


        [Range(1, 200)]
        public float ItemPrice { get; set; }

        [Required]
        [Range(1, 200)]
        public float TotalPrice { get => ItemPrice * Quantity; }

        [Required]
        public string CoverImg { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }
    }
}
