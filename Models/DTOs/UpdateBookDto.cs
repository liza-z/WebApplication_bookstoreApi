using System.ComponentModel.DataAnnotations;

namespace WebApplication_bookstoreApi.Models.DTOs
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public int Pages { get; set; }

        [Required]
        [Range(1, 200)]
        public float Price { get; set; }

        [Required]
        public string CoverImg { get; set; } = string.Empty;
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public bool IsInStock { get; set; }
    }
}
