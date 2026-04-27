using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Application.DTOs;
using GestorPrestamos.Domain.Entities;

namespace GestorPrestamos.Application.Services;

public sealed class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<Usuario>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => _usuarioRepository.GetAllAsync(cancellationToken);

    public async Task<Usuario> CrearAsync(CreateUsuarioRequest request, CancellationToken cancellationToken = default)
    {
        var usuario = string.IsNullOrWhiteSpace(request.Correo)
            ? new Usuario(request.NombreCompleto, request.Matricula, request.TipoUsuario)
            : new Usuario(request.NombreCompleto, request.Matricula, request.TipoUsuario, request.Correo);

        await _usuarioRepository.AddAsync(usuario, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return usuario;
    }
}
