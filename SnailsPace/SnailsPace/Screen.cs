using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace A_Snail_s_Pace
{
    abstract class Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SnailsPace snailsPace;

        protected Screen(SnailsPace game)
            : base(game)
        {
            snailsPace = game;
        }

        private bool _ready = false;
        public bool ready
        {
            get
            {
                return _ready;
            }
            protected set
            {
                _ready = value;
            }
        }
    }
}
