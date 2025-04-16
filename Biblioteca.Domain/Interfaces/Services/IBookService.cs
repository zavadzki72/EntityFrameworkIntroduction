using Biblioteca.Domain.Dtos.Book;

namespace Biblioteca.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<int> CreateBook(CreateBook createBook);
        Task UpdateBook(int id, UpdateBook updateBook);
        Task DeleteBook(int id);
        Task<List<BookResponse>> GetAllBooks();
        Task<BookResponse> GetBook(int id);
    }
}
