using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace A_Snail_s_Pace.Screens
{
    class GameLoadingScreen : LoadingScreen
    {
        public GameLoadingScreen( SnailsPace game )
            : base(game, SnailsPace.GameStates.Game)
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
                Color.Red, 1.0f, 0);
        }

        public override bool ready()
        {
            return true;
        }
    }
}
