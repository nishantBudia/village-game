using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using VillageGame.Infrastructure.Services;

namespace VillageGame.Game
{
    [Activity(
        Label = "Village Game",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private VillageGame _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Initialize the game and view
            var globalContext = new GlobalContext();
            globalContext.Initialize();
            
            _game = new VillageGame(globalContext);
            _view = _game.Services.GetService(typeof(View)) as View;

            // Set our view
            SetContentView(_view);
            _game.Run();
        }
    }
} 