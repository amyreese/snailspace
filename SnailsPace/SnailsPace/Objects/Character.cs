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
		#region Character properties
		public int maxHealth;
        private float _health;
		public float health {
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
		#endregion

		/// <summary>
		/// Create a character with a generic weapon.
		/// </summary>
        public Character()
            : this("generic")
        {
        }

		/// <summary>
		/// Create a character with a specific weapon.
		/// </summary>
		/// <param name="weaponName">The specific weapon for this character.</param>
        public Character(String weaponName)
            : base()
        {
            weapon = Weapon.load(weaponName);
        }

		/// <summary>
		/// Shoot at a specifc target.
		/// </summary>
		/// <param name="targetPosition"></param>
		/// <param name="gameTime"></param>
        public void ShootAt(Vector2 targetPosition, GameTime gameTime)
        {
            weapon.ShootAt(this, targetPosition, gameTime);
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
        public virtual void takeDamage(float damage)
        {
            takeDamage(damage, true);
        }

		/// <summary>
		/// Make this character take a specified amount of damage.
		/// </summary>
		/// <param name="damage">The amount of damage.</param>
		/// <param name="initialHit">True if this is a hit to be counted by statistics, false if not (for multi-hit weapons).</param>
        public virtual void takeDamage(float damage, bool initialHit)
        {
            if (this is Helix)
            {
                Engine.player.gotHit();
            }
            else if( initialHit )
            {
                Engine.player.enemyHit();
            }
			health -= damage;
		}
    }
}
