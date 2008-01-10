using System;
using System.Collections.Generic;
using System.Text;

namespace A_Snail_s_Pace.Objects
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
