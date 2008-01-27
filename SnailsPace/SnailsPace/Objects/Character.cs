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

		public double lastFired;
		public double coolDown = 100;

        String thinker = "";
        
        public virtual void think(GameTime gameTime)
        {
            if (thinker.Length > 0)
            {
                Engine.lua.Call(thinker, this, gameTime);
            }
        }
    }
}
