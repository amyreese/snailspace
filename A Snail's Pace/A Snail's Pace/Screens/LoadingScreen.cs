using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Snail_s_Pace.Screens
{
    abstract class LoadingScreen : Screen
    {
        private SnailsPace.GameStates nextState;
        protected LoadingScreen(SnailsPace.GameStates nextState)
        {
            this.nextState = nextState;
        }

        public override void Update(GameTime gameTime)
        {
            if ( SnailsPace.getInstance().getScreen(nextState).ready())
            {
                SnailsPace.getInstance().changeState(nextState);
            }
        }
    }
}
