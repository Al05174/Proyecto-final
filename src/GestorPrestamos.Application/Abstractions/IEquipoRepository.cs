using GestorPrestamos.Domain.Entities;

namespace GestorPrestamos.Application.Abstractions;

public interface IEquipoRepository
{
    Task<IReadOnlyList<Equipo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Equipo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Equipo equipo, CancellationToken cancellationToken = default);
    void Update(Equipo equipo);
}
