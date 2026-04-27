using System.Net.Http.Json;
using System.Text.Json;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5007")
};

var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    WriteIndented = true
};

Console.WriteLine("Gestor de Prestamo de Equipos Audiovisuales");
Console.WriteLine("Cliente distribuido de consola");
Console.WriteLine("-------------------------------------------");

var continuar = true;

while (continuar)
{
    Console.WriteLine();
    Console.WriteLine("1. Ver equipos");
    Console.WriteLine("2. Ver usuarios");
    Console.WriteLine("3. Crear prestamo");
    Console.WriteLine("4. Ver prestamos");
    Console.WriteLine("5. Registrar devolucion");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opcion: ");

    var opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            await MostrarAsync("/api/equipos");
            break;
        case "2":
            await MostrarAsync("/api/usuarios");
            break;
        case "3":
            await CrearPrestamoAsync();
            break;
        case "4":
            await MostrarAsync("/api/prestamos");
            break;
        case "5":
            await RegistrarDevolucionAsync();
            break;
        case "0":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opcion invalida.");
            break;
    }
}

async Task MostrarAsync(string ruta)
{
    try
    {
        var contenido = await httpClient.GetStringAsync(ruta);
        Console.WriteLine(JsonSerializer.Serialize(
            JsonSerializer.Deserialize<object>(contenido, jsonOptions),
            jsonOptions));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"No fue posible consultar la API: {ex.Message}");
    }
}

async Task CrearPrestamoAsync()
{
    try
    {
        Console.Write("Id del usuario: ");
        var usuarioId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Id del equipo: ");
        var equipoId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Dias de prestamo: ");
        var dias = int.Parse(Console.ReadLine() ?? "1");

        Console.Write("Observaciones de entrega: ");
        var observaciones = Console.ReadLine();

        var request = new CrearPrestamoApiRequest(
            usuarioId,
            equipoId,
            DateTime.Now.AddDays(dias),
            observaciones);

        var response = await httpClient.PostAsJsonAsync("/api/prestamos", request);
        var contenido = await response.Content.ReadAsStringAsync();
        Console.WriteLine(contenido);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"No fue posible crear el prestamo: {ex.Message}");
    }
}

async Task RegistrarDevolucionAsync()
{
    try
    {
        Console.Write("Id del prestamo: ");
        var prestamoId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Observaciones de devolucion: ");
        var observaciones = Console.ReadLine();

        Console.Write("Tiene danos? (s/n): ");
        var tieneDanio = string.Equals(Console.ReadLine(), "s", StringComparison.OrdinalIgnoreCase);

        decimal costoDanio = 0m;
        if (tieneDanio)
        {
            Console.Write("Costo estimado del dano: ");
            costoDanio = decimal.Parse(Console.ReadLine() ?? "0");
        }

        var request = new RegistrarDevolucionApiRequest(
            DateTime.Now,
            observaciones,
            tieneDanio,
            costoDanio);

        var response = await httpClient.PostAsJsonAsync($"/api/prestamos/{prestamoId}/devolucion", request);
        var contenido = await response.Content.ReadAsStringAsync();
        Console.WriteLine(contenido);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"No fue posible registrar la devolucion: {ex.Message}");
    }
}

internal sealed record CrearPrestamoApiRequest(
    int UsuarioId,
    int EquipoId,
    DateTime FechaDevolucionEstimada,
    string? ObservacionesEntrega);

internal sealed record RegistrarDevolucionApiRequest(
    DateTime FechaDevolucionReal,
    string? ObservacionesDevolucion,
    bool DanioReportado,
    decimal CostoDanio);
