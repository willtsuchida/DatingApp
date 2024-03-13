using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

        //Habilitando CORS
        services.AddCors(); // Continuar no http pipeline, usar na ordem correta

        services.AddScoped<ITokenService, TokenService>(); //AddTransient, AddScoped, Add Singleton

        return services;
    }
}
