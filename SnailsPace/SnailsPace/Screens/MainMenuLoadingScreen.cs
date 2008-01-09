using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A_Snail_s_Pace.Screens
{
    class MainMenuLoadingScreen : LoadingScreen
    {
        public MainMenuLoadingScreen(SnailsPace game)
            : base(game, SnailsPace.GameStates.MainMenu)
        {
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.Green, 1.0f, 0);
        }
    }
}
