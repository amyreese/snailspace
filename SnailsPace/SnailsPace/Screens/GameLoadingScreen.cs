using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SnailsPace.Screens
{
    /// <summary>
    /// A screen displayed when the game is loading
    /// </summary>
    class GameLoadingScreen : LoadingScreen
    {
        /// <summary>
        /// Constructor for the screen
        /// </summary>
        /// <param name="game">Snails Pace instance</param>
        public GameLoadingScreen(SnailsPace game)
            : base(game, SnailsPace.GameStates.Game)
        {
        }

        // The background image
        private Texture2D screenImage;

        /// <summary>
        /// Load the background image
        /// </summary>
        protected override void LoadContent()
        {
            screenImage = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/GameLoadingScreen");
            base.LoadContent();
        }

        /// <summary>
        /// Draw the screen
        /// </summary>
        /// <param name="gameTime">GameTime for this draw</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.Red, 1.0f, 0);
            SpriteBatch batch = new SpriteBatch(GraphicsDevice);
            batch.Begin();
            {
                batch.Draw(screenImage, new Rectangle(0, 0, SnailsPace.getInstance().Window.ClientBounds.Width, SnailsPace.getInstance().Window.ClientBounds.Height), Color.White);
            }
            batch.End();
            batch.Dispose();
        }
    }
}
