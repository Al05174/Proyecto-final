using GestorPrestamos.Domain.Enums;

namespace GestorPrestamos.Domain.Entities;

public sealed class Usuario
{
    private Usuario()
    {
    }

    public Usuario(string nombreCompleto, string matricula, TipoUsuario tipoUsuario)
    {
        NombreCompleto = nombreCompleto;
        Matricula = matricula;
        TipoUsuario = tipoUsuario;
    }

    public Usuario(string nombreCompleto, string matricula, TipoUsuario tipoUsuario, string correo)
        : this(nombreCompleto, matricula, tipoUsuario)
    {
        Correo = correo;
    }

    public int Id { get; private set; }
    public string NombreCompleto { get; private set; } = string.Empty;
    public string Matricula { get; private set; } = string.Empty;
    public string? Correo { get; private set; }
    public TipoUsuario TipoUsuario { get; private set; }

    public ICollection<Prestamo> Prestamos { get; private set; } = new List<Prestamo>();
}
