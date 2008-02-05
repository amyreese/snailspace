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
        private Boolean gameStarted = false;

        public MainMenuScreen(SnailsPace game)
            : base(game)
        {
        }

        protected override void SetupMenuItems()
        {
            float itemY = spriteFont.LineSpacing;
            float itemX = 25.0f;
            menuItems = new MenuItem[3];
			menuItems[0] = new MenuItem("Play Game", this, new Vector2(itemX, itemY));
            menuItems[1] = new MenuItem("Settings", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 2));
            menuItems[2] = new MenuItem("Quit", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4));
            menuItemIndex = 0;
            ready = true;
        }

        public override void Update(GameTime gameTime)
        {
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
                        gameStarted = true;
                        snailsPace.changeState(SnailsPace.GameStates.GameLoading);
                        break;
                    case 1:
                        snailsPace.changeState(SnailsPace.GameStates.SettingsMenu);
                        break;
                    case 2:
                        snailsPace.exitGame(gameTime);
                        break;
                }
            }

            base.Update(gameTime);
        }
    }
}
