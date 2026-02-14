using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_bookstoreApi.Models;

namespace WebApplication_bookstoreApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private static List<Book> books = new List<Book>()
        {
            new Book() { Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", Pages = 310, Price = 15.99f, CoverImg = "https://example.com/images/the-hobbit.jpg", Description = "A fantasy novel about Bilbo Baggins' adventurous quest to win a share of treasure guarded by a dragon.", IsInStock = true },
            new Book() { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Pages = 328, Price = 12.50f, CoverImg = "https://example.com/images/1984.jpg", Description = "A dystopian novel set in a totalitarian society ruled by Big Brother.", IsInStock = true },
            new Book() { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", Pages = 281, Price = 14.25f, CoverImg = "https://example.com/images/to-kill-a-mockingbird.jpg", Description = "A novel about racial injustice and moral growth in the American South.", IsInStock = false },
            new Book() { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", Pages = 180, Price = 10.99f, CoverImg = "https://example.com/images/the-great-gatsby.jpg", Description = "A story about the mysterious millionaire Jay Gatsby and his obsession with Daisy Buchanan.", IsInStock = true },
            new Book() { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Fiction", Pages = 277, Price = 11.75f, CoverImg = "https://example.com/images/the-catcher-in-the-rye.jpg", Description = "A novel following the experiences of Holden Caulfield in New York City.", IsInStock = true }
        };

        [HttpGet]
        public IActionResult AllBooks()
        {
            return Ok(books);
        }


        [HttpGet("(id)")]
        public IActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if(book == null)
            {
                return BadRequest("Can not find this book!");
            }

            return Ok(book);
        }

        [HttpGet]
        public IActionResult SearchBook(string? search)
        {
            var query = books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {

                query = query.Where(book =>
                    book.Author.ToLower().Contains(search.ToLower()) ||
                    book.Title.ToLower().Contains(search.ToLower())
                );
            }

            var results = query.ToList();

            if (!results.Any())
            {
                return BadRequest("No books have been found.");
            }

            return Ok(results);
        }

        [HttpGet]
        public IActionResult FilterBookByGenre(string? genre)
        {
            var query = books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(book => book.Genre.ToLower().Contains(genre.ToLower()));
            }

            var results = query.ToList();

            if (!results.Any())
            {
                return BadRequest("No books have been found.");
            }

            return Ok(results);
        }

        [HttpPost]
        public IActionResult AddNewBook(Book newBook)
        {
            var book = newBook;
            book.Id = newBook.Id;
            book.Title = newBook.Title;
            book.Author = newBook.Author;
            book.Pages = newBook.Pages;
            book.Price = newBook.Price;
            book.CoverImg = newBook.CoverImg;
            book.Description = newBook.Description;
            book.IsInStock = newBook.IsInStock;
            books.Add(book);

            return Ok(book);
        }

        [HttpPut]
        public IActionResult UpdateBook(Book updateBook)
        {
            var book = books.FirstOrDefault(book => book.Id == updateBook.Id);
            if (book == null)
            {
                return BadRequest("Can not find this book to update!");
            }

            book.Id = updateBook.Id;
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Pages = updateBook.Pages;
            book.Price = updateBook.Price;
            book.CoverImg = updateBook.CoverImg;
            book.Description = updateBook.Description;
            book.IsInStock = updateBook.IsInStock;

            return Ok(book);
        }

        [HttpDelete("(id)")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
            {
                return BadRequest("Can not find this book to delete!");
            }

            books.Remove(book);
            return Ok("The book has been deleted!");
        }
    }
}
