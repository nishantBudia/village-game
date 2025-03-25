# Code Standards and Development Guidelines

This document outlines the coding standards, practices, and development guidelines for the Village Game project.

## Table of Contents

- [Code Organization](#code-organization)
- [Naming Conventions](#naming-conventions)
- [Documentation](#documentation)
- [Exception Handling](#exception-handling)
- [Asynchronous Programming](#asynchronous-programming)
- [Testing](#testing)
- [Performance Considerations](#performance-considerations)
- [Mobile-Specific Guidelines](#mobile-specific-guidelines)
- [Workflow and Git Practices](#workflow-and-git-practices)

## Code Organization

### Project Structure

The solution is divided into four main projects:

1. **VillageGame.Core**: Contains interfaces and domain models
2. **VillageGame.Infrastructure**: Implements the interfaces from Core
3. **VillageGame.Game**: Contains the MonoGame implementation and game logic
4. **VillageGame.Tests**: Contains unit tests

### File Organization

- One class per file, with the filename matching the class name
- Group related files in appropriate folders
- Keep files focused and not too large (aim for < 500 lines per file)

### Class Organization

Organize class members in the following order:

1. Private/protected fields
2. Public properties
3. Constructors
4. Public methods
5. Private/protected methods
6. Nested types

## Naming Conventions

- **Namespaces**: PascalCase, align with folder structure (e.g., `VillageGame.Core.Interfaces`)
- **Classes/Interfaces**: PascalCase, nouns or noun phrases
  - Interfaces should start with "I" (e.g., `IDataStore`)
- **Methods**: PascalCase, verbs or verb phrases (e.g., `SaveData`, `Initialize`)
- **Properties**: PascalCase, nouns or adjectives (e.g., `Name`, `IsEnabled`)
- **Fields**: 
  - Private/protected: camelCase with underscore prefix (e.g., `_dataStore`)
  - Public: PascalCase (rare, prefer properties)
- **Parameters/Variables**: camelCase (e.g., `userData`, `configFile`)
- **Constants**: All caps with underscores (e.g., `MAX_PLAYERS`, `DEFAULT_TIMEOUT`)

## Documentation

### Code Comments

- Use XML documentation comments for all public members
- Include `<summary>` tags for classes, interfaces, methods, and properties
- Add `<param>`, `<returns>`, `<exception>` tags where applicable
- Include example usage for complex methods

Example:

```csharp
/// <summary>
/// Saves data to the data store
/// </summary>
/// <typeparam name="T">The type of data to save</typeparam>
/// <param name="key">The key to store the data under</param>
/// <param name="data">The data to store</param>
/// <returns>True if the save operation was successful</returns>
public Task<bool> SaveDataAsync<T>(string key, T data);
```

### Self-Documenting Code

- Use clear, descriptive names that convey purpose
- Extract complex logic into well-named methods
- Use enums instead of magic numbers or strings

## Exception Handling

- Use try/catch blocks around operations that might fail
- Prefer returning failure indicators instead of throwing exceptions for expected conditions
- Log exceptions with appropriate context information
- Don't catch exceptions you can't handle properly

```csharp
try
{
    // Operation that might fail
    await _dataStore.SaveDataAsync(key, data);
}
catch (Exception ex)
{
    _logger.LogException(ex, $"Failed to save data for key: {key}");
    return false; // Indicate failure
}
```

## Asynchronous Programming

- Use `async`/`await` pattern for asynchronous operations
- Methods that perform I/O or other potentially blocking operations should be asynchronous
- Suffix asynchronous methods with "Async"
- Avoid blocking calls in asynchronous methods
- Consider using cancellation tokens for long-running operations

```csharp
public async Task<T> LoadDataAsync<T>(string key)
{
    try
    {
        var json = await File.ReadAllTextAsync(GetFilePath(key));
        return JsonConvert.DeserializeObject<T>(json);
    }
    catch (Exception ex)
    {
        _logger.LogException(ex);
        return default;
    }
}
```

## Testing

### Unit Testing

- Write unit tests for all services and important business logic
- Follow the AAA pattern (Arrange, Act, Assert)
- Mock dependencies using interfaces
- Test both happy paths and error cases
- Keep tests independent and deterministic

```csharp
[Test]
public async Task SaveAndLoadData_StringValue_ReturnsCorrectValue()
{
    // Arrange
    var key = "test_string";
    var value = "Hello, World!";
    
    // Act
    var saveResult = await _dataStore.SaveDataAsync(key, value);
    var loadedValue = await _dataStore.LoadDataAsync<string>(key);
    
    // Assert
    Assert.IsTrue(saveResult, "Save operation should succeed");
    Assert.AreEqual(value, loadedValue, "Loaded value should match saved value");
}
```

### Test Coverage

- Aim for high test coverage of business logic and service implementations
- Focus on testing behavior rather than implementation details
- Use parameterized tests for testing multiple scenarios

## Performance Considerations

### Game Performance

- Minimize garbage collection during gameplay
- Pool objects instead of creating new ones frequently
- Optimize draw calls in render loops
- Use appropriate data structures for the task

### Mobile Considerations

- Be mindful of battery usage
- Minimize memory footprint
- Consider touch input differences from desktop
- Respect platform lifecycle events (suspend/resume)

## Mobile-Specific Guidelines

### Android Considerations

- Handle different screen sizes and densities
- Use appropriate file naming for drawables
- Follow Android lifecycle patterns

### iOS Considerations

- Follow Apple's Human Interface Guidelines
- Support different device sizes and orientations
- Manage memory carefully to avoid termination

## Workflow and Git Practices

### Branching Strategy

- Main branch: `main` - always stable, production-ready code
- Development branch: `develop` - integration branch for features
- Feature branches: `feature/feature-name` - for new features
- Release branches: `release/version` - for release preparation
- Hotfix branches: `hotfix/issue-description` - for urgent fixes

### Commit Messages

- Use clear, descriptive commit messages
- Start with a verb in imperative mood (e.g., "Add", "Fix", "Update")
- Keep the first line under 72 characters
- Add detailed description in subsequent lines if needed

Example:
```
Add save game functionality to GameWorld

- Implement serialization of game state
- Add auto-save feature on level completion
- Create save/load UI elements
```

### Pull Requests

- Keep PRs focused on a single feature or fix
- Include a description of changes and testing performed
- Reference related issues
- Ensure all tests pass before requesting review

### Code Reviews

- Review code for correctness, performance, and adherence to standards
- Look for potential bugs, edge cases, and security issues
- Check that appropriate tests are included
- Provide constructive feedback 