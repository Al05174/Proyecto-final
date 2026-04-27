using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Domain.Entities;
using GestorPrestamos.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GestorPrestamos.Infrastructure.Persistence;

public sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly GestorPrestamosDbContext _context;

    public DatabaseInitializer(GestorPrestamosDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.EnsureCreatedAsync(cancellationToken);

        if (await _context.Usuarios.AnyAsync(cancellationToken))
        {
            return;
        }

        var usuarios = new[]
        {
            new Usuario("Albert Rafael Hernndez Osoria", "2024-0001", TipoUsuario.Estudiante, "albert@example.com"),
            new Usuario("Mariela Santos", "DOC-110", TipoUsuario.Docente, "mariela@example.com"),
            new Usuario("Carlos Peña", "ADM-205", TipoUsuario.Administrativo)
        };

        var equipos = new Equipo[]
        {
            new Camara("CAM-001", "Camara Canon EOS", "Canon", "Almacen A", "4K", "Teleobjetivo"),
            new Camara("CAM-002", "Camara Sony Alpha", "Sony", "Almacen A", "Full HD", "Gran angular"),
            new Proyector("PRO-001", "Proyector Epson", "Epson", "Salon Multimedia", 3200, "1920x1080"),
            new Accesorio("ACC-001", "Tripode profesional", "Manfrotto", "Almacen B", "Soporte", "Camara y luces"),
            new Accesorio("ACC-002", "Microfono de solapa", "Boya", "Almacen B", "Audio", "Camara y smartphone")
        };

        await _context.Usuarios.AddRangeAsync(usuarios, cancellationToken);
        await _context.Equipos.AddRangeAsync(equipos, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
