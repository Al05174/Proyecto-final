using GestorPrestamos.Domain.Entities;

namespace GestorPrestamos.Application.Abstractions;

public interface IUsuarioRepository
{
    Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Usuario?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default);
}
