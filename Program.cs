using mcpTest.Server;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

// Si se pasa "--server", arrancamos en modo servidor MCP
if (args.Contains("--server"))
{
    await McpServerHost.RunAsync(args);
    return;
}

// --- Modo Cliente MCP ---
Console.WriteLine("=== Cliente MCP ===");
Console.WriteLine();

// 1. Conectar al servidor (lanzándolo como proceso hijo con --server)
var exePath = Environment.ProcessPath!;
var clientTransport = new StdioClientTransport(new StdioClientTransportOptions
{
    Name = "mcpTest-client",
    Command = exePath,
    Arguments = ["--server"],
});

await using var client = await McpClient.CreateAsync(clientTransport);
Console.WriteLine("Conectado al servidor MCP.");
Console.WriteLine();

// 2. Listar herramientas disponibles
var tools = await client.ListToolsAsync();
Console.WriteLine("Herramientas disponibles:");
foreach (var tool in tools)
{
    Console.WriteLine($"  - {tool.Name}: {tool.Description}");
}
Console.WriteLine();

// 3. Invocar echo
Console.WriteLine("--- Invocando: echo ---");
var echoResult = await client.CallToolAsync(
    "echo",
    new Dictionary<string, object?> { ["message"] = "Hola desde el cliente MCP" });
Console.WriteLine($"Resultado: {echoResult.Content.OfType<TextContentBlock>().FirstOrDefault()?.Text}");
Console.WriteLine();

// 4. Invocar get_datetime
Console.WriteLine("--- Invocando: get_datetime ---");
var dateResult = await client.CallToolAsync(
    "get_datetime",
    new Dictionary<string, object?>());
Console.WriteLine($"Resultado: {dateResult.Content.OfType<TextContentBlock>().FirstOrDefault()?.Text}");
Console.WriteLine();

// 5. Invocar calculator: add 10 + 5
Console.WriteLine("--- Invocando: calculator (add 10 + 5) ---");
var addResult = await client.CallToolAsync(
    "calculator",
    new Dictionary<string, object?> { ["operation"] = "add", ["a"] = 10, ["b"] = 5 });
Console.WriteLine($"Resultado: {addResult.Content.OfType<TextContentBlock>().FirstOrDefault()?.Text}");
Console.WriteLine();

// 6. Invocar calculator: divide 10 / 0 (probar manejo de errores)
Console.WriteLine("--- Invocando: calculator (divide 10 / 0) ---");
try
{
    var divResult = await client.CallToolAsync(
        "calculator",
        new Dictionary<string, object?> { ["operation"] = "divide", ["a"] = 10, ["b"] = 0 });

    // El servidor puede devolver el error como contenido con IsError = true
    if (divResult.IsError == true)
    {
        Console.WriteLine($"Error controlado: {divResult.Content.OfType<TextContentBlock>().FirstOrDefault()?.Text}");
    }
    else
    {
        Console.WriteLine($"Resultado: {divResult.Content.OfType<TextContentBlock>().FirstOrDefault()?.Text}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error controlado: {ex.Message}");
}
Console.WriteLine();

Console.WriteLine("=== Fin del cliente MCP ===");
