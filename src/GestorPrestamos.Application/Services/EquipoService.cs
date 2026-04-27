using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Application.DTOs;
using GestorPrestamos.Domain.Entities;

namespace GestorPrestamos.Application.Services;

public sealed class EquipoService
{
    private readonly IEquipoRepository _equipoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EquipoService(IEquipoRepository equipoRepository, IUnitOfWork unitOfWork)
    {
        _equipoRepository = equipoRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<Equipo>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => _equipoRepository.GetAllAsync(cancellationToken);

    public async Task<Camara> CrearCamaraAsync(CreateCamaraRequest request, CancellationToken cancellationToken = default)
    {
        var camara = new Camara(
            request.CodigoInventario,
            request.Nombre,
            request.Marca,
            request.Ubicacion,
            request.Resolucion,
            request.TipoLente);

        await _equipoRepository.AddAsync(camara, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return camara;
    }

    public async Task<Proyector> CrearProyectorAsync(CreateProyectorRequest request, CancellationToken cancellationToken = default)
    {
        var proyector = new Proyector(
            request.CodigoInventario,
            request.Nombre,
            request.Marca,
            request.Ubicacion,
            request.Lumens,
            request.ResolucionNativa);

        await _equipoRepository.AddAsync(proyector, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return proyector;
    }

    public async Task<Accesorio> CrearAccesorioAsync(CreateAccesorioRequest request, CancellationToken cancellationToken = default)
    {
        var accesorio = new Accesorio(
            request.CodigoInventario,
            request.Nombre,
            request.Marca,
            request.Ubicacion,
            request.Categoria,
            request.Compatibilidad);

        await _equipoRepository.AddAsync(accesorio, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return accesorio;
    }
}
