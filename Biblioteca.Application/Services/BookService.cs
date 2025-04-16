using Biblioteca.Domain.Dtos.Book;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enumerators;
using Biblioteca.Domain.Interfaces.Services;
using Biblioteca.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationContext _applicationContext;

        public BookService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> CreateBook(CreateBook createBook)
        {
            if(string.IsNullOrWhiteSpace(createBook.Title) || createBook.PublicationYear is null || createBook.Authors.Count <= 0)
            {
                throw new ArgumentException("Dados invalidos");
            }

            if(!Enum.TryParse<Category>(createBook.Category, out var category))
            {
                throw new ArgumentException("Categoria invalida");
            }

            List<Author> authors = [];
            foreach(var author in createBook.Authors)
            {
                var authorInDatabase = await _applicationContext.Authors.FirstOrDefaultAsync(x => x.Name == author);

                if (authorInDatabase is not null)
                {
                    authors.Add(authorInDatabase);
                    continue;
                }

                authorInDatabase = new Author(author, []);
                authors.Add(authorInDatabase);
            }

            var book = new Book(createBook.Title!, createBook.PublicationYear!.Value, category, authors!);

            var result = await _applicationContext.AddAsync(book);
            await _applicationContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateBook(int id, UpdateBook updateBook)
        {
            var book = await _applicationContext.Books.FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new ArgumentException("Livro não encontrado");

            if (string.IsNullOrWhiteSpace(updateBook.Title) || updateBook.PublicationYear is null || updateBook.Authors.Count <= 0)
            {
                throw new ArgumentException("Dados invalidos");
            }

            if (!Enum.TryParse<Category>(updateBook.Category, out var category))
            {
                throw new ArgumentException("Categoria invalida");
            }

            List<Author> authors = [];
            foreach (var author in updateBook.Authors)
            {
                var authorInDatabase = await _applicationContext.Authors.FirstOrDefaultAsync(x => x.Name == author);

                if (authorInDatabase is not null)
                {
                    authors.Add(authorInDatabase);
                    continue;
                }

                authorInDatabase = new Author(author, []);
                authors.Add(authorInDatabase);
            }

            book.Update(updateBook.Title, updateBook.PublicationYear!.Value, category, authors);

            _applicationContext.Update(book);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _applicationContext.Books.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ArgumentException("Livro não encontrado");

            _applicationContext.Remove(book);
            await _applicationContext.SaveChangesAsync();
        }

        public Task<List<BookResponse>> GetAllBooks()
        {
            var books = _applicationContext.Books
                .Include(x => x.Authors)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    PublicationYear = x.PublicationYear,
                    Category = x.Category.ToString(),
                    Authors = x.Authors.Select(a => a.Name).ToList()
                }).ToListAsync();

            return books;
        }

        public Task<BookResponse?> GetBook(int id)
        {
            var book = _applicationContext.Books
                .Include(x => x.Authors)
                .Where(x => x.Id == id)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    PublicationYear = x.PublicationYear,
                    Category = x.Category.ToString(),
                    Authors = x.Authors.Select(a => a.Name).ToList()
                })
                .FirstOrDefaultAsync();

            return book;
        }
    }
}
