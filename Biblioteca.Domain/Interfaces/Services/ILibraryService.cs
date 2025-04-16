using Biblioteca.Domain.Dtos.Library;

namespace Biblioteca.Domain.Interfaces.Services
{
    public interface ILibraryService
    {
        Task LoanBook(LoanBook loanBook);
        Task<List<LoanResponse>> GetLoanByUsername(string username);
    }
}
