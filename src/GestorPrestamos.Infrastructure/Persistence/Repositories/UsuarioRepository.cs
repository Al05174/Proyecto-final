using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPrestamos.Infrastructure.Persistence.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly GestorPrestamosDbContext _context;

    public UsuarioRepository(GestorPrestamosDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Usuarios
            .AsNoTracking()
            .OrderBy(u => u.NombreCompleto)
            .ToListAsync(cancellationToken);

    public Task<Usuario?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default)
        => _context.Usuarios.AddAsync(usuario, cancellationToken).AsTask();
}
