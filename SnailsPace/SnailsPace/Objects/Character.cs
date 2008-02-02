using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;

namespace SnailsPace.Objects
{
    class Character : GameObject
    {
        // Character properties.
        private int _health;
		public int health {
			get 
			{
				return _health;
			}
			set
			{
				_health = value;
				if (value <= 0)
				{
					SnailsPace.debug("Character died.");
					// TODO handle death
				}
			}
		}

		public double lastFired;
		public double coolDown = 100;

		public void ShootAt(Vector2 targetPosition, GameTime gameTime)
		{
			if (lastFired + coolDown < gameTime.TotalRealTime.TotalMilliseconds)
			{
				Objects.Bullet bullet = new Objects.Bullet();
				bullet.velocity = targetPosition - position;
				bullet.velocity.Normalize();
				bullet.rotation = ((targetPosition.X - position.X) < 0 ? MathHelper.Pi : 0) + (float)Math.Atan((targetPosition.Y - position.Y) / (targetPosition.X - position.X));
				bullet.position = position + Vector2.Multiply(bullet.velocity, Math.Max(size.X, size.Y) / 2);
				bullet.maxVelocity = Math.Max(terminalVelocity, maxVelocity) + 128.0f;
				bullet.velocity = Vector2.Multiply(bullet.velocity, bullet.maxVelocity);
				bullet.layer = -0.001f;
				bullet.isPCBullet = this is Helix;
				bullet.damage = 1;
				Engine.bullets.Add(bullet);
				lastFired = gameTime.TotalRealTime.TotalMilliseconds;
			}
		}

        String thinker = "";
        
        public virtual void think(GameTime gameTime)
        {
            if (thinker.Length > 0)
            {
                Engine.lua.Call(thinker, this, gameTime);
            }
        }

		public override bool canCollideWith(GameObject otherObject)
		{
			if ((otherObject is Objects.Bullet) && !(((Bullet)otherObject).isPCBullet))
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
