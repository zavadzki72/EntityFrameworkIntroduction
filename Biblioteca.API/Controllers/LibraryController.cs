using Biblioteca.Domain.Dtos.Library;
using Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost("loan-book")]
        public async Task<IActionResult> LoanBook([FromBody] LoanBook loanBook)
        {
            await _libraryService.LoanBook(loanBook);
            return NoContent();
        }

        [HttpGet("user/{username}/loaned-books")]
        public async Task<IActionResult> LoanedBooksByUser(string username)
        {
            var response = await _libraryService.GetLoanByUsername(username);
            return Ok(response);
        }
    }
}
