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

        //AddScoped makes it injectable...
        services.AddScoped<ITokenService, TokenService>(); //AddTransient, AddScoped, Add Singleton
        services.AddScoped<IUserRepository, UserRepository>(); // 
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //single project...
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));



        return services;
    }
}
