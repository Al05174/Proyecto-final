using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPrestamos.Infrastructure.Persistence.Repositories;

public sealed class EquipoRepository : IEquipoRepository
{
    private readonly GestorPrestamosDbContext _context;

    public EquipoRepository(GestorPrestamosDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Equipo>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Equipos
            .AsNoTracking()
            .OrderBy(e => e.Nombre)
            .ToListAsync(cancellationToken);

    public Task<Equipo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _context.Equipos.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task AddAsync(Equipo equipo, CancellationToken cancellationToken = default)
        => _context.Equipos.AddAsync(equipo, cancellationToken).AsTask();

    public void Update(Equipo equipo)
    {
        _context.Equipos.Update(equipo);
    }
}
