using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Screens
{
    abstract class LoadingScreen : Screen
    {
        protected SnailsPace.GameStates nextState;
        protected LoadingScreen(SnailsPace game, SnailsPace.GameStates nextState)
            : base(game)
        {
            this.nextState = nextState;
            ready = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (snailsPace.getScreen(nextState).ready && ready )
            {
                snailsPace.changeState(nextState);
            }
        }
    }
}
