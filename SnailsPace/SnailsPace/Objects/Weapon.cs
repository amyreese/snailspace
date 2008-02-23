using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;
using LuaInterface;

namespace SnailsPace.Objects
{
    class Weapon
    {
		// Name of this weapon.
        public String name;

		// Inventory slot this weapon occupies.
        public int slot = 0;

        public Sprite sprite; // Only used when rendering Helix
        public Sprite bulletSprite;
        public Sprite hudSprite;

		// Gun properties
		public double lastFired = 0;
        public double cooldown = 100;
        public int ammunition = -1;

        public String cue = "gun1";
        public String hudicon = "Resources/Textures/HealthIcon";

        public LuaTable state;

		/// <summary>
		/// Default constructor.
		/// </summary>
        public Weapon()
        {
        }

		/// <summary>
		/// Load a weapon from Lua with the specified name.
		/// </summary>
		/// <param name="name">The name of the weapon.</param>
		/// <returns>The weapon object.</returns>
        public static Weapon load(String name)
        {
            try
            {
                Weapon weapon = new Weapon();
                Engine.lua.CallOn((LuaTable)Engine.lua["Weapons"], name, weapon);
                return weapon;
            }
            catch (LuaException e)
            {
                SnailsPace.debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Shoot a bullet directly at the specified target.
        /// </summary>
        /// <param name="targetPosition">The bullet's target</param>
        /// <param name="gameTime">The current time, used for firing rate</param>
        public void ShootAt(Character shooter, Vector2 targetPosition, GameTime gameTime)
        {
            if ((ammunition > 0 || ammunition == -1) && lastFired + cooldown < gameTime.TotalRealTime.TotalMilliseconds)
            {
                lastFired = gameTime.TotalRealTime.TotalMilliseconds;

                Engine.lua.CallOn(state, "ShootAt", shooter, targetPosition, gameTime);
                if ((ammunition > 0) && (!SnailsPace.cheatInfiniteAmmo)) ammunition--;

                if (shooter is Helix)
                {
                    Engine.sound.play(cue);
                }
            }
        }
    }
}
