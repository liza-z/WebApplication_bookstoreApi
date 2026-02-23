using WebApplication_bookstoreApi.Models.DTOs;
using WebApplication_bookstoreApi.Models.Entities;

namespace WebApplication_bookstoreApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();

        Task<Book?> GetBookByIdAsync(int id);

        Task<IEnumerable<BookDto>> SearchBookAsync(string query);

        Task<IEnumerable<BookDto>> FilterBooksAsync(string query);

        Task<IEnumerable<Book>> GetGenresAsync();

        Task CreateBook(CreateBookDto book);

        Task UpdateBook(int id, UpdateBookDto updateBookDto);

        Task DeleteBookAsync(int id);
    }
}
