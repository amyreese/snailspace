using System;
using System.Collections.Generic;
using System.Text;
using SnailsPace.Core;

namespace SnailsPace.Objects
{
    class Bullet : GameObject
    {
        // Bullet characteristics.
        public int damage;

		public bool isPCBullet = false;

		public Bullet() : base()
		{
			horizontalFriction = 0;
		}

		public override bool canCollideWith(GameObject otherObject)
		{
			// Note: This should be in the order of most specific to least specific (ie: Helix before character), otherwise more specific cases will be missed
			if (otherObject == null)
			{
				return true;
			}
			else if (otherObject is Helix)
			{
				if (isPCBullet)
				{
					return false;
				}
			}
			else if (otherObject is Character)
			{
				if (!isPCBullet)
				{
					return false;
				}
			}
			else if (otherObject is Objects.Bullet)
			{
				return false;
			}
			return base.canCollideWith(otherObject);
		}

		public override void collidedWith(GameObject otherObject)
		{
			// Assumes canCollideWith
			if (otherObject is Character)
			{
				((Character)otherObject).takeDamage(damage);
			}
		}
	}
}
