using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;

namespace SnailsPace.Objects
{
    class Character : GameObject
    {
		public static Objects.Sprite bulletSprite;

		// Character properties.
		public int maxHealth;
        private int _health;
		public int health {
			get 
			{
				return _health;
			}
			set
			{
				_health = value;
			}
		}

		public double lastFired;
		public double coolDown = 100;

		public void ShootAt(Vector2 targetPosition, GameTime gameTime)
		{
			if (lastFired + coolDown < gameTime.TotalRealTime.TotalMilliseconds)
			{
				Objects.Bullet bullet = new Objects.Bullet();
				if (bulletSprite == null)
				{
					bulletSprite = new Objects.Sprite();
					bulletSprite.image = new Objects.Image();
					bulletSprite.image.filename = "Resources/Textures/Bullet";
					bulletSprite.image.blocks = new Vector2(1.0f, 1.0f);
					bulletSprite.image.size = new Vector2(16.0f, 8.0f);
					bulletSprite.visible = true;
					bulletSprite.effect = "Resources/Effects/effects";
				}
				bullet.sprites.Add("Bullet", bulletSprite);
				bullet.size = bulletSprite.image.size;
				bullet.velocity = targetPosition - position;
				bullet.velocity.Normalize();
				bullet.rotation = ((targetPosition.X - position.X) < 0 ? MathHelper.Pi : 0) + (float)Math.Atan((targetPosition.Y - position.Y) / (targetPosition.X - position.X));
				bullet.position = position + Vector2.Multiply(bullet.velocity, Math.Max(size.X, size.Y) / 2);
				bullet.maxVelocity = Math.Max(terminalVelocity, maxVelocity) + 128.0f;
				bullet.velocity = Vector2.Multiply(bullet.velocity, bullet.maxVelocity);
				bullet.layer = -0.001f;
				bullet.isPCBullet = this is Helix;
                if (bullet.isPCBullet)
                {
                    Engine.player.shotBullet();
                }
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
				return false;

			return base.canCollideWith(otherObject);
		}

		public override void collidedWith(GameObject otherObject)
		{
            if (otherObject is Objects.Helix)
            {
                ((Helix)otherObject).takeDamage();
            }
            else
            {
                base.collidedWith(otherObject);
            }
		}

		public void takeDamage()
		{
			takeDamage(1);
		}

		public virtual void takeDamage(int damage)
		{
            if (this is Helix)
            {
                Engine.player.gotHit();
            }
            else
            {
                Engine.player.enemyHit();
            }
			health -= damage;
		}
    }
}
