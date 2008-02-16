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
        public Explosion explosion;
        public bool destroy = true;
        public bool hit = false;

		public bool isPCBullet = false;

		/// <summary>
		/// Create a new bullet.
		/// </summary>
		public Bullet() : base()
		{
			horizontalFriction = 0;
		}

		/// <summary>
		/// Decide if a collision can occur between this bullet and another object.
		/// </summary>
		/// <param name="otherObject">The object that this bullet collided with.</param>
		/// <returns>Whether or not a collision should occur.</returns>
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

		/// <summary>
		/// Action performed when this bullet collides with another object.
		/// </summary>
		/// <param name="otherObject">The object that this bullet collided with.</param>
		public override void collidedWith(GameObject otherObject)
		{
			// Assumes canCollideWith
            if (otherObject is Character)
			{
                if (isPCBullet)
                {
                    Engine.sound.play("squash");
                }
                else
                {
                    Engine.sound.play("ping");
                }
                
                if (!hit)
                {
                    ((Character)otherObject).takeDamage(damage);
                    hit = true;
                }
                else
                {
                    ((Character)otherObject).takeDamage(damage / 4);
                }
			}
			else if (otherObject is GameObject && otherObject.name == "fallingPlatform")
			{
				((GameObject)otherObject).affectedByGravity = true;
				if (((GameObject)otherObject).sprites.ContainsKey("Pour"))
				{
					((GameObject)otherObject).sprites["Pour"].visible = false;
				}
			}
		}
	}
}
