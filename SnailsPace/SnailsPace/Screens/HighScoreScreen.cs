using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnailsPace.Objects;
namespace SnailsPace.Screens
{
	class HighScoreScreen : Screen
	{
		public HighScoreScreen(SnailsPace game, String mapName)
            : base(game)
        {
			this.mapName = mapName;
		}

		//String for map name
		private String mapName;

		// The image drawn in the background
		private Texture2D screenImage;

		// The font used for text
		private SpriteFont font;

		/// <summary>
		/// Load the background image and font
		/// </summary>
		protected override void LoadContent()
		{
			screenImage = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/LevelEndScreen");
			font = Game.Content.Load<SpriteFont>("Resources/Fonts/LevelOver");
		}

		/// <summary>
		/// Draw the background image, the player's score, and a string indicating how to leave the screen.
		/// </summary>
		/// <param name="gameTime">GameTime for this update</param>
		public override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
				Color.Red, 1.0f, 0);
			SpriteBatch batch = new SpriteBatch(GraphicsDevice);
			batch.Begin();
			{
				batch.Draw(screenImage, new Rectangle(0, 0, SnailsPace.getInstance().Window.ClientBounds.Width, SnailsPace.getInstance().Window.ClientBounds.Height), Color.White);
				batch.DrawString(font, mapName, new Vector2(100, 25), Color.White);
				List<Score> pointsScores;
				SnailsPace.highScoreList.pointsScores.TryGetValue(mapName, out pointsScores);
				List<Score> accuracyScores;
				SnailsPace.highScoreList.pointsScores.TryGetValue(mapName, out accuracyScores);
				List<Score>.Enumerator pointEnumerator = pointsScores.GetEnumerator();
				List<Score>.Enumerator accuracyEnumerator = accuracyScores.GetEnumerator();
				for (int i = 0; i < pointsScores.Count; i++)
				{
					pointEnumerator.MoveNext();
					batch.DrawString(font, pointEnumerator.Current.score + "   " + pointEnumerator.Current.name, new Vector2(50, 50 + (25 * i)), Color.White);
				}
			}
			batch.End();
			batch.Dispose();
		}
	}
}
