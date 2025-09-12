# FactorySimulator

FactorySimulator is a .NET 8 console application that allows users to simulate factory assembly lines using Lua scripting. The project provides a flexible environment for modeling, testing, and visualizing assembly line logic directly in the console.

## Features

- **Lua Scripting Integration:** Define and control assembly line behavior using Lua scripts.
- **Console Visualization:** View simulation results and assembly line status in the console.
- **Extensible Architecture:** Easily add new components or modify simulation logic.
- **.NET 8 Powered:** Modern, fast, and cross-platform.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- (Optional) [Visual Studio](https://visualstudio.microsoft.com/) or any C# IDE

### Building the Project

1. Clone the repository:
```sh
git clone <repository-url>
   cd FactorySimulator
```

2. Build the solution:
```sh
dotnet build
```

### Running the Simulation

1. Navigate to the `FactorySimulator.Game` directory:
```sh
cd FactorySimulator.Game
```

2. Run the application:
```sh
dotnet run
```

3. Follow the prompts to load or create a Lua script for your assembly line simulation.

## Lua Scripting

You can define your assembly line logic in Lua. Example script:

```lua
-- Example: Simple conveyor simulation
for i = 1,5 do
    f:Miner(0):Smelter():Constructor(r.IronRod):Constructor(r.Screw)
end
```

Refer to the documentation or sample scripts in the repository for more details.

## Project Structure

- `FactorySimulator`: Core simulation logic and Lua integration.
- `FactorySimulator.Game`: Console application entry point.
- `FactorySimulator.Tests`: Unit tests for simulation components.
