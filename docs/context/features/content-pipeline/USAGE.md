# Content Pipeline Usage Guide

## Overview

This guide explains how to use the content pipeline for Village Game, including adding new assets, building content, and special considerations for different platforms (especially Apple Silicon Macs).

## Prerequisites

Before working with the content pipeline, ensure you have:

1. The MonoGame SDK installed for your platform
2. For Apple Silicon Macs: x64 .NET SDK installed via Rosetta 2 (see installation steps in the [SOLUTION.md](./SOLUTION.md))
3. Required MonoGame content tools:
   - `dotnet-mgcb` (Content Builder)
   - `dotnet-mgcb-editor` (Editor UI)

## Project Content Structure

The game's content is organized in the following structure:

```
src/VillageGame.Game/
└── Content/
    ├── Content.mgcb       # Main content project file
    ├── bin/               # Output directory for processed content
    └── obj/               # Intermediate files during content building
```

## Adding New Content

### Method 1: Using MGCB Editor (Recommended for most users)

1. **Launch MGCB Editor**:
   - **Windows**: `mgcb-editor Content/Content.mgcb`
   - **macOS (Intel)**: `mgcb-editor Content/Content.mgcb`
   - **macOS (Apple Silicon)**: `arch -x86_64 $HOME/.dotnet/dotnet mgcb-editor Content/Content.mgcb`

2. **Add Content**:
   - Click "Add" → "Existing Item..." and select your asset file
   - Set appropriate importer and processor based on the asset type
   - Click "Build" to test the content processing

3. **Save Project**:
   - The changes will be saved to the Content.mgcb file

### Method 2: Manual Content.mgcb Editing

You can manually edit the Content.mgcb file to add new assets:

```
#begin path/to/your/asset.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:path/to/your/asset.png
```

## Building Content

### Automatic Content Building

The content is automatically built when you:

1. Run the build script: `./build-macos.sh` (macOS) or `dotnet build` (other platforms)
2. Run the game: `./run-macos.sh` (macOS) or `dotnet run --project src/VillageGame.Game` (other platforms)

### Manual Content Building

To manually build content:

```bash
# Windows and macOS Intel
dotnet mgcb /@:src/VillageGame.Game/Content/Content.mgcb

# macOS Apple Silicon
arch -x86_64 $HOME/.dotnet/dotnet mgcb /@:src/VillageGame.Game/Content/Content.mgcb
```

## Loading Content in Code

To load content in code:

```csharp
// In LoadContent method
Texture2D myTexture = Content.Load<Texture2D>("path/to/asset");  // No file extension needed
SpriteFont myFont = Content.Load<SpriteFont>("fonts/myfont");
```

## Supported Asset Types

The MonoGame content pipeline supports many asset types:

| Asset Type | File Extensions | Content Type |
|------------|----------------|--------------|
| Textures   | .png, .jpg, .bmp, .tga, .dds | Texture2D |
| Fonts      | .spritefont | SpriteFont |
| Audio      | .wav, .mp3, .ogg | SoundEffect, Song |
| 3D Models  | .fbx, .x | Model |
| Effects    | .fx | Effect |
| JSON Data  | .json | Various |

## Special Considerations for Apple Silicon Macs

If you're using an Apple Silicon Mac:

1. Always use the provided `build-macos.sh` and `run-macos.sh` scripts
2. When manually working with content tools, prefix commands with `arch -x86_64 $HOME/.dotnet/dotnet`
3. Keep the content simple and standardized to ensure cross-platform compatibility

## Troubleshooting

### Common Issues

1. **Content Not Found**:
   - Ensure path is correct relative to the Content directory
   - Check that the content was built successfully
   - Verify that Content.RootDirectory is set correctly (usually in Game constructor)

2. **Build Errors**:
   - Check that the content file format is supported
   - Verify the correct importer/processor is being used
   - Look for syntax errors in the Content.mgcb file

3. **Apple Silicon Issues**:
   - Ensure x64 .NET SDK is installed
   - Check that you're running the commands with `arch -x86_64`
   - Verify that all required tools are installed for the x64 architecture

### Getting Help

If you encounter issues not covered in this guide:

1. Check the [MonoGame documentation](https://docs.monogame.net/articles/content/pipeline/index.html)
2. Review the solution architecture in [SOLUTION.md](./SOLUTION.md)
3. Contact the project team for specific help with Village Game content pipeline 