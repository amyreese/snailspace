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
        private bool started = false;

        public GameScreen(SnailsPace game)
            : base(game)
        {
        }

        #region Graphics Stuff
        protected override void LoadContent()
        {
            ReloadEngine();
			base.LoadContent();
        }

        public void ReloadEngine()
        {
            ReloadEngine(MAIN_MAP);
        }

        public void ReloadEngine( String map )
        {
            this.map = map;
            new System.Threading.Thread(loadEngine).Start();
        }

        private const string MAIN_MAP = "Garden2";
        public String map = "Garden";
        protected void loadEngine()
        {
            engine = new Engine(map);
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
            if (!started)
            {
                Engine.player.load();
                started = true;
            }
			engine.think(gameTime);
			engine.physics(gameTime);
            base.Update(gameTime);
        }
    }
}
