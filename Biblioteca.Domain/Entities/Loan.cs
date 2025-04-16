namespace Biblioteca.Domain.Entities
{
    public class Loan : BaseEntity
    {
        public Loan(Book book, string username, DateTime loanDate, DateTime returnDate, decimal loanValue)
        {
            Username = username;
            LoanDate = loanDate;
            ReturnDate = returnDate;
            LoanValue = loanValue;
            
            BookId = book.Id;
            Book = book;
        }

        public string Username { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public decimal LoanValue { get; private set; }
        public int BookId { get; private set; }

        public Book? Book { get; private set; }
    }
}
