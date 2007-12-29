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

namespace A_Snail_s_Pace.Screens
{
    class MainMenuScreen : InputReadyScreen
    {
        public MainMenuScreen(Game game)
            : base(game)
        {
        }

        #region Graphics Stuff
        protected override void LoadContent()
        {
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, SnailsPace.getInstance().Window.ClientBounds.Width / SnailsPace.getInstance().Window.ClientBounds.Height, 0.2f, 500.0f);
            SnailsPace.getInstance().viewMatrix = viewMatrix;
            SnailsPace.getInstance().projectionMatrix = projectionMatrix;
            initializeSprites();
        }

        protected override void UnloadContent()
        {
        }

        protected Matrix viewMatrix;
        protected Matrix projectionMatrix;
        protected GenericSprite[] sprites;
        protected void initializeSprites()
        {
            sprites = new GenericSprite[1];
            sprites[0] = new Graphics.TestGraphics.TextureTestSprite(SnailsPace.getInstance().content, new Vector2(8, 8), new Vector2(4, 4), 0.01f);
        }

        public override void Draw(GameTime gameTime)
        {
            SnailsPace.getInstance().graphics.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.SeaShell, 1.0f, 0);

            for (int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
            {
                sprites[spriteIndex].draw(SnailsPace.getInstance().graphics.GraphicsDevice);
            }
        }
        #endregion


        public override bool ready()
        {
            return DateTime.Now.Second % 5 == 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #region Input Commands
        protected override void initializeKeyMappings()
        {
            // Exit
            assignKeyToAction(new KeyCombination(Keys.Escape),
                            new ActionMapping(new ActionMapping.KeyAction(SnailsPace.getInstance().exitGame),
                            ActionMapping.Perform.OnKeyDown));

            // Start The Game
            assignKeyToAction(new KeyCombination(Keys.Space),
                            new ActionMapping(new ActionMapping.KeyAction(SnailsPace.getInstance().startGame),
                            ActionMapping.Perform.OnKeyDown));

            // Full screen toggle
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.RightAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(SnailsPace.getInstance().toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.LeftAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(SnailsPace.getInstance().toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
        }
        #endregion


    }
}
