using Microsoft.EntityFrameworkCore;
using WebApplication_bookstoreApi.Models.Entities;

namespace WebApplication_bookstoreApi.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions options) : base(options)

        {

        }

        protected BookstoreContext()

        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
