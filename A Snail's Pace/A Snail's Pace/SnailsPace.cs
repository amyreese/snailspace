#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using A_Snail_s_Pace.Graphics;
using A_Snail_s_Pace.Input;
#endregion

namespace A_Snail_s_Pace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SnailsPace : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private Dictionary<KeyCombination, ActionMapping> keyMapping;

        public SnailsPace()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);
            initializeKeyMappings();
        }

        protected void initializeKeyMappings()
        {
            keyMapping = new Dictionary<KeyCombination, ActionMapping>();

            // Exit
            assignKeyToAction(new KeyCombination(Keys.Escape),
                            new ActionMapping(new ActionMapping.KeyAction(this.exitGame),
                            ActionMapping.Perform.OnKeyDown));

            // Full screen toggle
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.RightAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(this.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
            assignKeyToAction(new KeyCombination(new Keys[] { Keys.LeftAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(this.toggleFullscreen),
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

        public void assignKeyToAction(KeyCombination keyCombination, ActionMapping action)
        {
            if (keyMapping.ContainsKey(keyCombination))
            {
#if DEBUG
                ActionMapping oldAction;
                keyMapping.TryGetValue(keyCombination, out oldAction);
                debug("Key Combination \"" + keyCombination.ToString() + "\" re-assigned from \"" +
                    oldAction.ToString() + "\" to \"" + action.ToString() + "\"");
#endif
                keyMapping.Remove(keyCombination);
            }
            keyMapping.Add(keyCombination, action);
        }

        /// <summary>
        /// Writes out a debug statement, if we're in debug mode
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("DEBUG")]
        public static void debug(string msg)
        {
            Debug.WriteLine(msg);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferMultiSampling = false;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// Load your graphics content.  If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                // TODO: Load any ResourceManagementMode.Automatic content
                viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
                projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, this.Window.ClientBounds.Width / this.Window.ClientBounds.Height, 0.2f, 500.0f);
                initializeSprites();
            }

            // TODO: Load any ResourceManagementMode.Manual content
        }

        public static Matrix viewMatrix;
        public static Matrix projectionMatrix;

        GenericSprite[] sprites;
        protected void initializeSprites()
        {
            sprites = new GenericSprite[4];
            sprites[0] = new Graphics.TestGraphics.TextureTestSprite(content, new Vector2(8, 5), new Vector2(0, 0), 0.01f);
            sprites[1] = new Graphics.TestGraphics.TextureTestSprite(content, new Vector2(2, 2), new Vector2(4, 4), 0.00f);
            sprites[2] = new Graphics.TestGraphics.TextureTestSprite(content, new Vector2(3, 3), new Vector2(0, 4), 0.02f);
            sprites[3] = new Graphics.TestGraphics.TransparencyTestSprite(content, new Vector2(5, 8), new Vector2(8, 4), 0.00f);
        }

        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent)
            {
                // TODO: Unload any ResourceManagementMode.Automatic content
                content.Unload();
            }

            // TODO: Unload any ResourceManagementMode.Manual content
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
            sprites[2].moveRight(0.1f);
        }
        protected void moveLeft(GameTime gameTime)
        {
            sprites[2].moveLeft(0.1f);
        }
        protected void moveUp(GameTime gameTime)
        {
            sprites[2].moveUp(0.1f);
        }
        protected void moveDown(GameTime gameTime)
        {
            sprites[2].moveDown(0.1f);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Dictionary<KeyCombination, ActionMapping>.Enumerator keyMapEnum = keyMapping.GetEnumerator();
            while (keyMapEnum.MoveNext())
            {
                KeyCombination keyCombination = keyMapEnum.Current.Key;
                ActionMapping actionMap = keyMapEnum.Current.Value;
                bool keyDown = true;
                Keys[] keys = keyCombination.getKeys();
                for (int keyIndex = 0; keyDown && keyIndex < keys.Length; keyIndex++)
                {
                    if (keyboardState.IsKeyUp(keys[keyIndex]))
                    {
                        keyDown = false;
                    }
                }
                if (keyDown)
                {
                    actionMap.keyDown(gameTime);
                }
                else
                {
                    actionMap.keyUp(gameTime);
                }
            }
            base.Update(gameTime);
        }



#if DEBUG
        private int frames = 0;
#endif
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.CornflowerBlue, 1.0f, 0);

            for (int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
            {
                sprites[spriteIndex].draw(graphics.GraphicsDevice);
            }

#if DEBUG
            frames = frames + 1;
            double fps = 0;
            fps = Math.Round(frames / gameTime.TotalRealTime.TotalSeconds);
            debug("Average FPS: " + fps);
            fps = Math.Round(1000 / gameTime.ElapsedRealTime.TotalMilliseconds);
            debug("FPS: " + fps);
            debug("Slow? " + gameTime.IsRunningSlowly);
#endif

            base.Draw(gameTime);
        }

        /// <summary>
        /// Toggles the game between full screen and windowed mode
        /// </summary>
        protected void toggleFullscreen(GameTime gameTime)
        {
            graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        protected void exitGame(GameTime gameTime)
        {
            this.Exit();
        }
    }
}
