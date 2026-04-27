using GestorPrestamos.Domain.Enums;

namespace GestorPrestamos.Application.DTOs;

public sealed record CreateUsuarioRequest(
    string NombreCompleto,
    string Matricula,
    TipoUsuario TipoUsuario,
    string? Correo);
