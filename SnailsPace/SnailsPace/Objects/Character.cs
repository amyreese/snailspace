using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace.Objects
{
    class Character : GameObject
    {
        // Character properties.
        public int health;

        // Character AI to be overridden by Lua
        public void think()
        {
        }
    }
}
