using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_bookstoreApi.Data;
using WebApplication_bookstoreApi.Models;
using WebApplication_bookstoreApi.Models.DTOs;
using WebApplication_bookstoreApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>List of books</returns
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await bookService.GetBooksAsync();

            return Ok(books);
        }


        /// <summary>
        /// Get specific book by ID
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Detailed book information</returns>
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


        /// <summary>
        /// Search books by title or author
        /// </summary>
        /// <param name="query">Search text</param>
        /// <returns>Filtered list of books</returns>
        [HttpGet]
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


        /// <summary>
        /// Filter books by genre and year range
        /// </summary>
        /// <param name="genre">Genre (optional)</param>
        /// <returns>Filtered list of books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDto>>> FilterBooks([FromQuery] string? genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                return BadRequest(new { message = "Genre is required" });
            }
            var books = await bookService.FilterBooksAsync(genre);
            return Ok(books);

        }


        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="createBookDto">Book data</param>
        /// <returns>Created book</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            await bookService.CreateBook(createBookDto);

            return CreatedAtAction(nameof(GetBook), new { });
        }


        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <param name="updateBookDto">Updated data</param>
        /// <returns>Updated book</returns>
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


        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Deletion confirmation</returns>
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


        /// <summary>
        /// Get list of available genres
        /// </summary>
        /// <returns>List of genres</returns>
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
