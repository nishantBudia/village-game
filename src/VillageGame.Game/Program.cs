using VillageGame.Core.Interfaces;
using VillageGame.Infrastructure.Services;

namespace VillageGame.Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Create and configure global context
            var globalContext = new GlobalContext();
            globalContext.Initialize();

            // Log application start
            globalContext.Logger.LogInformation("Village Game starting up");

            // Start game
            using (var game = new VillageGame(globalContext))
            {
                game.Run();
            }

            // Log application exit
            globalContext.Logger.LogInformation("Village Game shutting down");
        }
    }
} 