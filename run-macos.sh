#!/bin/bash

# Detect if running on Apple Silicon
if [[ $(uname -m) == "arm64" ]]; then
    echo "Running Village Game using Rosetta 2..."
    
    # Path to x64 dotnet executable
    DOTNET_X64="$HOME/.dotnet/dotnet"
    
    # Check if x64 .NET SDK exists
    if [ ! -f "$DOTNET_X64" ]; then
        echo "Error: x64 .NET SDK not found at $DOTNET_X64"
        echo "Please install it using: arch -x86_64 /bin/bash -c \"curl -sSL https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh | bash -s -- --channel 9.0\""
        exit 1
    fi
    
    # First run the build script to ensure content is built
    ./build-macos.sh
    
    # Only run if build was successful
    if [ $? -eq 0 ]; then
        echo "Running the game with Rosetta 2..."
        arch -x86_64 "$DOTNET_X64" run --project src/VillageGame.Game
    else
        echo "Build failed. Please fix the issues before running the game."
        exit 1
    fi
else
    echo "Running Village Game..."
    dotnet run --project src/VillageGame.Game
fi 