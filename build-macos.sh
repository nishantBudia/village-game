#!/bin/bash

# Detect if running on Apple Silicon
if [[ $(uname -m) == "arm64" ]]; then
    echo "Building Village Game using Rosetta 2..."
    
    # Path to x64 dotnet executable
    DOTNET_X64="$HOME/.dotnet/dotnet"
    
    # Check if x64 .NET SDK exists
    if [ ! -f "$DOTNET_X64" ]; then
        echo "Error: x64 .NET SDK not found at $DOTNET_X64"
        echo "Please install it using: arch -x86_64 /bin/bash -c \"curl -sSL https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh | bash -s -- --channel 9.0\""
        exit 1
    fi
    
    # Ensure MonoGame content pipeline package is restored
    echo "Restoring MonoGame content pipeline dependencies..."
    arch -x86_64 "$DOTNET_X64" add src/VillageGame.Game/VillageGame.Game.csproj package MonoGame.Framework.Content.Pipeline --version 3.8.2.1105 --no-restore
    arch -x86_64 "$DOTNET_X64" restore src/VillageGame.Game/VillageGame.Game.csproj
    
    # First, build content separately using Rosetta
    echo "Building MonoGame content using Rosetta 2..."
    CONTENT_MGCB="src/VillageGame.Game/Content/Content.mgcb"
    OUTPUT_DIR="src/VillageGame.Game/Content/bin/DesktopGL/Content"
    INTERMEDIATE_DIR="src/VillageGame.Game/Content/obj/DesktopGL/net9.0/Content"
    WORKING_DIR="src/VillageGame.Game/Content"
    
    # Create directories
    mkdir -p "$OUTPUT_DIR"
    mkdir -p "$INTERMEDIATE_DIR"
    
    # Run MGCB under Rosetta 2
    arch -x86_64 "$DOTNET_X64" mgcb /@:"$CONTENT_MGCB" /platform:DesktopGL /outputDir:"$OUTPUT_DIR" /intermediateDir:"$INTERMEDIATE_DIR" /workingDir:"$WORKING_DIR"
    
    # If content build was successful, build the project
    if [ $? -eq 0 ]; then
        echo "Content build successful, building project..."
        arch -x86_64 "$DOTNET_X64" build
    else
        echo "Content build failed!"
        exit 1
    fi
else
    echo "Building Village Game..."
    dotnet build
fi 