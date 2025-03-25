#!/bin/bash

# Set the Android SDK path to the specific path provided
ANDROID_SDK_PATH="/Users/nishant-personal/Library/Android/sdk"

# Check if Android SDK exists
if [ ! -d "$ANDROID_SDK_PATH" ]; then
    echo "Android SDK not found at $ANDROID_SDK_PATH"
    echo "Please run Android Studio first and complete the SDK installation"
    echo "Then update this script with the correct SDK path"
    exit 1
fi

# Build the Android APK
echo "Building Android APK..."
dotnet build src/VillageGame.Game/VillageGame.Game.csproj -c Release -p:AndroidSdkDirectory="$ANDROID_SDK_PATH"

# Check if build was successful
if [ $? -eq 0 ]; then
    # Find the APK file
    APK_PATH=$(find src/VillageGame.Game/bin/Release -name "*.apk" | head -1)
    
    if [ -n "$APK_PATH" ]; then
        # Copy the APK to a convenient location
        mkdir -p apk
        cp "$APK_PATH" apk/
        echo "APK built successfully and copied to: apk/$(basename "$APK_PATH")"
        echo "You can install this APK on your Android device"
    else
        echo "APK not found after build"
        exit 1
    fi
else
    echo "Build failed"
    exit 1
fi 