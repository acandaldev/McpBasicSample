using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace mcpTest.Server;

public static class McpServerHost
{
    /// <summary>
    /// Arranca el servidor MCP con transporte Stdio.
    /// Se ejecuta cuando el programa recibe el argumento "--server".
    /// </summary>
    public static async Task RunAsync(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Logging.AddConsole(options =>
        {
            // Enviar logs a stderr para no interferir con el protocolo MCP en stdout
            options.LogToStandardErrorThreshold = LogLevel.Trace;
        });

        builder.Services
            .AddMcpServer(options =>
            {
                options.ServerInfo = new()
                {
                    Name = "mcpTest-server",
                    Version = "1.0.0"
                };
            })
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        await builder.Build().RunAsync();
    }
}
