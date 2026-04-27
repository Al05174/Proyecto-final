namespace GestorPrestamos.Domain.Entities;

public sealed class Accesorio : Equipo
{
    private Accesorio()
    {
    }

    public Accesorio(
        string codigoInventario,
        string nombre,
        string marca,
        string ubicacion,
        string categoria,
        string compatibilidad)
        : base(codigoInventario, nombre, marca, ubicacion)
    {
        Categoria = categoria;
        Compatibilidad = compatibilidad;
    }

    public string Categoria { get; private set; } = string.Empty;
    public string Compatibilidad { get; private set; } = string.Empty;

    public override string ObtenerTipoEquipo() => "Accesorio";
}
