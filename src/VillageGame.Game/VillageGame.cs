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
        private SpriteBatch? _spriteBatch;
        private GameWorld2D? _gameWorld;
        private IGlobalContext _globalContext;

        public VillageGame(IGlobalContext globalContext)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _globalContext = globalContext;
            _gameWorld = new GameWorld2D();
        }

        protected override void Initialize()
        {
            _globalContext.Logger.LogInformation("Game initialization started");
            
            // Configure graphics settings
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            
            base.Initialize();
            
            _globalContext.Logger.LogInformation("Game initialization completed");
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Load game world content
            if (_gameWorld != null)
            {
                _gameWorld.LoadContent(Content, GraphicsDevice);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update game world
            if (_gameWorld != null)
            {
                _gameWorld.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (_spriteBatch != null && _gameWorld != null)
            {
                _spriteBatch.Begin();
                
                // Draw game world
                _gameWorld.Draw(_spriteBatch);
                
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
} 