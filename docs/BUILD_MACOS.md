# Building on macOS with Apple Silicon

## One-Time Setup

Install the x64 .NET SDK using Rosetta 2:

```bash
arch -x86_64 /bin/bash -c "curl -sSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel 9.0"
```

Add the x64 .NET SDK to your PATH (add to your shell profile for persistence):

```bash
export PATH="$PATH:$HOME/.dotnet/x64"
```

## Development Workflow

Always use the `arch -x86_64` prefix when building or running the project:

### Building the project

```bash
arch -x86_64 dotnet build
```

### Running the game

```bash
arch -x86_64 dotnet run --project src/VillageGame.Game
```

### Using the Content Pipeline (MGCB) tool

```bash
arch -x86_64 dotnet mgcb
```

## CI/CD

No changes needed for CI/CD pipelines, as they typically run on x64 architecture already.

## Troubleshooting

If you encounter issues with the content pipeline:

1. Make sure you're using the `arch -x86_64` prefix
2. Verify the x64 .NET SDK is installed: `arch -x86_64 dotnet --info`
3. Check that the MGCB tool is installed: `arch -x86_64 dotnet tool list -g` 