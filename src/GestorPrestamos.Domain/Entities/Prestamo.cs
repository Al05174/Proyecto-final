using GestorPrestamos.Domain.Enums;

namespace GestorPrestamos.Domain.Entities;

public sealed class Prestamo
{
    private Prestamo()
    {
    }

    public Prestamo(int usuarioId, int equipoId, DateTime fechaDevolucionEstimada)
        : this(usuarioId, equipoId, DateTime.Now, fechaDevolucionEstimada)
    {
    }

    public Prestamo(int usuarioId, int equipoId, DateTime fechaPrestamo, DateTime fechaDevolucionEstimada)
    {
        UsuarioId = usuarioId;
        EquipoId = equipoId;
        FechaPrestamo = fechaPrestamo;
        FechaDevolucionEstimada = fechaDevolucionEstimada;
        Estado = EstadoPrestamo.Activo;
    }

    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public int EquipoId { get; private set; }
    public DateTime FechaPrestamo { get; private set; }
    public DateTime FechaDevolucionEstimada { get; private set; }
    public DateTime? FechaDevolucionReal { get; private set; }
    public EstadoPrestamo Estado { get; private set; }
    public string? ObservacionesEntrega { get; private set; }
    public string? ObservacionesDevolucion { get; private set; }
    public bool DanioReportado { get; private set; }
    public decimal CostoDanio { get; private set; }

    public Usuario? Usuario { get; private set; }
    public Equipo? Equipo { get; private set; }

    public void RegistrarEntrega(string? observaciones)
    {
        ObservacionesEntrega = observaciones;
        Estado = EstadoPrestamo.Activo;
    }

    public void RegistrarDevolucion(DateTime fechaDevolucionReal, string? observaciones)
    {
        RegistrarDevolucion(fechaDevolucionReal, observaciones, false, 0m);
    }

    public void RegistrarDevolucion(
        DateTime fechaDevolucionReal,
        string? observaciones,
        bool danioReportado,
        decimal costoDanio)
    {
        FechaDevolucionReal = fechaDevolucionReal;
        ObservacionesDevolucion = observaciones;
        DanioReportado = danioReportado;
        CostoDanio = costoDanio;
        Estado = fechaDevolucionReal.Date > FechaDevolucionEstimada.Date
            ? EstadoPrestamo.Atrasado
            : EstadoPrestamo.Devuelto;
    }
}
