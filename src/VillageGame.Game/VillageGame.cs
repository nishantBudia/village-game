using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VillageGame.Core.Interfaces;
using VillageGame.Game.GameWorld;

namespace VillageGame.Game
{
    public class VillageGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameWorld2D _gameWorld;
        
        // Individual service dependencies
        private readonly ILoggerService _logger;
        private readonly IDataStore _dataStore;
        private readonly IAnalyticsService _analytics;
        private readonly IMonetizationService _monetization;
        private readonly IAppConfig _config;

        public VillageGame(
            ILoggerService logger,
            IDataStore dataStore,
            IAnalyticsService analytics,
            IMonetizationService monetization,
            IAppConfig config)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            // Store service references
            _logger = logger;
            _dataStore = dataStore;
            _analytics = analytics;
            _monetization = monetization;
            _config = config;
        }

        protected override void Initialize()
        {
            _logger.LogInformation("Game initialization started");
            
            // Configure graphics settings
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            
            // Initialize game world
            _gameWorld = new GameWorld2D();
            
            base.Initialize();
            
            _logger.LogInformation("Game initialization completed");
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Load game world content
            _gameWorld.LoadContent(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update game world
            _gameWorld.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            // Draw game world
            _gameWorld.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
} 