using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Core;

namespace SnailsPace.Screens.Menus
{
	class CheatMenuScreen : MenuScreen
	{
		public CheatMenuScreen(SnailsPace game)
			: base(game)
		{
		}

		/// <summary>
		/// The background image to be used for this menu
		/// </summary>
		/// <returns>The background image to be used for this menu</returns>
		protected override string GetBackgroundImage()
		{
			return "Resources/Textures/CheatsScreen";
		}

		/// <summary>
		/// Prepare the menu items to be displayed on this menu
		/// </summary>
		protected override void SetupMenuItems()
		{
			float itemY = spriteFont.LineSpacing;
			float itemX = 25.0f;
			menuItems = new MenuItem[5];
			menuItems[0] = new MenuItem("Back to Menu", this, new Vector2(itemX, itemY));
			menuItems[1] = new MenuItem("Infinite Health: " + SnailsPace.cheatInfiniteHealth , this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 1.5f));
			menuItems[2] = new MenuItem("Infinite Fuel: " + SnailsPace.cheatInfiniteFuel, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 3.0f));
			menuItems[3] = new MenuItem("Infinite Ammo: " + SnailsPace.cheatInfiniteAmmo, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4.5f));
			menuItems[4] = new MenuItem("All Weapons: " + SnailsPace.cheatAllWeapons, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 6.0f));
			menuItemIndex = 0;
			ready = true;
		}

		/// <summary>
		/// Check to see if any of the menu items have been selected
		/// </summary>
		/// <param name="gameTime">GameTime for this update</param>
		public override void Update(GameTime gameTime)
		{
			Input input = SnailsPace.inputManager;
			float itemY = spriteFont.LineSpacing;
            float itemX = 25.0f;

			if (input.inputPressed("MenuToggle"))
			{
				snailsPace.changeState(SnailsPace.GameStates.MainMenu);
			}

			if (input.inputPressed("MenuSelect"))
			{
				switch(menuItemIndex)
				{
					case 0:
						snailsPace.changeState(SnailsPace.GameStates.MainMenu);
						break;
					case 1:
						SnailsPace.cheatInfiniteHealth = !SnailsPace.cheatInfiniteHealth;
						menuItems[1] = new MenuItem("Infinite Health: " + SnailsPace.cheatInfiniteHealth, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 1.5f));
						menuItems[1].Selected = true;
						break;
					case 2:
						SnailsPace.cheatInfiniteFuel = !SnailsPace.cheatInfiniteFuel;
						menuItems[2] = new MenuItem("Infinite Fuel: " + SnailsPace.cheatInfiniteFuel, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 3.0f));
						menuItems[2].Selected = true;
						break;
					case 3:
						SnailsPace.cheatInfiniteAmmo = !SnailsPace.cheatInfiniteAmmo;
						menuItems[3] = new MenuItem("Infinite Ammo: " + SnailsPace.cheatInfiniteAmmo, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4.5f));
						menuItems[3].Selected = true;
						break;
					case 4:
						SnailsPace.cheatAllWeapons = !SnailsPace.cheatAllWeapons;
						menuItems[4] = new MenuItem("All Weapons: " + SnailsPace.cheatAllWeapons, this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 6.0f));
						menuItems[4].Selected = true;
						break;
				}
			}

			base.Update(gameTime);
		}
	}
}
