using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VillageGame.Game.GameWorld
{
    /// <summary>
    /// Represents the 2D game world for the tower defense game.
    /// This class manages the game map, entities, and their interactions.
    /// </summary>
    public class GameWorld2D
    {
        private Texture2D? _gridTexture;
        private Rectangle _worldBounds;
        
        /// <summary>
        /// Constructor for the GameWorld2D class
        /// </summary>
        public GameWorld2D()
        {
            _worldBounds = new Rectangle(0, 0, 1280, 720);
        }
        
        /// <summary>
        /// Loads content required by the game world
        /// </summary>
        /// <param name="content">The ContentManager to use for loading</param>
        /// <param name="graphicsDevice">The graphics device used to create resources</param>
        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            // Create a 1x1 white texture for the grid
            _gridTexture = new Texture2D(graphicsDevice, 1, 1);
            _gridTexture.SetData(new[] { Color.White });
        }
        
        /// <summary>
        /// Updates the game world state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Update game entities (will be implemented later)
        }
        
        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use for drawing</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw grid for visualization (32x32 grid cells)
            const int gridSize = 32;
            for (int x = 0; x <= _worldBounds.Width; x += gridSize)
            {
                spriteBatch.Draw(
                    _gridTexture, 
                    new Rectangle(x, 0, 1, _worldBounds.Height), 
                    Color.Gray * 0.3f
                );
            }
            
            for (int y = 0; y <= _worldBounds.Height; y += gridSize)
            {
                spriteBatch.Draw(
                    _gridTexture, 
                    new Rectangle(0, y, _worldBounds.Width, 1), 
                    Color.Gray * 0.3f
                );
            }
            
            // Draw placeholder text
            // Note: In a complete implementation, a SpriteFont would be loaded and used here
        }
    }
} 