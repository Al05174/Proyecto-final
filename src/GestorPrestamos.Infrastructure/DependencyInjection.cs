using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Infrastructure.Persistence;
using GestorPrestamos.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestorPrestamos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GestorPrestamosDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IEquipoRepository, EquipoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPrestamoRepository, PrestamoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        return services;
    }
}
