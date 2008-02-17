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
        #region Levels that can be selected from the screen
        class LevelDefinition {
			public readonly String name;
			public readonly String file;
			public LevelDefinition(String name, String file)
			{
				this.name = name;
				this.file = file;
			}
		}
        private static LevelDefinition[] levels = { new LevelDefinition("Garden", "Garden"), new LevelDefinition("Garden #2", "Garden2") };
        #endregion

        /// <summary>
        /// Creates the screen
        /// </summary>
        /// <param name="game">Snails Pace instance</param>
        public LevelSelectScreen(SnailsPace game)
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
			menuItems = new MenuItem[levels.Length + 1];
			for (int index = 0; index < levels.Length; index++)
			{
				menuItems[index] = new MenuItem( levels[index].name, this, new Vector2( itemX, itemY + spriteFont.LineSpacing * ( index * 2 ) ) );
			}
			menuItems[menuItems.Length - 1] = new MenuItem( "Back", this, new Vector2(itemX, itemY + spriteFont.LineSpacing * levels.Length * 2 ) );
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
