using Microsoft.EntityFrameworkCore;
using WebApplication_bookstoreApi.Data;
using WebApplication_bookstoreApi.Models.DTOs;
using WebApplication_bookstoreApi.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication_bookstoreApi.Services
{
    public class BookService : IBookService
    {
        private readonly BookstoreContext _context;

        public BookService(BookstoreContext context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            var books = await _context.Books
           .Select(b => new BookDto
           {
               Id = b.Id,
               Title = b.Title,
               Author = b.Author,
               Genre = b.Genre,
               Pages = b.Pages,
               Price = b.Price,
               CoverImg = b.CoverImg,
               Description = b.Description,
               IsInStock = b.IsInStock
           })
           .ToListAsync();
            return books;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);

        }

        public async Task<IEnumerable<BookDto>> SearchBookAsync(string query)
        {
           return await _context.Books
            .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                Pages = b.Pages,
                Price = b.Price,
                CoverImg = b.CoverImg,
                Description = b.Description,
                IsInStock = b.IsInStock
            })
            .ToListAsync();
        }

        public async Task CreateBook(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Genre = createBookDto.Genre,
                Pages = createBookDto.Pages,
                Price = createBookDto.Price,
                CoverImg = createBookDto.CoverImg,
                Description = createBookDto.Description,
                IsInStock = createBookDto.IsInStock
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            var book = await GetBookByIdAsync(id);
            book.Title = updateBookDto.Title;
            book.Author = updateBookDto.Author;
            book.Genre = updateBookDto.Genre;
            book.Pages = updateBookDto.Pages;
            book.Price = updateBookDto.Price;
            book.CoverImg = updateBookDto.CoverImg;
            book.Description = updateBookDto.Description;
            book.IsInStock = updateBookDto.IsInStock;

            await _context.SaveChangesAsync();

            var bookDto = new BookDto
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
        }

        public async Task DeleteBookAsync(int id)
        {

            var book = await GetBookByIdAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetGenresAsync()
        {
            var genres = await _context.Books
            .Select(b => b.Genre)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();

            return genres;
        }

        public async Task<IEnumerable<BookDto>> FilterBooksAsync(string genre)
        {
            return await _context.Books
            .Where(b => b.Genre == genre)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                Pages = b.Pages,
                Price = b.Price,
                CoverImg = b.CoverImg,
                Description = b.Description,
                IsInStock = b.IsInStock
            })
            .ToListAsync();
        }
    }
}
