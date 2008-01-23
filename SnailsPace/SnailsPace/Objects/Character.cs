using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
    class Character : GameObject
    {
        // Character properties.
        public int health;
		public Boolean horizontalFlip;

        // Character AI to be overridden by Lua
        public void think(GameTime gameTime)
        {
        }
    }
}
