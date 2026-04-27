namespace GestorPrestamos.Application.DTOs;

public sealed record RegistrarDevolucionRequest(
    DateTime FechaDevolucionReal,
    string? ObservacionesDevolucion,
    bool DanioReportado,
    decimal CostoDanio);
