using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using VillageGame.Core.Interfaces;
using VillageGame.Infrastructure.Services;

namespace VillageGame.Game
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        Theme = "@style/Theme.Splash",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private VillageGame _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

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

            // Create game instance with services
            _game = new VillageGame(logger, dataStore, analytics, monetization, config);
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);
            _game.Run();
        }
    }
} 