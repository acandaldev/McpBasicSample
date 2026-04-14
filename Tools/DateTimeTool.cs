using ModelContextProtocol.Server;
using System.ComponentModel;

namespace mcpTest.Tools;

[McpServerToolType]
public static class DateTimeTool
{
    [McpServerTool, Description("Devuelve la fecha y hora actual del servidor en formato ISO 8601.")]
    public static string GetDatetime()
    {
        return DateTime.Now.ToString("o");
    }
}
