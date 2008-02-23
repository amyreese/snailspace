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
        public Vector2 position;
        public GameObjectBounds bounds;

        // Object state for Lua
        public LuaTable state;
        public bool inside = false;

		/// <summary>
		/// Default constructor.
		/// </summary>
        public Trigger()
        {
            //Engine.map.triggers.Add(this);
        }

        /// <summary>
        /// Trigger the trigger.
        /// </summary>
        /// <param name="character">The character that triggered the trigger.</param>
        public void trigger(Character character)
        {
            Engine.lua.CallOn(state, "trigger", character, Engine.gameTime);
        }

		/// <summary>
		/// Called when a character is inside of a trigger.
		/// </summary>
		/// <param name="character">The character inside the trigger.</param>
        public void triggerIn(Character character)
        {
            inside = true;
            Engine.lua.CallOn(state, "triggerIn", character, Engine.gameTime);
        }

		/// <summary>
		/// Called when a characer exits a trigger.
		/// </summary>
		/// <param name="character"></param>
        public void triggerOut(Character character)
        {
            inside = false;
            Engine.lua.CallOn(state, "triggerOut", character, Engine.gameTime);
        }
    }
}
