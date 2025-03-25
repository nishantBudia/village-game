# Village Game Architecture Documentation

This document provides an overview of the architecture and design principles used in the Village Game project, particularly focusing on the Model Context Protocol implementation.

## Table of Contents

- [Global Context](#global-context)
- [Local Contexts](#local-contexts)
- [Cross-Cutting Concerns](#cross-cutting-concerns)
- [Protocol Rules](#protocol-rules)
- [Future Cloud Integration](#future-cloud-integration)
- [Code Organization](#code-organization)

## Global Context

The Global Context serves as the centralized service locator and configuration hub for the entire application. It provides access to all shared services and ensures they're properly initialized and available throughout the application lifecycle.

### Core Components

1. **IGlobalContext Interface** (`src/VillageGame.Core/Interfaces/IGlobalContext.cs`)
   - Acts as the central access point for all global services
   - Provides properties for accessing Logger, DataStore, Analytics, Monetization, and Config services
   - Includes an Initialize method that bootstraps all services in the correct order

2. **GlobalContext Implementation** (`src/VillageGame.Infrastructure/Services/GlobalContext.cs`)
   - Implements the IGlobalContext interface
   - Manages initialization of all core services
   - Handles dependencies between services (e.g., Analytics depends on Logger and DataStore)

### Global Services

1. **Configuration (IAppConfig)**
   - Manages application-wide settings
   - Provides type-safe access to configuration values
   - Supports loading/saving from JSON files
   - Tracks environment (Development/Production) and version information

2. **Logging (ILoggerService)**
   - Provides structured logging capabilities
   - Supports different log levels (Debug, Information, Warning, Error)
   - Captures user events for analytics purposes
   - Implements file-based logging with rotation support

3. **Data Storage (IDataStore)**
   - Abstracts data persistence operations
   - Supports saving, loading, and deleting data by key
   - Designed for easy replacement with cloud storage in the future
   - Current implementation uses local JSON files with memory caching

4. **Analytics (IAnalyticsService)**
   - Tracks user behavior and application usage
   - Records events, screen views, and user properties
   - Designed to be replaceable with a cloud analytics provider
   - Current implementation stores data locally

5. **Monetization (IMonetizationService)**
   - Manages in-app purchases and advertisements
   - Provides product catalog and purchase history
   - Supports ad loading and display with different ad types
   - Current implementation is a mock for development purposes

## Local Contexts

These modules represent feature-specific functionality that depends on the global context but has their own internal state and behavior.

### Game World Context (`src/VillageGame.Game/GameWorld`)

1. **GameWorld2D Class**
   - Represents the 2D game world for the tower defense game
   - Manages the game grid, entities, and their interactions
   - Responsible for drawing and updating the game world
   - Integrates with the MonoGame rendering pipeline
   - Currently implements a basic grid visualization

### Relationship with Global Context

- The Game World Context uses services from the Global Context but doesn't modify them
- It receives the Content management system from MonoGame for resource loading
- In the future, it will interact with the Data Storage service for saving/loading game state

## Cross-Cutting Concerns

### Dependency Injection

- Services are injected through constructors
- The GlobalContext is passed to the main Game class
- Service implementations are created during GlobalContext initialization

### Testing

- Unit tests are isolated from production code through interfaces
- Mock implementations can be provided for testing
- Example: DataStoreTests demonstrates testing the JsonDataStore implementation

### Error Handling

- Services include proper exception handling
- Errors are logged through the Logger service
- Async operations use try/catch blocks and return success/failure indicators

## Protocol Rules

1. **Global Context Access Pattern**
   - Components should access services via the IGlobalContext interface
   - Direct service-to-service communication should be minimized
   - Services should be initialized in the correct dependency order

2. **Local Context Boundaries**
   - Local contexts should have clear boundaries and responsibilities
   - They should depend only on the Global Context, not on other local contexts
   - Internal state should be encapsulated and not exposed outside the context

3. **Service Lifetime Management**
   - Services are created during GlobalContext initialization
   - They remain alive for the duration of the application
   - Proper cleanup is performed when the application shuts down

4. **Extensibility Points**
   - All key services are defined by interfaces in the Core project
   - Implementations can be swapped without affecting game logic
   - This supports future extensions like cloud integration

## Future Cloud Integration

The architecture is designed to support easy integration with cloud services:

1. **Data Storage**
   - The IDataStore interface can be implemented by a cloud provider
   - Keys can map to cloud storage paths or document IDs
   - The application code won't need to change when switching storage providers

2. **Analytics**
   - IAnalyticsService can be implemented using a provider like Firebase or AppCenter
   - Events, screen views, and user properties map directly to cloud analytics concepts
   - Local buffering can be added for offline support

3. **Monetization**
   - IMonetizationService can be connected to app store purchases and ad networks
   - The current mock implementation can be replaced with platform-specific implementations
   - The events (PurchaseCompleted, AdDisplayed, AdClosed) provide hooks for game logic

## Code Organization

The project code is organized into the following key areas:

### Core Project (`VillageGame.Core`)

Contains all interfaces and domain models that define the application's core functionality:

- **Interfaces**: Defines contracts for all services
- **Models**: Contains domain models and data structures

### Infrastructure Project (`VillageGame.Infrastructure`)

Implements the interfaces defined in the Core project:

- **Services**: Concrete implementations of service interfaces

### Game Project (`VillageGame.Game`)

The main application that uses the Core and Infrastructure projects:

- **GameWorld**: Implements the game world and gameplay mechanics
- **Content**: Contains game assets and resources

### Tests Project (`VillageGame.Tests`)

Contains unit tests for all components:

- **Core**: Tests for core functionality and services 