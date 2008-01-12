using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using A_Snail_s_Pace.Graphics;
using A_Snail_s_Pace.Input;

namespace A_Snail_s_Pace.Screens.Menus
{
    abstract class MenuScreen : InputReadyScreen
    {
        protected MenuItem[] menuItems;
        protected int menuItemIndex = 0;

        public MenuScreen(SnailsPace game)
            : base(game)
        {
        }

        #region Graphics Stuff
        protected SpriteBatch _spriteBatch;
        public SpriteBatch spriteBatch
        {
            get
            {
                return _spriteBatch;
            }
            protected set
            {
                _spriteBatch = value;
            }
        }

        protected SpriteFont _spriteFont;
        public SpriteFont spriteFont
        {
            get
            {
                return _spriteFont;
            }
            protected set
            {
                _spriteFont = value;
            }
        }

        protected override void LoadContent()
        {
            // Don't overwrite what was already created by any subclass
            if (spriteBatch == null)
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
            }
            if (spriteFont == null)
            {
                spriteFont = Game.Content.Load<SpriteFont>("menufont");
            }
            SetupMenuItems();
            for (int index = 0; index < menuItems.Length; index++)
            {
                menuItems[index].Selected = index == menuItemIndex;
            }
            base.LoadContent();
        }

        protected abstract void SetupMenuItems();

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.Black, 1.0f, 0);
            if (menuItems != null)
            {
                for (int menuItemIndex = 0; menuItemIndex < menuItems.Length; menuItemIndex++)
                {
                    menuItems[menuItemIndex].Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }
        #endregion

        TimeSpan timeOfLastMenuMove = new TimeSpan();
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #region Input Commands
        protected override void initializeKeyMappings()
        {
            // Menu Commands
            assignKeyToAction(new KeyCombination(Keys.Space),
                            new ActionMapping(new ActionMapping.KeyAction(this.ActivateSelectedMenuItem),
                            ActionMapping.Perform.OnKeyDown));
            assignKeyToAction(new KeyCombination(Keys.Down),
                            new ActionMapping(new ActionMapping.KeyAction(this.NextMenuItem),
                            ActionMapping.Perform.WhileKeyDown));
            assignKeyToAction(new KeyCombination(Keys.Up),
							new ActionMapping(new ActionMapping.KeyAction(this.PreviousMenuItem),
                            ActionMapping.Perform.WhileKeyDown));

            // Full screen toggle
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.RightAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(snailsPace.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.LeftAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(snailsPace.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
        }

        protected void ActivateSelectedMenuItem(GameTime gameTime)
        {
            menuItems[menuItemIndex].Activate(gameTime);
        }

        // Make the time a large number so they can move right away on startup
        int minimumMillisecondsBetweenMenuMoves = 150;
		protected void PreviousMenuItem(GameTime gameTime)
        {
            if (timeOfLastMenuMove == null || gameTime.TotalRealTime.Subtract(timeOfLastMenuMove).Milliseconds > minimumMillisecondsBetweenMenuMoves )
            {
                timeOfLastMenuMove = gameTime.TotalRealTime;
                menuItems[menuItemIndex].Selected = false;
                menuItemIndex--;
                if (menuItemIndex < 0)
                {
                    menuItemIndex = menuItems.Length - 1;
                }
                menuItems[menuItemIndex].Selected = true;
            }
        }
		protected void NextMenuItem(GameTime gameTime)
        {
			if (timeOfLastMenuMove == null || gameTime.TotalRealTime.Subtract(timeOfLastMenuMove).Milliseconds > minimumMillisecondsBetweenMenuMoves)
			{
				timeOfLastMenuMove = gameTime.TotalRealTime;
				menuItems[menuItemIndex].Selected = false;
                menuItemIndex++;
                if (menuItemIndex >= menuItems.Length)
                {
                    menuItemIndex = 0;
                }
                menuItems[menuItemIndex].Selected = true;
            }
        }
        #endregion

    }
}
