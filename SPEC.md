# mcpTest - Especificación (Spec-Driven Development)

## Objetivo
Proyecto de aprendizaje para entender el protocolo MCP (Model Context Protocol).
Se implementará un **servidor** y un **cliente** MCP en C# / .NET 8.0.

---

## 1. Servidor MCP

### 1.1 Transporte
- Stdio (entrada/salida estándar) para comunicación entre cliente y servidor.

### 1.2 Herramientas expuestas

#### Tool: `echo`
- **Descripción**: Devuelve el mismo texto que recibe. Útil para verificar que la comunicación funciona.
- **Parámetros**:
  - `message` (string, requerido): El texto a devolver.
- **Retorno**: El mismo texto recibido en `message`.
- **Ejemplo**:
  - Input: `{ "message": "hola mundo" }`
  - Output: `"hola mundo"`

#### Tool: `get_datetime`
- **Descripción**: Devuelve la fecha y hora actual del servidor.
- **Parámetros**: Ninguno.
- **Retorno**: Fecha y hora en formato ISO 8601 (ej: `2026-04-14T10:30:00`).

#### Tool: `calculator`
- **Descripción**: Realiza operaciones matemáticas básicas.
- **Parámetros**:
  - `operation` (string, requerido): Una de: `add`, `subtract`, `multiply`, `divide`.
  - `a` (number, requerido): Primer operando.
  - `b` (number, requerido): Segundo operando.
- **Retorno**: El resultado de la operación como número.
- **Errores**: Si `operation` es `divide` y `b` es 0, devolver error "División por cero no permitida".

### 1.3 Información del servidor
- **Nombre**: `mcpTest-server`
- **Versión**: `1.0.0`

---

## 2. Cliente MCP

### 2.1 Comportamiento
- Se conecta al servidor MCP vía Stdio (lanzando el proceso del servidor).
- Lista las herramientas disponibles en el servidor.
- Invoca cada herramienta con parámetros de ejemplo.
- Imprime los resultados en consola.

### 2.2 Flujo de ejecución
1. Inicia el servidor como proceso hijo.
2. Se conecta al servidor vía MCP/Stdio.
3. Llama `tools/list` para obtener herramientas disponibles e imprime la lista.
4. Invoca `echo` con message: "Hola desde el cliente MCP".
5. Invoca `get_datetime` sin parámetros.
6. Invoca `calculator` con operation: "add", a: 10, b: 5.
7. Invoca `calculator` con operation: "divide", a: 10, b: 0 (para probar manejo de errores).
8. Imprime todos los resultados.
9. Cierra la conexión.

---

## 3. Estructura de archivos esperada

```
mcpTest/
├── mcpTest.csproj
├── SPEC.md              # Esta especificación
├── Program.cs           # Cliente MCP (punto de entrada)
├── Tools/
│   ├── EchoTool.cs      # Herramienta echo
│   ├── DateTimeTool.cs   # Herramienta get_datetime
│   └── CalculatorTool.cs # Herramienta calculator
└── Server/
    └── McpServer.cs      # Configuración y arranque del servidor
```

---

## 4. Criterios de aceptación

- [ ] El servidor arranca sin errores y acepta conexiones por Stdio.
- [ ] `echo` devuelve exactamente el mensaje recibido.
- [ ] `get_datetime` devuelve una fecha válida en formato ISO 8601.
- [ ] `calculator` realiza las 4 operaciones correctamente.
- [ ] `calculator` devuelve error controlado al dividir por cero.
- [ ] El cliente se conecta, lista herramientas e invoca cada una.
- [ ] El cliente imprime resultados legibles en consola.
- [ ] El cliente maneja correctamente el error de división por cero.
