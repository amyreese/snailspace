using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace A_Snail_s_Pace.Graphics
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class Helix : Microsoft.Xna.Framework.GameComponent
	{
		private Texture2D texture;
		private SpriteBatch spriter;
		float frameLength = 1f / 15f;
		float timer = 0f;
		int currentFrameX = 0;
		int currentFrameY = 0;
		int currentPosX = 0;
		int currentPosY = 0;


		public Helix(Game game)
			: base(game)
		{
			// TODO: Construct any child components here
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			// TODO: Add your initialization code here
			spriter = new SpriteBatch(Game.GraphicsDevice);
			ContentManager aLoader = new ContentManager(Game.Services);
			texture = aLoader.Load<Texture2D>("Resources/Textures/HelixTable") as Texture2D;

			base.Initialize();
		}

		public void draw()
		{
			spriter.Begin();
			spriter.Draw(texture, new Rectangle(currentPosX,currentPosY, 128, 128), new Rectangle(currentFrameX, currentFrameY, 128, 128), Color.White);
			spriter.End();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void update(GameTime gameTime)
		{
			// TODO: Add your update code here
			timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (timer >= frameLength)
			{
				timer = 0f;
				if (currentFrameX != 384)
				{
					currentFrameX = currentFrameX + 128;
				}
				else
				{
					currentFrameX = 0;
					if (currentFrameY != 384)
					{
						currentFrameY = currentFrameY + 128;
					}
					else
					{
						currentFrameY = 0;
					}
				}
			}
			base.Update(gameTime);
		}
		
		public void moveLeft(int amt)
		{
			currentPosX -= amt;
		}

		public void moveRight(int amt)
		{
			currentPosX += amt;
		}
	}
}