using System;
using System.Collections.Generic;
using System.Text;

namespace SnailsPace.Objects
{
    class Bullet : GameObject
    {
        // Bullet characteristics.
        public int damage;

		public bool isPCBullet = false;

		public override bool collidedWith(GameObject otherObject)
		{
			if (otherObject == null)
			{
				return true;
			}
			else if (otherObject.GetType() == typeof(Helix))
			{
				if (isPCBullet)
				{
					return false;
				}
				else
				{
					((Character)otherObject).health -= damage;
				}
			}
			else if (otherObject.GetType() == typeof(Character))
			{
				if (!isPCBullet)
				{
					return false;
				}
				else
				{
					((Character)otherObject).health -= damage;
				}
			}
			return true;
		}
	}
}
