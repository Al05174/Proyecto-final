namespace GestorPrestamos.Application.DTOs;

public sealed record CreateCamaraRequest(
    string CodigoInventario,
    string Nombre,
    string Marca,
    string Ubicacion,
    string Resolucion,
    string TipoLente);
