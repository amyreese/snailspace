using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SnailsPace.Screens
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
            set
            {
                _ready = value;
            }
        }
    }
}
