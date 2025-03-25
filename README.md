# Village Game - 2D Tower Defense

A cross-platform 2D tower defense game built with MonoGame, targeting Android and iOS platforms.

## Project Overview

This game is built using MonoGame and follows clean architecture principles with a modular design. The project is structured to support future integrations including cloud storage, analytics, and monetization features.

### Architecture

The project follows a clean, modular architecture with the following key components:

- **Global Context**: Centralized configuration and service management
- **Game World**: Core game mechanics and world management
- **Data Layer**: Abstracted data storage with support for local and cloud persistence
- **Analytics**: User behavior tracking and metrics collection
- **Monetization**: Framework for ads and in-app purchases

For detailed architecture information, see the [Architecture Documentation](docs/Architecture.md).

### Model Context Protocol

The project implements the Model Context Protocol pattern to organize code into distinct contexts with clear boundaries and responsibilities. See the [Model Context Protocol Documentation](docs/ModelContextProtocol.md) for details.

### Code Examples

For practical examples of how to use the architecture in common scenarios, see the [Code Examples](docs/CodeExamples.md) document.

### Directory Structure

```
VillageGame/
├── src/
│   ├── VillageGame.Core/           # Core game logic and interfaces
│   ├── VillageGame.Infrastructure/ # Implementation of core interfaces
│   ├── VillageGame.Game/           # Main game project
│   └── VillageGame.Tests/          # Unit tests
├── docs/                          # Documentation
└── tools/                         # Build and development tools
```

## Setup Instructions

### Prerequisites

- .NET 9.0 SDK or later
- MonoGame Framework
- Visual Studio 2022 or JetBrains Rider
- Git

### Building the Project

1. Clone the repository:
   ```bash
   git clone [repository-url]
   cd village_game
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

### Running Tests

```bash
dotnet test
```

## Development Guidelines

See the [Code Standards and Development Guidelines](docs/CodeStandards.md) for detailed information about coding standards, practices, and development guidelines.

### Development Roadmap

For information about planned features and future development, see the [Development Roadmap](docs/DevelopmentRoadmap.md).

## Future Roadmap

### Planned Features

- Cloud storage integration for game saves
- Analytics integration for user behavior tracking
- Ad network integration
- In-app purchase system
- Social features and leaderboards

### Monetization Strategy

- Free-to-play model with optional premium features
- In-app purchases for cosmetic items and power-ups
- Optional ad-supported gameplay
- Premium ad-free version

## License

[License details to be added]

## Contributing

[Contribution guidelines to be added] 