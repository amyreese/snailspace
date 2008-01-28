using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
    class Helix : Character
    {
        // Jetpack fuel
        public float fuel;
		public float maxFuel;
		public int fireCooldown;
		public bool flying;

		public void setSprite(String sprtName, String aSprtName)
		{
			Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = this.sprites.Values.GetEnumerator();
			while (sprtEnumerator.MoveNext())
			{
				sprtEnumerator.Current.visible = false;
			}
			this.sprites[sprtName].visible = true;
			this.sprites[aSprtName].visible = true;
		}

        public override void think(GameTime gameTime)
        {
			float fuelMod = (float)Math.Min(1, gameTime.ElapsedRealTime.TotalSeconds);
			if (flying)
			{
				fuel -= fuelMod * (1 + velocity.Length());
				if (fuel < 0)
				{
					fuel = 0;
				}
			}
			else
			{
				fuel += fuelMod * 10;
				if (fuel > maxFuel)
				{
					fuel = maxFuel;
				}
			}
#if DEBUG
			if( SnailsPace.debugFlying ) {
				if (flying)
				{
					SnailsPace.debug("Flying!");
				}
				SnailsPace.debug("Fuel: " + fuel);
			}
#endif
        }

		public override bool canCollideWith(GameObject otherObject)
		{
			if ((otherObject is Objects.Bullet) && (((Bullet)otherObject).isPCBullet))
			{
				return false;
			}
			else
			{
				return base.canCollideWith(otherObject);
			}
		}

    }
}
