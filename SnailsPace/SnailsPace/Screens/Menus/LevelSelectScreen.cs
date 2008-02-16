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
    class LevelSelectScreen : MenuScreen
    {
		class LevelDefinition {
			public readonly String name;
			public readonly String file;
			public LevelDefinition(String name, String file)
			{
				this.name = name;
				this.file = file;
			}
		}

		public LevelSelectScreen(SnailsPace game)
            : base(game)
        {
        }

		private static LevelDefinition[] levels = { new LevelDefinition( "Garden", "Garden" ), new LevelDefinition("Garden #2", "Garden2") };
        protected override void SetupMenuItems()
        {
            float itemY = spriteFont.LineSpacing;
            float itemX = 25.0f;
			menuItems = new MenuItem[levels.Length + 1];
			for (int index = 0; index < levels.Length; index++)
			{
				menuItems[index] = new MenuItem( levels[index].name, this, new Vector2( itemX, itemY + spriteFont.LineSpacing * ( index * 2 ) ) );
			}
			menuItems[menuItems.Length - 1] = new MenuItem( "Back", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * levels.Length * 2 ) );
			menuItemIndex = 0;
            ready = true;
        }

        public override void Update(GameTime gameTime)
        {
            Input input = SnailsPace.inputManager;

			if (input.inputPressed("MenuToggle"))
            {
				snailsPace.changeState(SnailsPace.GameStates.MainMenu);
			}

			if (input.inputPressed("MenuSelect"))
            {
				if (menuItemIndex == levels.Length)
				{
					snailsPace.changeState(SnailsPace.GameStates.MainMenu);
				}
				else
				{
					((GameScreen)snailsPace.getScreen(SnailsPace.GameStates.Game)).ReloadEngine(levels[menuItemIndex].file);
					((MainMenuScreen)snailsPace.getScreen(SnailsPace.GameStates.MainMenu)).gameStarted = true;
					snailsPace.changeState(SnailsPace.GameStates.GameLoading);
				}
            }

            base.Update(gameTime);
        }
    }
}
