using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;

namespace SnailsPace.Objects
{
    class Character : GameObject
    {
        // Character properties.
        private int _health;
		public int health {
			get 
			{
				return _health;
			}
			set
			{
				_health = value;
				if (value <= 0)
				{
					SnailsPace.debug("Character died.");
					// TODO handle death
				}
			}
		}

        // Character AI to be overridden by Lua
        public virtual void think(GameTime gameTime)
        {
            Engine.lua.Call(this, "_think", gameTime);
        }
    }
}
