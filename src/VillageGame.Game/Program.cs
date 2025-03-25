using System;
using VillageGame.Infrastructure.Services;

namespace VillageGame.Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize services
            var config = new AppConfig();
            config.LoadFromFile("config.json");

            var logger = new LoggerService();
            logger.Initialize();
            
            var dataStore = new JsonDataStore();
            dataStore.InitializeAsync().GetAwaiter().GetResult();
            
            var analytics = new LocalAnalyticsService(logger, dataStore);
            analytics.InitializeAsync().GetAwaiter().GetResult();
            
            var monetization = new MockMonetizationService();
            monetization.InitializeAsync().GetAwaiter().GetResult();

            logger.LogInformation("Village Game starting up");

            try
            {
                using (var game = new VillageGame(logger, dataStore, analytics, monetization, config))
                {
                    game.Run();
                }
            }
            finally
            {
                logger.LogInformation("Village Game shutting down");
            }
        }
    }
} 