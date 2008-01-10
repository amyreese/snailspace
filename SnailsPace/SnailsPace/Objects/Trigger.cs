using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Snail_s_Pace.Objects
{
    class Trigger
    {
        // Trigger location and size
        public Vector2 position;
        public Vector2 size;

        // Trigger function
        public String function;

        // Trigger the trigger
        public void trigger(Character character)
        {
            // TODO: call the appropriate Lua function
        }
    }
}
