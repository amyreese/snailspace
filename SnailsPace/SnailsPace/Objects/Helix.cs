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
		public int fireCooldown;
		public bool flying;

        public Helix( Vector2 position ) : base()
        {
            Engine.lua["helix"] = this;
            this.sprites = new Dictionary<string, Objects.Sprite>();
			this.name = "Helix";
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

            this.sprites.Add("Walk", walk);
            this.sprites["Walk"].animationStart = 0;
            this.sprites["Walk"].animationEnd = 7;
            this.sprites["Walk"].frame = 0;
            this.sprites["Walk"].animationDelay = 1.0f / 15.0f;
            this.sprites["Walk"].timer = 0f;

            this.sprites.Add("Fly", fly);
            this.sprites["Fly"].animationStart = 8;
            this.sprites["Fly"].animationEnd = 11;
            this.sprites["Fly"].frame = 8;
            this.sprites["Fly"].animationDelay = 1.0f / 15.0f;
            this.sprites["Fly"].timer = 0f;

            // TODO: Make hover it's own animation
            this.sprites.Add("Hover", fly);
            this.sprites["Hover"].animationStart = 8;
            this.sprites["Hover"].animationEnd = 11;
            this.sprites["Hover"].frame = 8;
            this.sprites["Hover"].animationDelay = 1.0f / 15.0f;
            this.sprites["Hover"].timer = 0f;

            this.sprites.Add("Gun", gun);
            this.sprites["Gun"].animationStart = 12;
            this.sprites["Gun"].animationEnd = 15;
            this.sprites["Gun"].frame = 12;
            this.sprites["Gun"].animationDelay = 1.0f / 15.0f;
            this.sprites["Gun"].timer = 0f;

            this.maxVelocity = 384.0f;
            this.maxFuel = 40;
            this.layer = 0;
            this.affectedByGravity = true;

            this.size = walk.image.size;
            Vector2[] points = new Vector2[11];
            points[0] = new Vector2(0.0f, 36.0f);
            points[1] = new Vector2(-27.0f, 25.0f);
            points[2] = new Vector2(-38.0f, 14.0f);
            points[3] = new Vector2(-47.0f, -32.0f);
            points[4] = new Vector2(29.0f, -34.0f);
            points[5] = new Vector2(53.0f, -13.0f);
            points[6] = new Vector2(51.0f, 8.0f);
            points[7] = new Vector2(39.0f, 8.0f);
            points[8] = new Vector2(28.0f, -9.0f);
            points[9] = new Vector2(27.0f, 6.0f);
            points[10] = new Vector2(21.0f, 23.0f);
            this.bounds = new Objects.GameObjectBounds(points);
            this.position = new Vector2(0, 0);

            Engine.helix = this;
        }

		public void setSprite(String sprtName, String aSprtName)
		{
			Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = this.sprites.Values.GetEnumerator();
			while (sprtEnumerator.MoveNext())
			{
				sprtEnumerator.Current.visible = false;
			}
			this.sprites[sprtName].visible = true;
			this.sprites[aSprtName].visible = true;
			
		}

        public override void think(GameTime gameTime)
        {
			affectedByGravity = true;
			float fuelMod = (float)Math.Min(1, gameTime.ElapsedRealTime.TotalSeconds);
			if (flying)
			{
				fuel -= fuelMod * (1 + velocity.Length());
				if (fuel < 0)
				{
					fuel = 0;
				}
				if (fuel > 0)
				{
					affectedByGravity = false;
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

            velocity = Vector2.Zero;
            if (input.inputDown("Left") && input.inputDown("Right"))
            {
                // do nothing
            }
            else if (input.inputDown("Left"))
            {
                if (!flying || fuel > 0)
                {
                    velocity.X = -1;
                    horizontalFlip = true;
                    sprites["Gun"].horizontalFlip = true;
                }
            }
            else if (input.inputDown("Right"))
            {
                if (!flying || fuel > 0)
                {
                    velocity.X = 1;
                    horizontalFlip = false;
                    sprites["Gun"].horizontalFlip = false;
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
                    velocity.Y = 1;
                }
            }
            else if (input.inputDown("Down"))
            {
                if (fuel > 0)
                {
                    velocity.Y = -1;
                }
            }

			// Sprites & Animations
			if (flying)
			{
				if (velocity.Y == 0 && velocity.X == 0)
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
				if (velocity.X != 0)
				{
					sprites["Walk"].animate(gameTime);
				}
			}

            this.sprites["Gun"].rotation = ((Engine.crosshair.position.X - this.position.X) < 0 ? MathHelper.Pi : 0) + (float)Math.Atan((Engine.crosshair.position.Y - this.position.Y) / (Engine.crosshair.position.X - this.position.X));

            if (input.inputDown("Fire"))
            {
                if (this.lastFired + this.coolDown < gameTime.TotalRealTime.TotalMilliseconds)
                {
                    Objects.Bullet bullet = new Objects.Bullet();
                    bullet.sprites = new Dictionary<string, Objects.Sprite>();
                    bullet.sprites.Add("Bullet", Engine.bulletSprite);
                    bullet.size = Engine.bulletSprite.image.size;
                    bullet.velocity = new Vector2(Engine.crosshair.position.X - this.position.X, Engine.crosshair.position.Y - this.position.Y);
                    bullet.velocity.Normalize();
                    bullet.rotation = this.sprites["Gun"].rotation;
                    bullet.position = this.position + Vector2.Multiply(bullet.velocity, 32 * 1.15f);
                    bullet.maxVelocity = this.maxVelocity + 64.0f;
                    bullet.layer = -0.001f;
                    bullet.isPCBullet = true;
                    bullet.damage = 1;
                    Engine.bullets.Add(bullet);
                    this.fireCooldown = 2;
                    this.lastFired = gameTime.TotalRealTime.TotalMilliseconds;
                }
            }
        }

		public override bool canCollideWith(GameObject otherObject)
		{
			if ((otherObject is Objects.Bullet) && (((Bullet)otherObject).isPCBullet))
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
