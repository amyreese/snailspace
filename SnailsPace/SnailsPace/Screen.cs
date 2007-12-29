using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace A_Snail_s_Pace
{
    abstract class Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Screen(Game game) : base(game) 
        {
        }

        public abstract bool ready();
    }
}
