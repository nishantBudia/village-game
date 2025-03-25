# Village Game Architecture

## Overview

The Village Game is built using a service-based architecture that emphasizes:
- Dependency injection for services
- Clear separation of concerns
- Platform independence through interfaces
- Testability through abstraction

## Project Structure

The solution is divided into four main projects:

1. **VillageGame.Core**
   - Contains interfaces and models
   - Defines contracts for services
   - Platform-independent business logic

2. **VillageGame.Infrastructure**
   - Implements service interfaces
   - Handles cross-cutting concerns
   - Provides concrete implementations

3. **VillageGame.Game**
   - MonoGame game implementation
   - Platform-specific entry points
   - Game loop and rendering

4. **VillageGame.Tests**
   - Unit tests
   - Integration tests
   - Test utilities

## Core Services

The game uses several key services, each defined by an interface in the Core project:

1. **Configuration (IAppConfig)**
   - Manages application settings
   - Loads configuration from files
   - Provides access to game parameters

2. **Logging (ILoggerService)**
   - Handles debug and error logging
   - Tracks game events
   - Supports different log levels

3. **Data Storage (IDataStore)**
   - Manages game state persistence
   - Handles save/load operations
   - Abstracts storage implementation

4. **Analytics (IAnalyticsService)**
   - Tracks user behavior
   - Collects gameplay metrics
   - Reports errors and crashes

5. **Monetization (IMonetizationService)**
   - Handles in-app purchases
   - Manages advertisements
   - Tracks revenue events

## Service Implementation

Each service is implemented in the Infrastructure project:

1. **AppConfig**
   - JSON-based configuration
   - Environment-specific settings
   - Default values handling

2. **LoggerService**
   - File-based logging
   - Console output
   - Error aggregation

3. **JsonDataStore**
   - Local file storage
   - JSON serialization
   - Async operations

4. **LocalAnalyticsService**
   - Local event tracking
   - Metric aggregation
   - Debug analytics

5. **MockMonetizationService**
   - Simulated purchases
   - Test ad implementations
   - Revenue tracking

## Dependency Injection

Services are initialized at application startup and injected into components that need them:

1. **Entry Points**
   - Desktop (Program.cs)
   - Android (Activity1.cs)
   Initialize all required services

2. **Game Class**
   - Receives services through constructor
   - Manages game lifecycle
   - Coordinates subsystems

3. **Components**
   - Request services they need
   - Follow interface contracts
   - Maintain single responsibility

## Platform Independence

The architecture supports multiple platforms through:

1. **Interface Abstraction**
   - Core interfaces are platform-agnostic
   - Platform-specific implementations in Infrastructure
   - Entry points handle platform differences

2. **MonoGame Integration**
   - Cross-platform rendering
   - Input handling
   - Asset management

## Testing Strategy

The architecture facilitates testing through:

1. **Interface-based Design**
   - Services can be easily mocked
   - Dependencies are explicit
   - Behavior can be verified

2. **Separation of Concerns**
   - Components are focused
   - Dependencies are minimal
   - Logic is isolated

## Future Extensibility

The architecture supports future growth through:

1. **Service Evolution**
   - New services can be added
   - Existing services can be enhanced
   - Implementations can be swapped

2. **Platform Support**
   - New platforms can be added
   - Services can be specialized
   - Core remains unchanged

3. **Feature Addition**
   - New components can be created
   - Services can be extended
   - Integration is straightforward 