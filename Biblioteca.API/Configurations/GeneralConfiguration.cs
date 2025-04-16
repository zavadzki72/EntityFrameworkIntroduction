using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces.Services;
using Biblioteca.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Configurations
{
    public static class GeneralConfiguration
    {
        public static void AddGeneralConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILibraryService, LibraryService>();

            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
