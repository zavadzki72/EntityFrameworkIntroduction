using Biblioteca.Domain.Dtos.Book;
using Biblioteca.Domain.Interfaces.Services;

namespace Biblioteca.Application.Services
{
    public class BookService : IBookService
    {
        public Task<int> CreateBook(CreateBook createBook)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBook(int id, UpdateBook updateBook)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookResponse>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<BookResponse> GetBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}
