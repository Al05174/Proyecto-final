using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Application.DTOs;
using GestorPrestamos.Domain.Entities;

namespace GestorPrestamos.Application.Services;

public sealed class PrestamoService
{
    private readonly IPrestamoRepository _prestamoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEquipoRepository _equipoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PrestamoService(
        IPrestamoRepository prestamoRepository,
        IUsuarioRepository usuarioRepository,
        IEquipoRepository equipoRepository,
        IUnitOfWork unitOfWork)
    {
        _prestamoRepository = prestamoRepository;
        _usuarioRepository = usuarioRepository;
        _equipoRepository = equipoRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<Prestamo>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => _prestamoRepository.GetAllAsync(cancellationToken);

    public Task<Prestamo> CrearAsync(int usuarioId, int equipoId, DateTime fechaDevolucionEstimada, CancellationToken cancellationToken = default)
        => CrearAsync(new CreatePrestamoRequest(usuarioId, equipoId, fechaDevolucionEstimada, null), cancellationToken);

    public async Task<Prestamo> CrearAsync(CreatePrestamoRequest request, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.UsuarioId, cancellationToken)
            ?? throw new InvalidOperationException("El usuario no existe.");

        var equipo = await _equipoRepository.GetByIdAsync(request.EquipoId, cancellationToken)
            ?? throw new InvalidOperationException("El equipo no existe.");

        if (!equipo.Disponible)
        {
            throw new InvalidOperationException("El equipo no está disponible para préstamo.");
        }

        var prestamo = new Prestamo(usuario.Id, equipo.Id, request.FechaDevolucionEstimada);
        prestamo.RegistrarEntrega(request.ObservacionesEntrega);
        equipo.MarcarNoDisponible("Equipo entregado al usuario.");

        await _prestamoRepository.AddAsync(prestamo, cancellationToken);
        _equipoRepository.Update(equipo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return prestamo;
    }

    public async Task<Prestamo> RegistrarDevolucionAsync(
        int prestamoId,
        RegistrarDevolucionRequest request,
        CancellationToken cancellationToken = default)
    {
        var prestamo = await _prestamoRepository.GetByIdAsync(prestamoId, cancellationToken)
            ?? throw new InvalidOperationException("El préstamo no existe.");

        var equipo = await _equipoRepository.GetByIdAsync(prestamo.EquipoId, cancellationToken)
            ?? throw new InvalidOperationException("El equipo asociado no existe.");

        if (request.DanioReportado)
        {
            prestamo.RegistrarDevolucion(
                request.FechaDevolucionReal,
                request.ObservacionesDevolucion,
                true,
                request.CostoDanio);

            equipo.RegistrarDanio(
                request.ObservacionesDevolucion ?? "Equipo devuelto con daños.",
                request.CostoDanio);
        }
        else
        {
            prestamo.RegistrarDevolucion(request.FechaDevolucionReal, request.ObservacionesDevolucion);
            equipo.MarcarDisponible();
        }

        _prestamoRepository.Update(prestamo);
        _equipoRepository.Update(equipo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return prestamo;
    }
}
