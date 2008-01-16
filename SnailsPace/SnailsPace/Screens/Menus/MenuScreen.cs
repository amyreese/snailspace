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
    abstract class MenuScreen : Screen
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
                spriteFont = Game.Content.Load<SpriteFont>("Resources/Fonts/Menu");
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

        public override void Update(GameTime gameTime)
        {
            #region Input Commands
            Input input = SnailsPace.inputManager;
            
            if (input.inputPressed("MenuUp"))
            {
                menuItems[menuItemIndex].Selected = false;
                menuItemIndex--;
                if (menuItemIndex < 0)
                {
                    menuItemIndex = menuItems.Length - 1;
                }
                menuItems[menuItemIndex].Selected = true;
            }

            if (input.inputPressed("MenuDown"))
            {
                menuItems[menuItemIndex].Selected = false;
                menuItemIndex++;
                if (menuItemIndex >= menuItems.Length)
                {
                    menuItemIndex = 0;
                }
                menuItems[menuItemIndex].Selected = true;
            }
            #endregion

            base.Update(gameTime);
        }
    }
}
