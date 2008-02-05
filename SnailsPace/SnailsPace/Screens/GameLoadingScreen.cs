using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SnailsPace.Screens
{
    class GameLoadingScreen : LoadingScreen
    {
        public GameLoadingScreen(SnailsPace game)
            : base(game, SnailsPace.GameStates.Game)
        {
        }

        Texture2D screenImage;

        protected override void LoadContent()
        {
            screenImage = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/GameLoadingScreen");
        }

        protected override void UnloadContent()
        {
        }

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
