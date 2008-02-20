using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SnailsPace.Core;
using SnailsPace.Objects;

namespace SnailsPace.Screens.Menus
{
	class HighScoreMenuScreen : MenuScreen
	{
		/// <summary>
		/// Creates the screen
		/// </summary>
		/// <param name="game">Snails Pace instance</param>
		public HighScoreMenuScreen(SnailsPace game)
			: base(game)
		{
		}

		/// <summary>
		/// The background image to be used for this menu
		/// </summary>
		/// <returns>The background image to be used for this menu</returns>
		protected override string GetBackgroundImage()
		{
			return "Resources/Textures/MainMenuScreen";
		}

		/// <summary>
        /// Prepare the menu items to be displayed on this menu
        /// </summary>
		protected override void SetupMenuItems()
		{
			float itemY = spriteFont.LineSpacing;
			float itemX = 25.0f;
			menuItems = new MenuItem[SnailsPace.highScoreList.pointsScores.Count];
			Dictionary<String, List<Score>>.Enumerator pointScoresEnumerator = SnailsPace.highScoreList.pointsScores.GetEnumerator();

			for ( int i = 0; i < SnailsPace.highScoreList.pointsScores.Count; i++) 
			{
				pointScoresEnumerator.MoveNext();
				String currentMap = pointScoresEnumerator.Current.Key;
				menuItems[i] = new MenuItem(currentMap, this, new Vector2(itemX, itemY + (spriteFont.LineSpacing * 2 * i)));
				
			}
			menuItemIndex = 0;
			ready = true;
			
		}
	}
}
