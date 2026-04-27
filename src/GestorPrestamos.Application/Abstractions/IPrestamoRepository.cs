using GestorPrestamos.Domain.Entities;

namespace GestorPrestamos.Application.Abstractions;

public interface IPrestamoRepository
{
    Task<IReadOnlyList<Prestamo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Prestamo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Prestamo prestamo, CancellationToken cancellationToken = default);
    void Update(Prestamo prestamo);
}
