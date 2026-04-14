using ModelContextProtocol.Server;
using System.ComponentModel;

namespace mcpTest.Tools;

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Devuelve el mismo texto que recibe. Útil para verificar que la comunicación funciona.")]
    public static string Echo(
        [Description("El texto a devolver")] string message)
    {
        return message;
    }
}
