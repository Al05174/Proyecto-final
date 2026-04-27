using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPrestamos.Infrastructure.Persistence.Repositories;

public sealed class PrestamoRepository : IPrestamoRepository
{
    private readonly GestorPrestamosDbContext _context;

    public PrestamoRepository(GestorPrestamosDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Prestamo>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Equipo)
            .AsNoTracking()
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync(cancellationToken);

    public Task<Prestamo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Equipo)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task AddAsync(Prestamo prestamo, CancellationToken cancellationToken = default)
        => _context.Prestamos.AddAsync(prestamo, cancellationToken).AsTask();

    public void Update(Prestamo prestamo)
    {
        _context.Prestamos.Update(prestamo);
    }
}
