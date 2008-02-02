using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;
using SnailsPace.Objects;
using LuaInterface;

namespace SnailsPace.Objects
{
    class Trigger
    {
        // Trigger location and size
        public Rectangle location;

        // Object state for Lua
        public LuaTable state;

        public Trigger()
        {
            //Engine.map.triggers.Add(this);
        }

        // Trigger the trigger
        public void trigger(Character character)
        {
            Engine.lua.CallOn(state, "trigger", character, Engine.gameTime);
        }
    }
}
