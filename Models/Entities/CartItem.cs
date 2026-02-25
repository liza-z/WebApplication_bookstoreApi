using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_bookstoreApi.Models.Entities
{
    [Table("cart")]
    public class CartItem
    {
        public int Id { get; set; }
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
