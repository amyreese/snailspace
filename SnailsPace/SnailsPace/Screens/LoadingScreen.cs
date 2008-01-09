using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Snail_s_Pace.Screens
{
    abstract class LoadingScreen : Screen
    {
        private SnailsPace.GameStates nextState;
        protected LoadingScreen(SnailsPace game, SnailsPace.GameStates nextState)
            : base(game)
        {
            this.nextState = nextState;
        }

        public override void Update(GameTime gameTime)
        {
            if (snailsPace.getScreen(nextState).ready)
            {
                snailsPace.changeState(nextState);
            }
        }
    }
}
