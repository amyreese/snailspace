using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
    class Helix : Character
    {
        // Jetpack fuel
        public int fuel;
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
			if (flying)
			{
				SnailsPace.debug("Flying!");
			}
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
