using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_bookstoreApi.Models.Entities
{
    [Table("basket")]
    public class BasketItem
    {
        public int Id { get; set; }

        public int MyProperty { get; set; }
    }
}
