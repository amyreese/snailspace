using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;

namespace SnailsPace.Objects
{
    class Helix : Character
    {
        // Jetpack fuel
        public float fuel;
		public float maxFuel;
		public bool flying;
		public bool boosting;
		public bool thrust;
        public bool thrustable;
		public bool repelling;
		public const float flyingHorizontalFriction = 640.0f;
		public const float walkingHorizontalFriction = 2560.0f;
		public const float flyingAcceleration = 2560.0f;
		public const float walkingAcceleration = 3840.0f;
		public const float flyingMaxVelocity = 640.0f;
		public const float walkingMaxVelocity = 384.0f;

		public double lastTookDamage;
		public double invincibilityPeriod = 100;
		public double boostPeriod = 0;
		public double repelPeriod = 0;

        public Weapon[] inventory;

        public Helix( Vector2 position )
            : this(position, "generic")
        {
        }

        public Helix( Vector2 position, String weaponName)
            : base(weaponName)
        {
            try
            {
                Engine.lua["helix"] = this;
            }
            catch (LuaInterface.LuaException e)
            {
                SnailsPace.debug(e.Message);
            }
            
            name = "Helix";
            affectedByGravity = true;
            
            Objects.Sprite walk = new Objects.Sprite();
            walk.image = new Objects.Image();
            walk.image.filename = "Resources/Textures/HelixTable";
            walk.image.blocks = new Vector2(4.0f, 4.0f);
            walk.image.size = new Vector2(128.0f, 128.0f);
            walk.visible = true;
            walk.effect = "Resources/Effects/effects";

            Objects.Sprite fly = new Objects.Sprite();
            fly.image = walk.image;
            fly.visible = false;
            fly.effect = walk.effect;

            sprites.Add("Walk", walk);
            sprites["Walk"].animationStart = 0;
            sprites["Walk"].animationEnd = 7;
            sprites["Walk"].frame = 0;
            sprites["Walk"].animationDelay = 1.0f / 15.0f;
            sprites["Walk"].timer = 0f;

            sprites.Add("Fly", fly);
            sprites["Fly"].animationStart = 8;
            sprites["Fly"].animationEnd = 11;
            sprites["Fly"].frame = 8;
            sprites["Fly"].animationDelay = 1.0f / 15.0f;
            sprites["Fly"].timer = 0f;

            // TODO: Make hover it's own animation
            sprites.Add("Hover", fly.clone());
            sprites["Hover"].animationStart = 12;
            sprites["Hover"].animationEnd = 12;
            sprites["Hover"].frame = 12;
            sprites["Hover"].animationDelay = 1.0f / 7.0f;
            sprites["Hover"].timer = 0f;

			sprites.Add("NoFuel", fly.clone());
			sprites["NoFuel"].animationStart = 12;
			sprites["NoFuel"].animationEnd = 13;
			sprites["NoFuel"].frame = 12;
			sprites["NoFuel"].animationDelay = 1.0f / 4.0f;
			sprites["NoFuel"].timer = 0f;
			
			maxVelocity = walkingMaxVelocity;
            maxFuel = 30;
            fuel = maxFuel;
            layer = 0;
            
            size = walk.image.size;
            Vector2[] points = new Vector2[11];
            points[0] = new Vector2(0.0f, 36.0f);
            points[1] = new Vector2(-27.0f, 25.0f);
            points[2] = new Vector2(-38.0f, 14.0f);
            points[3] = new Vector2(-47.0f, -34.0f);
            points[4] = new Vector2(29.0f, -34.0f);
            points[5] = new Vector2(53.0f, -13.0f);
            points[6] = new Vector2(51.0f, 8.0f);
            points[7] = new Vector2(39.0f, 8.0f);
            points[8] = new Vector2(28.0f, -9.0f);
            points[9] = new Vector2(27.0f, 6.0f);
            points[10] = new Vector2(21.0f, 23.0f);
            bounds = new Objects.GameObjectBounds(points);
            
            this.position = position;
            startPosition = position;

			maxHealth = 20;
			health = 20;

			Weapon wep = weapon;
            inventory = new Weapon[5];
			if (weaponName != "generic")
            {
				AddWeapon(Weapon.load("generic"));
            }
			AddWeapon(wep);
		}

		public void AddWeapon(Weapon gun)
		{
			inventory[gun.slot] = gun;
			weapon = gun;
		}

        public void NextWeapon()
        {
            int i = 0, s = weapon.slot;
            while (i < inventory.Length)
            {
                do
                {
                    s = ++s >= inventory.Length ? 0 : s;
                    weapon = inventory[s];
                    i++;
                } while (weapon == null);
                
                if (weapon.ammunition == -1 || weapon.ammunition > 0)
                {
                    break;
                }
            }
        }

		public void setSprite(params String[] spriteNames)
		{
			Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = sprites.Values.GetEnumerator();
			while (sprtEnumerator.MoveNext())
			{
				sprtEnumerator.Current.visible = false;
			}
            sprtEnumerator.Dispose();

            for (int i = 0; i < spriteNames.Length; i++)
            {
                sprites[spriteNames[i]].visible = true;
            }
		}

        public override void think(GameTime gameTime)
        {
			if (fuel > maxFuel)
			{
				fuel = maxFuel;
			}
			if (health > maxHealth)
			{
				health = maxHealth;
			}
            float fuelMod = (float)Math.Min(1, gameTime.ElapsedRealTime.TotalSeconds);
			if (flying)
			{
                if (thrust)
                {
                    fuel -= fuelMod * (6 * direction.Length());
                }
                else
                {
                    fuel += fuelMod * 1;
                }
			}
			else
			{
				setSprite("Walk");
				fuel += fuelMod * 15;
				if (fuel > maxFuel)
				{
					fuel = maxFuel;
				}
			}

            if (thrustable)
            {
                if (fuel < 0)
                {
                    fuel = 0;
                    thrustable = false;
                }
            }
            else
            {
                if (fuel > 1)
                {
                    thrustable = true;
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

            // Deal with Helix's movement
            Input input = SnailsPace.inputManager;

			direction = Vector2.Zero;

			thrust = false;

            if (input.inputDown("Left") && input.inputDown("Right"))
            {
                // do nothing
            }
            else if (input.inputDown("Left"))
            {
                if (!flying || thrustable)
                {
					direction.X = -1;
                    horizontalFlip = true;
                    thrust = true;
                }
                else if (!thrustable)
                {
                    direction.X = -0.2f;
                    horizontalFlip = true;
                    thrust = true;
                }
            }
            else if (input.inputDown("Right"))
            {
                if (!flying || thrustable)
                {
					direction.X = 1;
                    horizontalFlip = false;
                    thrust = true;
                }
                else if (!thrustable)
                {
                    direction.X = 0.2f;
                    horizontalFlip = false;
                    thrust = true;
                }
            } 
			

            if (input.inputDown("Up") && input.inputDown("Down"))
            {
                //do nothing
            }
            else if (input.inputDown("Up"))
            {
                if (thrustable)
                {
                    direction.Y = 1;
                    thrust = true;
                }
            }
			else if (input.inputDown("Down"))
			{
                if (thrustable)
                {
                    direction.Y = -1;
                    thrust = true;
                }
			}


			// Sprites & Animations
			if (flying)
			{
                if (direction.Y == 0 && direction.X == 0)
				{
					// TODO: Hover
					setSprite("Hover");
					sprites["Hover"].animate(gameTime);
				}
				else
				{
					setSprite("Fly");
					sprites["Fly"].animate(gameTime);
				}
			}
			else
			{
				if (direction.X != 0)
				{
					sprites["Walk"].animate(gameTime);
				}
			}

            if (fuel <= 6)
            {
                Engine.sound.playRepeat("alarm");
                sprites["NoFuel"].visible = true;

                if (thrustable)
                {
                    sprites["NoFuel"].animate(gameTime);
                }
                else
                {
                    sprites["NoFuel"].frame = sprites["NoFuel"].animationEnd;
                }
            }
            else
            {
                Engine.sound.stop("alarm");
            }

            if (flying && thrust)
            {
                Engine.sound.playRepeat("jetpack");
            }
            else
            {
                Engine.sound.stop("jetpack");
            }

			// Acceleration
			if (flying)
			{
    			acceleration = flyingAcceleration;
				horizontalFriction = flyingHorizontalFriction;
				maxVelocity = flyingMaxVelocity;
			}
			else
			{
				acceleration = walkingAcceleration;
				horizontalFriction = walkingHorizontalFriction;
				maxVelocity = walkingMaxVelocity;
			}

            if (boosting)
            {
                boostPeriod -= gameTime.ElapsedRealTime.TotalSeconds;
                if (boostPeriod > 0)
                {
                    acceleration = (float)(acceleration * (1 + boostPeriod));
                    horizontalFriction = 0;
                }
                if (boostPeriod < -0.5)
                {
                    boosting = false;
                }
                else
                {
                    maxVelocity = (float)(maxVelocity * (1 + 2 * (0.5 + boostPeriod)));
                    desiredMaxVelocity = maxVelocity;
                }
            }

			if (repelling)
			{
				repelPeriod -= gameTime.ElapsedRealTime.TotalSeconds;

				// TODO: Make helix bounce away from the enemy.
				if (repelPeriod > 0)
				{

				}
				if (repelPeriod < -0.5)
				{

				}
				else
				{

				}

			}

            GameObject crosshair = Player.crosshair;
            if (input.inputDown("Fire"))
            {
				ShootAt(crosshair.position, gameTime);
            }

            if (input.inputPressed("WeaponNext") || weapon.ammunition == 0)
            {
                NextWeapon();
            }
        }

		public override bool canCollideWith(GameObject otherObject)
		{
			if ((otherObject is Objects.Bullet) && (((Bullet)otherObject).isPCBullet))
				return false;
			
			return base.canCollideWith(otherObject);
		}

		public override void collidedWith(GameObject otherObject)
		{
			if (otherObject is Objects.Character)
			{
				base.takeDamage();
				repelling = true;
				repelPeriod = 0.5;
			}
			else
			{
				base.collidedWith(otherObject);
			}
		}

		public override void takeDamage(int damage)
		{
			if (lastTookDamage + invincibilityPeriod < Engine.gameTime.TotalRealTime.TotalMilliseconds)
			{
				base.takeDamage(damage);
				lastTookDamage = Engine.gameTime.TotalRealTime.TotalMilliseconds;
			}
		}

    }
}
