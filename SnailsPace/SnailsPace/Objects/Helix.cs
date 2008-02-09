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
		public bool thrust;
		public const float flyingHorizontalFriction = 640.0f;
		public const float walkingHorizontalFriction = 2560.0f;
		public const float flyingAcceleration = 2560.0f;
		public const float walkingAcceleration = 3840.0f;
		public const float flyingMaxVelocity = 640.0f;
		public const float walkingMaxVelocity = 384.0f;

		public double lastTookDamage;
		public double invincibilityPeriod = 100;

        public Helix( Vector2 position ) : base()
        {
            Engine.lua["helix"] = this;
            
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

            Objects.Sprite gun = new Objects.Sprite();
            gun.image = walk.image;
            gun.visible = true;
            gun.effect = walk.effect;
            gun.layerOffset = -1.0f;

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
			sprites["NoFuel"].animationDelay = 1.0f / 2.0f;
			sprites["NoFuel"].timer = 0f;
			
			sprites.Add("Gun", fly.clone());
            sprites["Gun"].animationStart = 15;
            sprites["Gun"].animationEnd = 15;
            sprites["Gun"].frame = 15;
            sprites["Gun"].animationDelay = 1.0f / 15.0f;
            sprites["Gun"].timer = 0f;

            maxVelocity = walkingMaxVelocity;
            maxFuel = 20;
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
		}

		public void setSprite(String sprtName, String aSprtName)
		{
			Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = sprites.Values.GetEnumerator();
			while (sprtEnumerator.MoveNext())
			{
				sprtEnumerator.Current.visible = false;
			}
			sprites[sprtName].visible = true;
			sprites[aSprtName].visible = true;
		}

        public override void think(GameTime gameTime)
        {
			float fuelMod = (float)Math.Min(1, gameTime.ElapsedRealTime.TotalSeconds);
			if (flying)
			{
				if (thrust)
				{
					fuel -= fuelMod * (3 * direction.Length());
					if (fuel < 0)
					{
						fuel = 0;
					}
					if (fuel > 0)
					{
						//affectedByGravity = false;
					}
				}
			}
			else
			{
				setSprite("Walk", "Gun");
				fuel += fuelMod * 10;
				if (fuel > maxFuel)
				{
					fuel = maxFuel;
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
                if (!flying || fuel > 0)
                {
					direction.X = -1;
                    horizontalFlip = true;
                    sprites["Gun"].horizontalFlip = true;
                    thrust = true;
                }
            }
            else if (input.inputDown("Right"))
            {
                if (!flying || fuel > 0)
                {
					direction.X = 1;
                    horizontalFlip = false;
                    sprites["Gun"].horizontalFlip = false;
                    thrust = true;
                }
            } 
			

            if (input.inputDown("Up") && input.inputDown("Down"))
            {
                //do nothing
            }
            else if (input.inputDown("Up"))
            {
                if (fuel > 0)
                {
					direction.Y = 1;
					thrust = true;
                }
            }
			else if (input.inputDown("Down"))
			{
				if (fuel > 0)
				{
					direction.Y = -1;
                    thrust = true;
				}

			}


			// Sprites & Animations
			if (flying)
			{
				if (fuel <= 0)
				{
					setSprite("NoFuel", "Gun");
					sprites["NoFuel"].animate(gameTime);
				}
				else if (direction.Y == 0 && direction.X == 0)
				{
					// TODO: Hover
					setSprite("Hover", "Gun");
					sprites["Hover"].animate(gameTime);
				}
				else
				{
					setSprite("Fly", "Gun");
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

            GameObject crosshair = Player.crosshair;
            sprites["Gun"].rotation = ((crosshair.position.X - position.X) < 0 ? MathHelper.Pi : 0) + (float)Math.Atan((crosshair.position.Y - position.Y) / (crosshair.position.X - position.X));

            if (input.inputDown("Fire"))
            {
				ShootAt(crosshair.position, gameTime);
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
				base.takeDamage();
			else
				base.collidedWith(otherObject);
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
