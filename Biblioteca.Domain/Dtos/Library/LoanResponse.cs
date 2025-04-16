using Biblioteca.Domain.Dtos.Book;

namespace Biblioteca.Domain.Dtos.Library
{
    public class LoanResponse
    {
        public required string Username { get; set; }
        public required DateTime LoanDate { get; set; }
        public required DateTime ReturnDate { get; set; }
        public required decimal LoanValue { get; set; }
        public required BookResponse Book { get; set; }
    }
}
