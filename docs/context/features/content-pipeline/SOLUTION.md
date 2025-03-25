# Content Pipeline Solution for Village Game

## Overview

This document outlines the implementation of the content pipeline solution for the Village Game project, with a particular focus on ensuring compatibility with Apple Silicon Macs.

## Challenge

The main challenge was that MonoGame's Content Pipeline tools (MGCB) have compatibility issues with Apple Silicon (ARM64) architecture. This is a common issue for many game development tools that are built for x64 architecture and haven't been fully updated for ARM64 support.

## Solution Approach

We implemented a Rosetta 2-based solution that enables the game to build and run on Apple Silicon Macs. This approach leverages the x64 .NET SDK running under Rosetta 2 to handle content building and game execution.

### Components of the Solution

1. **Build Script (`build-macos.sh`)**
   - Detects if running on Apple Silicon
   - Uses the x64 .NET SDK via Rosetta 2 for building on ARM64 Macs
   - Manually builds content using MGCB tool via Rosetta 2 
   - Provides appropriate error handling and instructions

2. **Run Script (`run-macos.sh`)**
   - Detects if running on Apple Silicon
   - Runs the build script to ensure content is up-to-date
   - Executes the game using the x64 .NET SDK via Rosetta 2

3. **Content Management**
   - Uses a simplified `Content.mgcb` file that defines the basic structure
   - Builds content separately from the main build process when on Apple Silicon

### Key Implementation Details

#### Rosetta 2 Integration

We use the `arch -x86_64` command to run the x64 .NET SDK under Rosetta 2:

```bash
arch -x86_64 "$DOTNET_X64" mgcb /@:"$CONTENT_MGCB" /platform:DesktopGL /outputDir:"$OUTPUT_DIR" /intermediateDir:"$INTERMEDIATE_DIR" /workingDir:"$WORKING_DIR"
```

#### Content Building Process

The content building process is separated from the main build on Apple Silicon to ensure compatibility:

1. First, we build the content using the MGCB tool via Rosetta 2
2. If successful, we proceed with building the project using the x64 .NET SDK

#### Code Adjustments

We made necessary adjustments to the codebase to accommodate the content pipeline approach:
- Updated `GameWorld2D.LoadContent` to accept a `GraphicsDevice` parameter
- Fixed corresponding call in `VillageGame.cs`

## Installation Requirements

For macOS Apple Silicon users:
1. Install the x64 .NET 9.0 SDK using Rosetta 2:
   ```bash
   arch -x86_64 /bin/bash -c "curl -sSL https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh | bash -s -- --channel 9.0"
   ```

2. Install the required tools using the x64 .NET SDK:
   ```bash
   arch -x86_64 $HOME/.dotnet/dotnet tool install -g dotnet-mgcb
   arch -x86_64 $HOME/.dotnet/dotnet tool install -g dotnet-mgcb-editor
   ```

## Building and Running

### On macOS (Apple Silicon and Intel)

Building:
```bash
./build-macos.sh
```

Running:
```bash
./run-macos.sh
```

The scripts automatically detect the architecture and use the appropriate approach.

## Limitations and Future Improvements

1. **Performance**: Running under Rosetta 2 has some performance overhead compared to native ARM64 execution.

2. **Long-term Solution**: As MonoGame improves ARM64 support, we should eventually transition to native ARM64 builds.

3. **Content Workflow**: The current solution separates content building from the main build process. A more integrated approach could be implemented in the future.

4. **Editor Experience**: The MGCB editor GUI may have issues on Apple Silicon. Consider documenting alternative workflows for content creation and editing.

## Conclusion

The implemented solution successfully addresses the compatibility issues with MonoGame's content pipeline on Apple Silicon Macs. It provides a reliable way to build and run the Village Game, ensuring that developers using Apple Silicon Macs can contribute to the project without issues. 