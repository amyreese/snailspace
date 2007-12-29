using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Snail_s_Pace.Screens
{
    abstract class LoadingScreen : Screen
    {
        private SnailsPace.GameStates nextState;
        protected LoadingScreen(Game game, SnailsPace.GameStates nextState) : base( game )
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
