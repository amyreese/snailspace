using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;
using LuaInterface;

namespace SnailsPace.Objects
{
    class Weapon
    {
        public String name;
        public Objects.Sprite bulletSprite;

		public double lastFired = 0;
        public double cooldown = 100;
        public int ammunition = -1;
        public String cue = "gun1";

        public LuaTable state;

        public Weapon()
        {
        }

        public static Weapon load(String name)
        {
            return (Weapon)Engine.lua.CallOn((LuaTable)Engine.lua["Weapons"], name);
        }

        /// <summary>
        /// Shoot a bullet directly at the specified target.
        /// </summary>
        /// <param name="targetPosition">The bullet's target</param>
        /// <param name="gameTime">The current time, used for firing rate</param>
        public void ShootAt(Character shooter, Vector2 targetPosition, GameTime gameTime)
        {
            if ((ammunition > 0 || ammunition == -1) && lastFired + cooldown < gameTime.TotalRealTime.TotalMilliseconds)
            {
                lastFired = gameTime.TotalRealTime.TotalMilliseconds;

                Engine.lua.CallOn(state, "ShootAt", shooter, targetPosition, gameTime);
                if (ammunition > 0) ammunition--;

                if (shooter is Helix)
                {
                    Engine.sound.play(cue);
                }
            }
            else if (shooter is Helix)
            {
                Player.helix.weapon = Player.helix.inventory["generic"];
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
        /*public void ShootFanAt(Vector2 position, Vector2 targetPosition, int numBullets, float offset, GameTime gameTime)
        {
            if (lastFired + cooldown < gameTime.TotalRealTime.TotalMilliseconds)
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
        }*/
    }
}
