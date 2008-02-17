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

        public MainMenuScreen(SnailsPace game)
            : base(game)
        {
        }

		protected override string GetBackgroundImage()
		{
			return "Resources/Textures/MainMenuScreen";
		}

        protected override void SetupMenuItems()
        {
            float itemY = spriteFont.LineSpacing;
            float itemX = 25.0f;
            menuItems = new MenuItem[7];
			menuItems[0] = new MenuItem("Play Game", this, new Vector2(itemX, itemY));
			menuItems[1] = new MenuItem("Key Bindings", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 2));
			menuItems[2] = new MenuItem("Quit", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4));

			menuItems[3] = new MenuItem("Resume Game", this, new Vector2(itemX, itemY));
			menuItems[4] = new MenuItem("Exit Level", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 2));
            menuItems[5] = new MenuItem("Key Bindings", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4));
			menuItems[6] = new MenuItem("Quit", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 6));

			menuItemIndex = 0;
            ready = true;
        }

        public override void Update(GameTime gameTime)
        {

			menuItems[0].Visible = !gameStarted;
			menuItems[1].Visible = !gameStarted;
			menuItems[2].Visible = !gameStarted;

			menuItems[3].Visible = gameStarted;
			menuItems[4].Visible = gameStarted;
			menuItems[5].Visible = gameStarted;
			menuItems[6].Visible = gameStarted;

			if (!menuItems[menuItemIndex].Visible)
			{
				menuItems[menuItemIndex].Selected = false;
				if (gameStarted)
				{
					menuItemIndex = 3;
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
					case 3:
							snailsPace.changeState(SnailsPace.GameStates.GameLoading);
							break;
					case 1:
					case 5:
						snailsPace.changeState(SnailsPace.GameStates.SettingsMenu);
                        break;
					case 2:
					case 6:
						snailsPace.exitGame(gameTime);
                        break;
					case 4:
						gameStarted = false;
						break;
				}
            }

            base.Update(gameTime);
        }
    }
}
