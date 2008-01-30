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
            {
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
