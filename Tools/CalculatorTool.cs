using ModelContextProtocol.Server;
using System.ComponentModel;

namespace mcpTest.Tools;

[McpServerToolType]
public static class CalculatorTool
{
    [McpServerTool, Description("Realiza operaciones matemáticas básicas: add, subtract, multiply, divide.")]
    public static string Calculator(
        [Description("Operación a realizar: add, subtract, multiply, divide")] string operation,
        [Description("Primer operando")] double a,
        [Description("Segundo operando")] double b)
    {
        var result = operation.ToLowerInvariant() switch
        {
            "add" => a + b,
            "subtract" => a - b,
            "multiply" => a * b,
            "divide" when b == 0 => throw new ArgumentException("División por cero no permitida"),
            "divide" => a / b,
            _ => throw new ArgumentException($"Operación no soportada: {operation}. Use: add, subtract, multiply, divide")
        };

        return result.ToString();
    }
}
