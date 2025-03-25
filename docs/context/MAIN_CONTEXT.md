# Village Game Project Context

## Project Overview

The Village Game is a 2D simulation game built using MonoGame and .NET 9.0. Players manage and develop a virtual village, making strategic decisions about resource management, construction, and villager activities.

## Core Features

1. **Content Pipeline**
   - Asset management system
   - Resource loading and optimization
   - Cross-platform compatibility
   - [Details](features/content-pipeline/CONTEXT.md)

2. **Village Management**
   - Resource gathering and allocation
   - Building construction and placement
   - Population growth and management
   - [Details](features/village-management/CONTEXT.md)

3. **Game World**
   - 2D tile-based environment
   - Dynamic weather system
   - Day/night cycle
   - [Details](features/game-world/CONTEXT.md)

4. **Character System**
   - Villager AI behaviors
   - Skills and attributes
   - Job assignments
   - [Details](features/character-system/CONTEXT.md)

5. **User Interface**
   - Intuitive controls
   - Resource indicators
   - Building menus
   - [Details](features/user-interface/CONTEXT.md)

## Architecture

### Project Structure
```
VillageGame/
├── src/
│   ├── VillageGame.Core/
│   ├── VillageGame.Infrastructure/
│   ├── VillageGame.Game/
│   └── VillageGame.Tests/
├── docs/
│   └── context/
└── tools/
```

### Layer Responsibilities

1. **Core Layer (VillageGame.Core)**
   - Domain models
   - Business logic
   - Interfaces
   - Value objects

2. **Infrastructure Layer (VillageGame.Infrastructure)**
   - Data persistence
   - External services
   - Cross-cutting concerns
   - Technical implementations

3. **Game Layer (VillageGame.Game)**
   - Game loop
   - Rendering
   - Input handling
   - Asset management

4. **Tests Layer (VillageGame.Tests)**
   - Unit tests
   - Integration tests
   - Performance tests
   - Behavior tests

## Technical Stack

### Core Technologies
- .NET 9.0
- MonoGame 3.8.1
- C# 12

### Development Tools
- Visual Studio/Rider
- Git
- MSBuild
- MonoGame Pipeline Tool

### Testing Framework
- xUnit
- Moq
- FluentAssertions

## Development Practices

### Code Quality
- Clean Architecture principles
- SOLID design
- Code reviews
- Static analysis

### Testing Strategy
- TDD approach
- Automated testing
- Performance benchmarking
- Coverage targets

### Documentation
- XML documentation
- Architecture decision records
- Feature contexts
- API documentation

## Platform-Specific Considerations

### macOS with Apple Silicon
- MonoGame content pipeline requires Rosetta 2 for compatibility
- Build and run scripts (build-macos.sh, run-macos.sh) provided for easy development
- See [BUILD_MACOS.md](../BUILD_MACOS.md) for detailed instructions
- Primary development target is Android/iOS

## Project Goals

### Short Term
1. Implement basic village management
2. Establish core game mechanics
3. Create initial content pipeline
4. Develop basic UI system

### Medium Term
1. Add advanced features
2. Optimize performance
3. Enhance graphics
4. Implement save/load system

### Long Term
1. Cross-platform support
2. Modding capabilities
3. Multiplayer features
4. Community tools

## Challenges and Solutions

### Technical Challenges
1. **Performance Optimization**
   - Efficient rendering
   - Memory management
   - Asset loading

2. **Cross-Platform Compatibility**
   - Platform-specific code
   - Asset management
   - Input handling

3. **Scalability**
   - Modular design
   - Extensible systems
   - Performance monitoring

### Solutions Implemented
1. **Performance**
   - Asset pooling
   - Lazy loading
   - Caching strategies

2. **Compatibility**
   - Platform abstraction
   - Conditional compilation
   - Unified asset pipeline

3. **Scalability**
   - Microservices architecture
   - Event-driven design
   - Modular components

## Future Roadmap

### Version 1.0
- Basic village management
- Core game mechanics
- Essential UI
- Basic content pipeline

### Version 2.0
- Advanced features
- Enhanced graphics
- Performance optimizations
- Save/load system

### Version 3.0
- Cross-platform support
- Modding support
- Multiplayer features
- Community tools

## Contributing

### Guidelines
- Code style guide
- Pull request process
- Testing requirements
- Documentation standards

### Getting Started
1. Setup instructions
2. Development environment
3. Building the project
4. Running tests

## Resources

### Documentation
- API reference
- Architecture guide
- Feature contexts
- User guides

### Tools
- Development tools
- Build scripts
- Testing utilities
- Documentation generators

## Support

### Community
- GitHub discussions
- Discord server
- Bug reporting
- Feature requests

### Maintenance
- Version control
- Release process
- Issue tracking
- Security updates 