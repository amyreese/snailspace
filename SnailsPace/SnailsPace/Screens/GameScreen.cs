using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SnailsPace.Core;

namespace SnailsPace.Screens
{
    class GameScreen : Screen
    {
		private Engine engine;

        public GameScreen(SnailsPace game)
            : base(game)
        {
        }

        #region Graphics Stuff
        protected override void LoadContent()
        {
            new System.Threading.Thread(loadEngine).Start();
			base.LoadContent();
            
        }

        protected void loadEngine()
        {
            // TODO pass in a map
            engine = new Engine("Garden");
            this.ready = true;
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
