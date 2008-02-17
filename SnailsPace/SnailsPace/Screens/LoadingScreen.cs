using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Screens
{
    /// <summary>
    /// A screen that waits for another screen to be ready before transitioning to that screen
    /// </summary>
    abstract class LoadingScreen : Screen
    {
        /// <summary>
        /// The game state that the loading screen transitions into
        /// </summary>
        protected SnailsPace.GameStates nextState;

        /// <summary>
        /// Sets up the loading screen
        /// </summary>
        /// <param name="game">The Snails Pace instance</param>
        /// <param name="nextState">The game state that the loading screen transitions into</param>
        protected LoadingScreen(SnailsPace game, SnailsPace.GameStates nextState)
            : base(game)
        {
            this.nextState = nextState;
            ready = true;
        }

        /// <summary>
        /// Checks to see if the next screen is ready, and if the loading screen is ready
        /// If they're both ready, tells Snails Pace to move to the next screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (snailsPace.getScreen(nextState).ready && ready )
            {
                snailsPace.changeState(nextState);
            }
        }
    }
}
