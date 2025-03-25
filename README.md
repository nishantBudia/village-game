# Village Game

A 2D tower defense game built with MonoGame and .NET 9.0, targeting Android and iOS platforms.

## Project Overview

Village Game is a 2D tower defense game where players strategically place defensive structures to protect their territory from waves of enemies.

## Development Setup

### Prerequisites
- .NET 9.0 SDK
- MonoGame 3.8.2
- For macOS with Apple Silicon: Rosetta 2

### Building and Running

#### Windows/Linux
```bash
dotnet build
dotnet run --project src/VillageGame.Game
```

#### macOS with Apple Silicon
```bash
# Use the provided scripts
./build-macos.sh
./run-macos.sh
```

See [BUILD_MACOS.md](docs/BUILD_MACOS.md) for detailed instructions on setting up for Apple Silicon.

## Documentation

Comprehensive project documentation is available in the `docs` directory:

- [Project Context](docs/context/MAIN_CONTEXT.md)
- [Content Pipeline](docs/context/features/content-pipeline/CONTEXT.md)
- [Architecture](docs/context/features/content-pipeline/ARCHITECTURE.md)
- [Technical Decisions](docs/context/features/content-pipeline/TECHNICAL_DECISIONS.md)
- [Dependencies](docs/context/features/content-pipeline/DEPENDENCIES.md)

## License

This project is licensed under the MIT License - see the LICENSE file for details. 