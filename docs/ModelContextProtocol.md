# Model Context Protocol Documentation

This document details how the Model Context Protocol is implemented in the Village Game project, explaining both global and local contexts and their interactions.

## Introduction to Model Context Protocol

The Model Context Protocol is an architectural pattern that organizes code into distinct contexts with clear boundaries and responsibilities. It defines how data and functionality flow between different parts of the application while maintaining loose coupling.

In our implementation, we distinguish between two main types of contexts:

1. **Global Context**: Provides application-wide services and configuration
2. **Local Contexts**: Represent specific features or modules with their own internal state

## Global Context

The Global Context acts as a central service hub that provides shared functionality to all parts of the application. It follows a service locator pattern while maintaining clear interfaces for all services.

### Implementation

Our Global Context is implemented through the `IGlobalContext` interface and its concrete implementation `GlobalContext`. The interface defines properties for accessing all global services:

```csharp
public interface IGlobalContext
{
    ILoggerService Logger { get; }
    IDataStore DataStore { get; }
    IAnalyticsService Analytics { get; }
    IMonetizationService Monetization { get; }
    IAppConfig Config { get; }
    void Initialize();
}
```

The implementation initializes all services in the correct order, respecting dependencies between them:

```csharp
public void Initialize()
{
    if (_isInitialized)
    {
        return;
    }
    
    // Initialize services in the correct order
    InitializeConfig();
    InitializeLogger();
    InitializeDataStore();
    InitializeAnalytics();
    InitializeMonetization();
    
    _isInitialized = true;
}
```

### Global Service Interfaces

All services in the Global Context are defined by interfaces in the Core project:

- **IAppConfig**: Configuration management
- **ILoggerService**: Logging and event tracking
- **IDataStore**: Data persistence abstraction
- **IAnalyticsService**: User behavior analytics
- **IMonetizationService**: In-app purchases and ads

These interfaces establish clear contracts for service implementations while hiding implementation details from consumers.

## Local Contexts

Local Contexts represent specific functional areas of the application. They encapsulate related functionality and state, and they depend on the Global Context for shared services.

### Game World Context

The main Local Context in our application is the Game World context, which manages the 2D game world for our tower defense game. It's implemented in the `GameWorld2D` class and related components.

The Game World context:

1. Manages the game grid and entities
2. Handles rendering through MonoGame
3. Processes game updates and user input
4. Will eventually manage game logic like tower placement, enemy movement, etc.

```csharp
public class GameWorld2D
{
    // Internal state
    private Texture2D _gridTexture;
    private Rectangle _worldBounds;
    
    // Methods for interacting with the context
    public void LoadContent(ContentManager content) { ... }
    public void Update(GameTime gameTime) { ... }
    public void Draw(SpriteBatch spriteBatch) { ... }
}
```

### Future Local Contexts

As the game evolves, additional Local Contexts will be added:

1. **Tower Management Context**: Will handle tower creation, upgrades, and abilities
2. **Enemy Context**: Will manage enemy waves, behavior, and pathfinding
3. **Player Resources Context**: Will track player's resources, score, and progression
4. **UI Context**: Will handle user interface elements and interactions

## Context Interaction Rules

Our implementation follows these key rules for context interactions:

### 1. Dependency Direction

- Local Contexts can depend on the Global Context
- The Global Context should never depend on Local Contexts
- Local Contexts should not directly depend on other Local Contexts

### 2. Service Access

- Components access Global Context services through the `IGlobalContext` interface
- Services should be accessed through their respective interfaces, not concrete implementations
- Direct service-to-service communication should be minimized

### 3. Data Flow

- Data flows from the Global Context to Local Contexts through service method calls
- Local Contexts can send data to the Global Context through events or service method calls
- When Local Contexts need to communicate, they should do so through the Global Context

### 4. State Management

- Global Context services can maintain global state (e.g., configuration, user data)
- Local Contexts maintain their own internal state
- State that needs to be persisted should go through the DataStore service

## Practical Examples

### Accessing Global Services from a Local Context

```csharp
// In VillageGame class (which receives the GlobalContext in its constructor)
protected override void Initialize()
{
    _globalContext.Logger.LogInformation("Game initialization started");
    
    // Configure graphics settings
    _graphics.PreferredBackBufferWidth = 1280;
    _graphics.PreferredBackBufferHeight = 720;
    _graphics.ApplyChanges();
    
    // Initialize game world
    _gameWorld = new GameWorld2D();
    
    base.Initialize();
    
    _globalContext.Logger.LogInformation("Game initialization completed");
}
```

### Storing and Retrieving Data

```csharp
// Store game progress
await _globalContext.DataStore.SaveDataAsync("game_progress", progressData);

// Retrieve game progress
var progress = await _globalContext.DataStore.LoadDataAsync<GameProgress>("game_progress");
```

### Tracking User Events

```csharp
// Log a tower placement event
_globalContext.Analytics.TrackEventAsync("tower_placed", new Dictionary<string, object>
{
    { "tower_type", towerType },
    { "position_x", position.X },
    { "position_y", position.Y },
    { "cost", cost }
});
```

## Benefits of This Approach

The Model Context Protocol as implemented in our project provides several benefits:

1. **Separation of Concerns**: Clear boundaries between different parts of the application
2. **Testability**: Services can be easily mocked for unit testing
3. **Flexibility**: Implementation details can change without affecting consumers
4. **Scalability**: New features can be added as new Local Contexts
5. **Maintainability**: Code organization makes the system easier to understand and modify

## Future Extensions

As the game evolves, the Model Context Protocol will support:

1. **Platform-specific Implementations**: Different service implementations for iOS and Android
2. **Cloud Integration**: Cloud-based implementations of storage and analytics services
3. **New Features**: Additional Local Contexts for new game features
4. **Multiplayer Support**: Contexts for handling online play and synchronization 