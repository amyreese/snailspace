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
    class OldGameScreen : InputReadyScreen
    {
        public OldGameScreen(SnailsPace game)
            : base(game)
        {
        }

		private Helix helix;

        #region Graphics Stuff
        protected override void LoadContent()
        {
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, snailsPace.Window.ClientBounds.Width / snailsPace.Window.ClientBounds.Height, 0.2f, 500.0f);
            snailsPace.viewMatrix = viewMatrix;
            snailsPace.projectionMatrix = projectionMatrix;
            initializeSprites();
			helix = new Helix(this.Game);
			helix.Initialize();
            base.LoadContent();
            ready = true;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected Matrix viewMatrix;
        protected Matrix projectionMatrix;
        protected GenericSprite[] sprites;
        protected void initializeSprites()
        {
            sprites = new GenericSprite[4];
            sprites[0] = new Graphics.TestGraphics.TextureTestSprite(Game.Content, new Vector2(8, 5), new Vector2(0, 0), 0.01f);
            sprites[1] = new Graphics.TestGraphics.TextureTestSprite(Game.Content, new Vector2(2, 2), new Vector2(4, 4), 0.00f);
            sprites[2] = new Graphics.TestGraphics.TextureTestSprite(Game.Content, new Vector2(3, 3), new Vector2(0, 4), 0.02f);
            sprites[3] = new Graphics.TestGraphics.TransparencyTestSprite(Game.Content, new Vector2(5, 8), new Vector2(8, 4), 0.00f);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.CornflowerBlue, 1.0f, 0);
			helix.draw();
            for (int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
            {
                sprites[spriteIndex].draw(GraphicsDevice);
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #region Input Commands
        protected override void initializeKeyMappings()
        {
            // Exit
            assignKeyToAction(new KeyCombination(Keys.Escape),
                            new ActionMapping(new ActionMapping.KeyAction(snailsPace.goToMainMenu),
                            ActionMapping.Perform.OnKeyDown));

            // Full screen toggle
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.RightAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(snailsPace.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.LeftAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(snailsPace.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));

            // Motion
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.Q }),
                            new ActionMapping(new ActionMapping.KeyAction(this.rotateCounterClockwise),
                            ActionMapping.Perform.WhileKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.E }),
                            new ActionMapping(new ActionMapping.KeyAction(this.rotateClockwise),
                            ActionMapping.Perform.WhileKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.W }),
                            new ActionMapping(new ActionMapping.KeyAction(this.moveUp),
                            ActionMapping.Perform.WhileKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.A }),
                            new ActionMapping(new ActionMapping.KeyAction(this.moveLeft),
                            ActionMapping.Perform.WhileKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.S }),
                            new ActionMapping(new ActionMapping.KeyAction(this.moveDown),
                            ActionMapping.Perform.WhileKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.D }),
                            new ActionMapping(new ActionMapping.KeyAction(this.moveRight),
                            ActionMapping.Perform.WhileKeyDown));
        }

        protected void rotateClockwise(GameTime gameTime)
        {
            sprites[2].rotateClockwise(0.1f);
        }

        protected void rotateCounterClockwise(GameTime gameTime)
        {
            sprites[2].rotateCounterClockwise(0.1f);
        }

        protected void moveRight(GameTime gameTime)
        {
            //sprites[2].moveRight(0.1f);
			helix.moveRight(1);
			helix.update(gameTime);
			
        }
        protected void moveLeft(GameTime gameTime)
        {
            //sprites[2].moveLeft(0.1f);
			helix.moveLeft(1);
			helix.update(gameTime);
			
        }
        protected void moveUp(GameTime gameTime)
        {
            sprites[2].moveUp(0.1f);
        }
        protected void moveDown(GameTime gameTime)
        {
            sprites[2].moveDown(0.1f);
        }
        #endregion
    }
}
