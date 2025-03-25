# Code Examples

This document provides practical code examples for common scenarios in the Village Game project, demonstrating how to work with the architecture.

## Table of Contents

- [Working with Global Context](#working-with-global-context)
- [Data Persistence](#data-persistence)
- [Logging](#logging)
- [User Analytics](#user-analytics)
- [In-App Purchases](#in-app-purchases)
- [Game World Interaction](#game-world-interaction)
- [Testing](#testing)

## Working with Global Context

### Accessing Services

To access services from the Global Context, you should receive it via constructor injection:

```csharp
public class GameFeature
{
    private readonly IGlobalContext _globalContext;
    
    public GameFeature(IGlobalContext globalContext)
    {
        _globalContext = globalContext;
    }
    
    public void DoSomething()
    {
        // Now you can access any service through the global context
        _globalContext.Logger.LogInformation("Feature is doing something");
        
        // Load configuration
        var maxEnemies = _globalContext.Config.GetValue<int>("MaxEnemiesPerWave", 10);
    }
}
```

### Initializing Services

The GlobalContext handles service initialization, but if you're creating a custom service, initialize it like this:

```csharp
public class CustomAnalyticsService : IAnalyticsService
{
    private readonly ILoggerService _logger;
    
    public CustomAnalyticsService(ILoggerService logger)
    {
        _logger = logger;
    }
    
    public async Task<bool> InitializeAsync()
    {
        try
        {
            _logger.LogInformation("Initializing custom analytics service");
            
            // Initialization logic here
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Failed to initialize custom analytics service");
            return false;
        }
    }
    
    // Implement other interface methods
}
```

## Data Persistence

### Saving Game Data

```csharp
public async Task SaveGameProgressAsync(GameProgress progress)
{
    // Create a game progress object
    var gameProgress = new GameProgress
    {
        PlayerLevel = 5,
        Gold = 1000,
        CompletedLevels = new List<int> { 1, 2, 3 },
        LastPlayedTime = DateTime.UtcNow
    };
    
    // Save it to the data store
    await _globalContext.DataStore.SaveDataAsync("game_progress", gameProgress);
}
```

### Loading Game Data

```csharp
public async Task<GameProgress> LoadGameProgressAsync()
{
    // Try to load existing progress
    var progress = await _globalContext.DataStore.LoadDataAsync<GameProgress>("game_progress");
    
    // Return default progress if none exists
    if (progress == null)
    {
        return new GameProgress
        {
            PlayerLevel = 1,
            Gold = 100,
            CompletedLevels = new List<int>(),
            LastPlayedTime = DateTime.UtcNow
        };
    }
    
    return progress;
}
```

### Checking If Data Exists

```csharp
public async Task<bool> HasGameProgressAsync()
{
    return await _globalContext.DataStore.KeyExistsAsync("game_progress");
}
```

## Logging

### Basic Logging

```csharp
// Log different severity levels
_globalContext.Logger.LogDebug("Debug information for troubleshooting");
_globalContext.Logger.LogInformation("General information about normal operation");
_globalContext.Logger.LogWarning("Warning about potential issues");
_globalContext.Logger.LogError("Errors that require attention");

// Log exceptions
try
{
    // Some operation that might fail
}
catch (Exception ex)
{
    _globalContext.Logger.LogException(ex, "Failed to perform operation");
}
```

### Logging User Events

```csharp
// Log a tower placement event
_globalContext.Logger.LogUserEvent("TowerPlaced", new 
{
    TowerType = "Archer",
    Position = new { X = 10, Y = 15 },
    Cost = 100
});

// Log a level completed event
_globalContext.Logger.LogUserEvent("LevelCompleted", new 
{
    LevelId = 5,
    Stars = 3,
    TimeSeconds = 120,
    Score = 1500
});
```

## User Analytics

### Tracking Events

```csharp
// Track a simple event
await _globalContext.Analytics.TrackEventAsync("button_clicked");

// Track an event with properties
await _globalContext.Analytics.TrackEventAsync("tower_purchased", new Dictionary<string, object>
{
    { "tower_type", "Archer" },
    { "price", 100 },
    { "level", 1 },
    { "using_premium_currency", false }
});
```

### Tracking Screen Views

```csharp
// Track when a player views a screen
await _globalContext.Analytics.TrackScreenViewAsync("MainMenu");
await _globalContext.Analytics.TrackScreenViewAsync("LevelSelect", new Dictionary<string, object>
{
    { "coming_from", "MainMenu" },
    { "available_levels", 10 }
});
```

### Setting User Properties

```csharp
// Set user properties for segmentation
await _globalContext.Analytics.SetUserPropertyAsync("player_level", 5);
await _globalContext.Analytics.SetUserPropertyAsync("premium_user", true);
await _globalContext.Analytics.SetUserPropertyAsync("total_playtime_minutes", 120);

// Set user ID if you have one
await _globalContext.Analytics.SetUserIdAsync("user_12345");
```

## In-App Purchases

### Checking Available Products

```csharp
public async Task DisplayStoreItemsAsync()
{
    var products = await _globalContext.Monetization.GetProductsAsync();
    
    foreach (var product in products)
    {
        Console.WriteLine($"Product: {product.Name}");
        Console.WriteLine($"  Description: {product.Description}");
        Console.WriteLine($"  Price: {product.Price} {product.Currency}");
        Console.WriteLine($"  ID: {product.Id}");
    }
}
```

### Making a Purchase

```csharp
public async Task<bool> PurchaseProductAsync(string productId)
{
    try
    {
        // Check if product is already owned
        if (await _globalContext.Monetization.IsProductOwnedAsync(productId))
        {
            _globalContext.Logger.LogInformation($"Product {productId} is already owned");
            return true;
        }
        
        // Initiate purchase
        var success = await _globalContext.Monetization.PurchaseAsync(productId);
        
        if (success)
        {
            _globalContext.Logger.LogInformation($"Successfully purchased {productId}");
            // Grant the purchased item to the player
            await GrantPurchasedItemAsync(productId);
        }
        else
        {
            _globalContext.Logger.LogWarning($"Failed to purchase {productId}");
        }
        
        return success;
    }
    catch (Exception ex)
    {
        _globalContext.Logger.LogException(ex, $"Error during purchase of {productId}");
        return false;
    }
}

// Event handler for purchase completion
private void OnPurchaseCompleted(object sender, PurchaseEventArgs e)
{
    _globalContext.Logger.LogInformation($"Purchase completed: {e.Purchase.ProductId}");
    _globalContext.Analytics.TrackEventAsync("purchase_completed", new Dictionary<string, object>
    {
        { "product_id", e.Purchase.ProductId },
        { "transaction_id", e.Purchase.TransactionId }
    });
}
```

### Showing Ads

```csharp
public async Task<bool> ShowRewardedAdAsync()
{
    try
    {
        // Check if ads are ready
        if (!await _globalContext.Monetization.AreAdsReadyAsync(AdType.Rewarded))
        {
            _globalContext.Logger.LogInformation("Rewarded ad not ready");
            return false;
        }
        
        // Show the ad
        var success = await _globalContext.Monetization.ShowAdAsync(AdType.Rewarded);
        
        if (success)
        {
            _globalContext.Logger.LogInformation("Rewarded ad shown successfully");
        }
        else
        {
            _globalContext.Logger.LogWarning("Failed to show rewarded ad");
        }
        
        return success;
    }
    catch (Exception ex)
    {
        _globalContext.Logger.LogException(ex, "Error showing rewarded ad");
        return false;
    }
}

// Event handler for ad completion
private void OnAdClosed(object sender, AdEventArgs e)
{
    if (e.AdType == AdType.Rewarded && e.Rewarded)
    {
        _globalContext.Logger.LogInformation("Rewarded ad completed, granting reward");
        GrantAdReward();
    }
}
```

## Game World Interaction

### Creating a New Enemy

```csharp
public class Enemy
{
    public Vector2 Position { get; set; }
    public float Health { get; set; }
    public float Speed { get; set; }
    public EnemyType Type { get; set; }
    
    public void Update(GameTime gameTime)
    {
        // Update enemy logic
    }
    
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        // Draw enemy
        spriteBatch.Draw(texture, Position, Color.White);
    }
}

// In GameWorld2D class
public void SpawnEnemy(EnemyType type, Vector2 position)
{
    var enemy = new Enemy
    {
        Position = position,
        Type = type,
        Health = GetHealthForType(type),
        Speed = GetSpeedForType(type)
    };
    
    _enemies.Add(enemy);
    
    _globalContext.Logger.LogInformation($"Spawned enemy of type {type} at position {position}");
}
```

### Tower Placement

```csharp
public class Tower
{
    public Vector2 Position { get; set; }
    public float Range { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public TowerType Type { get; set; }
    
    public void Update(GameTime gameTime, List<Enemy> enemies)
    {
        // Update tower logic, find targets, attack
    }
    
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        // Draw tower
        spriteBatch.Draw(texture, Position, Color.White);
    }
}

// In GameWorld2D class
public bool PlaceTower(TowerType type, int gridX, int gridY)
{
    // Convert grid coordinates to world position
    var position = new Vector2(gridX * GridSize, gridY * GridSize);
    
    // Check if the position is valid for tower placement
    if (!IsTowerPlacementValid(gridX, gridY))
    {
        _globalContext.Logger.LogInformation($"Cannot place tower at grid position {gridX},{gridY}");
        return false;
    }
    
    // Create the tower
    var tower = new Tower
    {
        Position = position,
        Type = type,
        Range = GetRangeForType(type),
        Damage = GetDamageForType(type),
        AttackSpeed = GetAttackSpeedForType(type)
    };
    
    _towers.Add(tower);
    
    _globalContext.Logger.LogInformation($"Placed tower of type {type} at grid position {gridX},{gridY}");
    _globalContext.Analytics.TrackEventAsync("tower_placed", new Dictionary<string, object>
    {
        { "tower_type", type.ToString() },
        { "grid_x", gridX },
        { "grid_y", gridY }
    });
    
    return true;
}
```

## Testing

### Testing a Service

```csharp
[TestFixture]
public class AnalyticsServiceTests
{
    private Mock<ILoggerService> _mockLogger;
    private Mock<IDataStore> _mockDataStore;
    private IAnalyticsService _analyticsService;
    
    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILoggerService>();
        _mockDataStore = new Mock<IDataStore>();
        
        _analyticsService = new LocalAnalyticsService(_mockLogger.Object, _mockDataStore.Object);
    }
    
    [Test]
    public async Task TrackEventAsync_ValidEvent_ReturnsTrue()
    {
        // Arrange
        var eventName = "test_event";
        var properties = new Dictionary<string, object> { { "test_key", "test_value" } };
        
        _mockDataStore.Setup(ds => ds.SaveDataAsync(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(true);
            
        // Act
        var result = await _analyticsService.TrackEventAsync(eventName, properties);
        
        // Assert
        Assert.IsTrue(result);
        _mockLogger.Verify(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        _mockDataStore.Verify(ds => ds.SaveDataAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
    }
}
```

### Testing Game Logic

```csharp
[TestFixture]
public class GameWorld2DTests
{
    private GameWorld2D _gameWorld;
    private Mock<ContentManager> _mockContent;
    
    [SetUp]
    public void Setup()
    {
        _mockContent = new Mock<ContentManager>();
        _gameWorld = new GameWorld2D();
    }
    
    [Test]
    public void PlaceTower_ValidPosition_ReturnsTrue()
    {
        // Arrange
        var gridX = 5;
        var gridY = 5;
        var towerType = TowerType.Archer;
        
        // Act
        var result = _gameWorld.PlaceTower(towerType, gridX, gridY);
        
        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(1, _gameWorld.Towers.Count);
        Assert.AreEqual(towerType, _gameWorld.Towers[0].Type);
    }
    
    [Test]
    public void PlaceTower_InvalidPosition_ReturnsFalse()
    {
        // Arrange - Set up an invalid position (e.g., on the path)
        var gridX = 2;
        var gridY = 2;
        var towerType = TowerType.Archer;
        
        // Assume this is a path position
        _gameWorld.SetPathTile(gridX, gridY);
        
        // Act
        var result = _gameWorld.PlaceTower(towerType, gridX, gridY);
        
        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(0, _gameWorld.Towers.Count);
    }
} 