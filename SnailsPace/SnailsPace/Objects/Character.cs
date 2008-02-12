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

        public Weapon weapon;

		String thinker = "";

        public Character()
            : this("generic")
        {
        }

        public Character(String weaponName)
            : base()
        {
            weapon = Weapon.load(weaponName);
        }

        public void ShootAt(Vector2 targetPosition, GameTime gameTime)
        {
            weapon.ShootAt(this, targetPosition, gameTime);
        }

        public void ShootFanAt(Vector2 targetPosition, int numBullets, float offset, GameTime gameTime)
        {
            //weapon.ShootFanAt(targetPosition, numBullets, offset, gameTime);
        }
                
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
