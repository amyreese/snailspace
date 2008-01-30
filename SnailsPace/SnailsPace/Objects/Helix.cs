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
			float fuelMod = (float)Math.Min(1, gameTime.ElapsedRealTime.TotalSeconds);
			if (flying)
			{
				fuel -= fuelMod * (1 + velocity.Length());
				if (fuel < 0)
				{
					fuel = 0;
				}
			}
			else
			{
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
            {
                Input input = SnailsPace.inputManager;

                this.velocity = Vector2.Zero;
                if (input.inputDown("Left") && input.inputDown("Right"))
                {
                    // do nothing

                }
                else if (input.inputDown("Left"))
                {
                    if (!this.flying || this.fuel > 0)
                    {
                        this.velocity.X = -1;
                        this.setSprite("Walk", "Gun");
                        this.sprites["Walk"].animate(gameTime);
                        this.horizontalFlip = true;
                        this.sprites["Gun"].horizontalFlip = true;
                    }
                }
                else if (input.inputDown("Right"))
                {
                    if (!this.flying || this.fuel > 0)
                    {
                        this.velocity.X = 1;
                        this.setSprite("Walk", "Gun");
                        this.sprites["Walk"].animate(gameTime);
                        this.horizontalFlip = false;
                        this.sprites["Gun"].horizontalFlip = false;
                    }
                }

                if (input.inputDown("Up") && input.inputDown("Down"))
                {
                    //do nothing
                    this.setSprite("Fly", "Gun");
                }
                else if (input.inputDown("Up"))
                {
                    if (this.fuel > 0)
                    {
                        this.velocity.Y = 1;
                        this.setSprite("Fly", "Gun");
                        this.sprites["Fly"].animate(gameTime);
                    }
                }
                else if (input.inputDown("Down"))
                {
                    if (this.fuel > 0)
                    {
                        this.velocity.Y = -1;
                        this.setSprite("Fly", "Gun");
                        this.sprites["Fly"].animate(gameTime);
                    }
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
