using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using A_Snail_s_Pace.Graphics;
using A_Snail_s_Pace.Input;

namespace A_Snail_s_Pace.Screens
{
    class GameScreen : Screen
    {
		private GameEngine engine;

        public GameScreen(SnailsPace game)
            : base(game)
        {
        }

        #region Graphics Stuff
        protected override void LoadContent()
        {
			// TODO pass in a map
			engine = new GameEngine("");
			base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
			engine.render(gameTime);
			base.Draw(gameTime);
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
			engine.think(gameTime);
			engine.physics(gameTime);
            base.Update(gameTime);
        }
    }
}
