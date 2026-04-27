namespace GestorPrestamos.Application.DTOs;

public sealed record CreateProyectorRequest(
    string CodigoInventario,
    string Nombre,
    string Marca,
    string Ubicacion,
    int Lumens,
    string ResolucionNativa);
