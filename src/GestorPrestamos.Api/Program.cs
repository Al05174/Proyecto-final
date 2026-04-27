using GestorPrestamos.Application.Abstractions;
using GestorPrestamos.Application.DTOs;
using GestorPrestamos.Application.Services;
using GestorPrestamos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "server=localhost;port=3306;database=gestor_prestamos_audiovisuales;user=root;password=1234;";

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<EquipoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PrestamoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    await initializer.InitializeAsync();
}

app.MapGet("/", () => Results.Ok(new
{
    proyecto = "Gestor de Prestamo de Equipos Audiovisuales",
    arquitectura = "Distribuida por API + cliente de consola",
    baseDeDatos = "MySQL"
}));

app.MapGet("/api/equipos", async (EquipoService service, CancellationToken cancellationToken) =>
{
    var equipos = await service.ObtenerTodosAsync(cancellationToken);
    return Results.Ok(equipos.Select(e => new
    {
        e.Id,
        e.CodigoInventario,
        e.Nombre,
        e.Marca,
        e.Ubicacion,
        e.Disponible,
        e.Estado,
        e.ObservacionEstado,
        Tipo = e.ObtenerTipoEquipo()
    }));
});

app.MapPost("/api/equipos/camaras", async (CreateCamaraRequest request, EquipoService service, CancellationToken cancellationToken) =>
{
    var camara = await service.CrearCamaraAsync(request, cancellationToken);
    return Results.Created($"/api/equipos/{camara.Id}", camara);
});

app.MapPost("/api/equipos/proyectores", async (CreateProyectorRequest request, EquipoService service, CancellationToken cancellationToken) =>
{
    var proyector = await service.CrearProyectorAsync(request, cancellationToken);
    return Results.Created($"/api/equipos/{proyector.Id}", proyector);
});

app.MapPost("/api/equipos/accesorios", async (CreateAccesorioRequest request, EquipoService service, CancellationToken cancellationToken) =>
{
    var accesorio = await service.CrearAccesorioAsync(request, cancellationToken);
    return Results.Created($"/api/equipos/{accesorio.Id}", accesorio);
});

app.MapGet("/api/usuarios", async (UsuarioService service, CancellationToken cancellationToken) =>
{
    var usuarios = await service.ObtenerTodosAsync(cancellationToken);
    return Results.Ok(usuarios);
});

app.MapPost("/api/usuarios", async (CreateUsuarioRequest request, UsuarioService service, CancellationToken cancellationToken) =>
{
    var usuario = await service.CrearAsync(request, cancellationToken);
    return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
});

app.MapGet("/api/prestamos", async (PrestamoService service, CancellationToken cancellationToken) =>
{
    var prestamos = await service.ObtenerTodosAsync(cancellationToken);
    return Results.Ok(prestamos.Select(p => new
    {
        p.Id,
        p.UsuarioId,
        Usuario = p.Usuario?.NombreCompleto,
        p.EquipoId,
        Equipo = p.Equipo?.Nombre,
        p.FechaPrestamo,
        p.FechaDevolucionEstimada,
        p.FechaDevolucionReal,
        p.Estado,
        p.ObservacionesEntrega,
        p.ObservacionesDevolucion,
        p.DanioReportado,
        p.CostoDanio
    }));
});

app.MapPost("/api/prestamos", async (CreatePrestamoRequest request, PrestamoService service, CancellationToken cancellationToken) =>
{
    try
    {
        var prestamo = await service.CrearAsync(request, cancellationToken);
        return Results.Created($"/api/prestamos/{prestamo.Id}", prestamo);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { mensaje = ex.Message });
    }
});

app.MapPost("/api/prestamos/{id:int}/devolucion", async (
    int id,
    RegistrarDevolucionRequest request,
    PrestamoService service,
    CancellationToken cancellationToken) =>
{
    try
    {
        var prestamo = await service.RegistrarDevolucionAsync(id, request, cancellationToken);
        return Results.Ok(prestamo);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { mensaje = ex.Message });
    }
});

app.Run();
