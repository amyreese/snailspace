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
    /// <summary>
    /// The screen for the actual game
    /// </summary>
    class GameScreen : Screen
    {
        // The game engine being used
		private Engine engine;

        // Has the game has been started?
        private bool started = false;

        /// <summary>
        /// Constructor for the screen
        /// </summary>
        /// <param name="game">Snails Pace Instance</param>
        public GameScreen(SnailsPace game)
            : base(game)
        {
            ready = false;
        }

        #region Engine Loading
        /// <summary>
        /// Reload the engine, using the specified map
        /// </summary>
        /// <param name="map">The map to load</param>
        public void ReloadEngine( String map )
        {
			ready = false;
            started = false;
            this.map = map;
            new System.Threading.Thread(loadEngine).Start();
        }

        // The current map
        public String map;

        /// <summary>
        /// Start up the engine using the current map
        /// </summary>
        protected void loadEngine()
        {
            engine = new Engine(map);
            ready = true;
        }
        #endregion

        /// <summary>
        /// Tell the engine that rendering should be done
        /// </summary>
        /// <param name="gameTime">GameTime for this draw</param>
        public override void Draw(GameTime gameTime)
        {
			engine.render(gameTime);
			base.Draw(gameTime);
        }

        /// <summary>
        /// If the game hasn't started yet, start it.
        /// Tell the engine to run the AI and physics
        /// </summary>
        /// <param name="gameTime">GameTime for this update</param>
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
