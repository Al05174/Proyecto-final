namespace GestorPrestamos.Application.DTOs;

public sealed record CreateAccesorioRequest(
    string CodigoInventario,
    string Nombre,
    string Marca,
    string Ubicacion,
    string Categoria,
    string Compatibilidad);
