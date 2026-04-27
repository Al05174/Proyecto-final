namespace GestorPrestamos.Domain.Entities;

public sealed class Proyector : Equipo
{
    private Proyector()
    {
    }

    public Proyector(
        string codigoInventario,
        string nombre,
        string marca,
        string ubicacion,
        int lumens,
        string resolucionNativa)
        : base(codigoInventario, nombre, marca, ubicacion)
    {
        Lumens = lumens;
        ResolucionNativa = resolucionNativa;
    }

    public int Lumens { get; private set; }
    public string ResolucionNativa { get; private set; } = string.Empty;

    public override string ObtenerTipoEquipo() => "Proyector";
}
