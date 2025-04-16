using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces.Services;

namespace Biblioteca.API.Configurations
{
    public static class GeneralConfiguration
    {
        public static void AddGeneralConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILibraryService, LibraryService>();
        }
    }
}
