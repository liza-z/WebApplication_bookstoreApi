using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_bookstoreApi.Data;
using WebApplication_bookstoreApi.Models;
using WebApplication_bookstoreApi.Models.DTOs;
using WebApplication_bookstoreApi.Services;

namespace WebApplication_bookstoreApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService service)
        {
            this.bookService = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await bookService.GetBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }
            var bookDetail = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Pages = book.Pages,
                Price = book.Price,
                CoverImg = book.CoverImg,
                Description = book.Description,
                IsInStock = book.IsInStock
            };
            return Ok(bookDetail);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "Search query is required" });
            }
            var books = await bookService.SearchBookAsync(query);
            return Ok(books);
        }

        //[HttpGet("filter")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<IEnumerable<BookDto>>> FilterBooks([FromQuery] string? genre)
        //{
        //    var query = bookService.Books.AsQueryable();
        //    if (!string.IsNullOrWhiteSpace(genre))
        //    {
        //        query = query.Where(b => b.Genre == genre);
        //    }
        //    var books = await query
        //    .Select(b => new BookDto
        //    {
        //        Id = b.Id,
        //        Title = b.Title,
        //        Author = b.Author,
        //        Genre = b.Genre,
        //        Pages = b.Pages,
        //        Price = b.Price,
        //        CoverImg = b.CoverImg,
        //        Description = b.Description,
        //        IsInStock = b.IsInStock
        //    })
        //    .ToListAsync();
        //    return Ok(books);
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            await bookService.CreateBook(createBookDto);

            return CreatedAtAction(nameof(GetBook), new { });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            var updatedBook = await bookService.GetBookByIdAsync(id);
            if (updatedBook == null)
            {
                return NotFound(new { message = "Book not found" });
            }
            await bookService.UpdateBook(id, updateBookDto);

            return Ok(new { message = "Book has been updated successfully"});
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }
            await bookService.DeleteBookAsync(id);
            return Ok(new { message = "Book has been deleted successfully" });
        }

        //[HttpGet("genres")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<IEnumerable<string>>> GetGenres()
        //{
        //    var genres = await bookService.Books
        //    .Select(b => b.Genre)
        //    .Distinct()
        //    .OrderBy(g => g)
        //    .ToListAsync();

        //    return Ok(genres);
        //}


    }
}
