using GestorPrestamos.Application.Abstractions;

namespace GestorPrestamos.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly GestorPrestamosDbContext _context;

    public UnitOfWork(GestorPrestamosDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
