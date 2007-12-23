using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A_Snail_s_Pace.Screens
{
    class MainMenuLoadingScreen : LoadingScreen
    {
        public MainMenuLoadingScreen()
            : base(SnailsPace.GameStates.MainMenu)
        {
        }

        public override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
            }
        }

        public override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SnailsPace.getInstance().graphics.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.Green, 1.0f, 0);
        }

        public override bool ready()
        {
            return true;
        }
    }
}
