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

namespace SnailsPace.Screens.Menus
{
    class MainMenuScreen : MenuScreen
    {
        public bool gameStarted = false;

        /// <summary>
        /// Creates the screen
        /// </summary>
        /// <param name="game">Snails Pace instance</param>
        public MainMenuScreen(SnailsPace game)
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
            menuItems = new MenuItem[9];
			menuItems[0] = new MenuItem("Play Game", this, new Vector2(itemX, itemY));
			menuItems[1] = new MenuItem("View Controls", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 2));
			menuItems[2] = new MenuItem("Cheats", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4));
			menuItems[3] = new MenuItem("Quit", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 6));

			menuItems[4] = new MenuItem("Resume Game", this, new Vector2(itemX, itemY));
			menuItems[5] = new MenuItem("Exit Level", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 2));
            menuItems[6] = new MenuItem("View Controls", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4));
			menuItems[7] = new MenuItem("Cheats", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 6));
			menuItems[8] = new MenuItem("Quit", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 8));

			menuItemIndex = 0;
            ready = true;
        }

        /// <summary>
        /// Check to see if any of the menu items have been selected
        /// Toggle visibility based on game started status
        /// </summary>
        /// <param name="gameTime">GameTime for this update</param>
        public override void Update(GameTime gameTime)
        {

			menuItems[0].Visible = !gameStarted;
			menuItems[1].Visible = !gameStarted;
			menuItems[2].Visible = !gameStarted;
			menuItems[3].Visible = !gameStarted;

			menuItems[4].Visible = gameStarted;
			menuItems[5].Visible = gameStarted;
			menuItems[6].Visible = gameStarted;
			menuItems[7].Visible = gameStarted;
			menuItems[8].Visible = gameStarted;

			if (!menuItems[menuItemIndex].Visible)
			{
				menuItems[menuItemIndex].Selected = false;
				if (gameStarted)
				{
					menuItemIndex = 4;
				}
				else
				{
					menuItemIndex = 0;
				}
				menuItems[menuItemIndex].Selected = true;
			}

			Input input = SnailsPace.inputManager;

            if (input.inputPressed("MenuToggle") && gameStarted)
            {
                snailsPace.changeState(SnailsPace.GameStates.GameLoading);
            }

            if (input.inputPressed("MenuSelect"))
            {
                switch (menuItemIndex)
                {
					case 0:
						snailsPace.changeState(SnailsPace.GameStates.LevelSelectMenu);
						break;
					case 4:
							snailsPace.changeState(SnailsPace.GameStates.GameLoading);
							break;
					case 1:
					case 6:
						snailsPace.changeState(SnailsPace.GameStates.KeyBindingsMenu);
                        break;
					case 3:
					case 8:
						snailsPace.exitGame(gameTime);
                        break;
					case 5:
						gameStarted = false;
						break;
					case 2:
					case 7:
						snailsPace.changeState(SnailsPace.GameStates.CheatMenu);
						break;
				}
            }

            base.Update(gameTime);
        }
    }
}
