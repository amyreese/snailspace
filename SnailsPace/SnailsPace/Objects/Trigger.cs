using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using LuaInterface;

namespace SnailsPace.Objects
{
    class Trigger
    {
        // Trigger location and size
        public Vector2 position;
        public Vector2 size;

        // Object state for Lua
        public LuaTable state;

        // Trigger function
        public String function;

        // Trigger the trigger
        public void trigger(Character character)
        {
            // TODO: call the appropriate Lua function
        }
    }
}
