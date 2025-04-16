using Biblioteca.Domain.Dtos.Library;
using Biblioteca.Domain.Interfaces.Services;

namespace Biblioteca.Application.Services
{
    public class LibraryService : ILibraryService
    {
        public Task LoanBook(LoanBook loanBook)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoanResponse>> GetLoanByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
