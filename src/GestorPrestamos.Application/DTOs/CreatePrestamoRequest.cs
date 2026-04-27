namespace GestorPrestamos.Application.DTOs;

public sealed record CreatePrestamoRequest(
    int UsuarioId,
    int EquipoId,
    DateTime FechaDevolucionEstimada,
    string? ObservacionesEntrega);
