using GestorPrestamos.Domain.Enums;

namespace GestorPrestamos.Domain.Entities;

public abstract class Equipo
{
    protected Equipo()
    {
    }

    protected Equipo(string codigoInventario, string nombre, string marca, string ubicacion)
    {
        CodigoInventario = codigoInventario;
        Nombre = nombre;
        Marca = marca;
        Ubicacion = ubicacion;
        Disponible = true;
        Estado = EstadoEquipo.Disponible;
    }

    public int Id { get; private set; }
    public string CodigoInventario { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Marca { get; private set; } = string.Empty;
    public string Ubicacion { get; private set; } = string.Empty;
    public bool Disponible { get; private set; }
    public EstadoEquipo Estado { get; private set; }
    public string? ObservacionEstado { get; private set; }

    public void ActualizarUbicacion(string nuevaUbicacion)
    {
        Ubicacion = nuevaUbicacion;
    }

    public void MarcarDisponible()
    {
        Disponible = true;
        Estado = EstadoEquipo.Disponible;
        ObservacionEstado = "Equipo disponible para préstamo.";
    }

    public void MarcarNoDisponible(string? motivo = null)
    {
        Disponible = false;
        Estado = EstadoEquipo.Prestado;
        ObservacionEstado = motivo ?? "Equipo entregado en préstamo.";
    }

    public void RegistrarDanio(string descripcion)
    {
        RegistrarDanio(descripcion, 0m);
    }

    public virtual void RegistrarDanio(string descripcion, decimal costoEstimado)
    {
        Disponible = false;
        Estado = EstadoEquipo.EnMantenimiento;
        ObservacionEstado = costoEstimado > 0
            ? $"{descripcion}. Costo estimado de reparación: {costoEstimado:C2}."
            : descripcion;
    }

    public abstract string ObtenerTipoEquipo();
}
