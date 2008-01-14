using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SnailsPace.Graphics;
using SnailsPace.Input;

namespace SnailsPace.Screens.Menus
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(SnailsPace game)
            : base(game)
        {
        }

        protected override void SetupMenuItems()
        {
            float itemY = spriteFont.LineSpacing;
            float itemX = 25.0f;
            menuItems = new MenuItem[3];
			menuItems[0] = new MenuItem("Play Game", this, new Vector2(itemX, itemY), new ActionMapping.KeyAction(snailsPace.startGame));
            menuItems[1] = new MenuItem("Settings", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 2), new ActionMapping.KeyAction(this.GoToSettingsMenu));
            menuItems[2] = new MenuItem("Quit", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * 4), new ActionMapping.KeyAction(snailsPace.exitGame));
            menuItemIndex = 0;
            ready = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected void GoToSettingsMenu(GameTime gameTime)
        {
            snailsPace.changeState(SnailsPace.GameStates.SettingsMenu);
        }
    }
}
