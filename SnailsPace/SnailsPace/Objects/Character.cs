using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;
using LuaInterface;

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

		/// <summary>
		/// Shoot a bullet directly at the specified target.
		/// </summary>
		/// <param name="targetPosition">The bullet's target</param>
		/// <param name="gameTime">The current time, used for firing rate</param>
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

		/// <summary>
		/// Shoot a fan pattern at a specified target.
		/// TODO: Make this work in more directions than straight down.
		/// </summary>
		/// <param name="targetPosition">The bullet's target</param>
		/// <param name="numBullets">The number of bullets in the fan</param>
		/// <param name="offset">How much to spread the bullets</param>
		/// <param name="gameTime">The current time, used for firing rate</param>
		public void ShootFanAt(Vector2 targetPosition, int numBullets, float offset, GameTime gameTime)
		{
			if (lastFired + coolDown < gameTime.TotalRealTime.TotalMilliseconds)
			{
				if (numBullets % 2 == 0)
					targetPosition.X -= offset * (numBullets / 2) - (offset / 2.0f);
				else
					targetPosition.X -= offset * (numBullets / 2);
				
				for (int i = 0; i < numBullets; i++)
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
					bullet.velocity /= numBullets;
					bullet.layer = -0.001f;
					bullet.isPCBullet = this is Helix;
					if (bullet.isPCBullet)
					{
						Engine.player.shotBullet();
					}
					bullet.damage = 1;
					Engine.bullets.Add(bullet);
					lastFired = gameTime.TotalRealTime.TotalMilliseconds;
					targetPosition.X += offset;
				}
			}
		}

		String thinker = "";
        
		/// <summary>
		/// Run this character's AI.
		/// </summary>
		/// <param name="gameTime">The current time.</param>
        public virtual void think(GameTime gameTime)
        {
            if (thinker.Length > 0)
            {
                Engine.lua.Call(thinker, this, gameTime);
            }
        }

		/// <summary>
		/// Decide if a collision can occur between this character and another object.
		/// </summary>
		/// <param name="otherObject">The object that this character collided with.</param>
		/// <returns>Whether or not a collision should occur.</returns>
		public override bool canCollideWith(GameObject otherObject)
		{
            if ((otherObject is Objects.Bullet) && !(((Bullet)otherObject).isPCBullet))
            {
                return false;
            }
            else if (otherObject is Character && !(otherObject is Helix || this is Helix))
            {
                return false;
            }

			return base.canCollideWith(otherObject);
		}

		/// <summary>
		/// Action performed when this character collides with another object.
		/// </summary>
		/// <param name="otherObject">The object that this character collided with.</param>
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

		/// <summary>
		/// Make this character take 1 damage.
		/// </summary>
		public void takeDamage()
		{
			takeDamage(1);
		}

		/// <summary>
		/// Make this character take a specified amount of damage.
		/// </summary>
		/// <param name="damage">The amount of damage.</param>
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
