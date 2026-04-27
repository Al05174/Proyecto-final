namespace GestorPrestamos.Domain.Entities;

public sealed class Camara : Equipo
{
    private Camara()
    {
    }

    public Camara(
        string codigoInventario,
        string nombre,
        string marca,
        string ubicacion,
        string resolucion,
        string tipoLente)
        : base(codigoInventario, nombre, marca, ubicacion)
    {
        Resolucion = resolucion;
        TipoLente = tipoLente;
    }

    public string Resolucion { get; private set; } = string.Empty;
    public string TipoLente { get; private set; } = string.Empty;

    public override string ObtenerTipoEquipo() => "Camara";
}
