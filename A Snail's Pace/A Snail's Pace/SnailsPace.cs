#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
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
            keyMapping = new Dictionary<KeyCombination, ActionMapping>();
            keyMapping.Add(new KeyCombination(Keys.Escape),
                            new ActionMapping( new ActionMapping.KeyAction(this.exitGame),
                            ActionMapping.Perform.OnKeyDown));
            keyMapping.Add(new KeyCombination(new Keys[] { Keys.RightAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(this.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
            keyMapping.Add(new KeyCombination(new Keys[] { Keys.LeftAlt, Keys.Enter }),
                            new ActionMapping(new ActionMapping.KeyAction(this.toggleFullscreen),
                            ActionMapping.Perform.OnKeyDown));
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
            }

            // TODO: Load any ResourceManagementMode.Manual content
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


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (KeyCombination keyCombination in keyMapping.Keys)
            {
                bool keyDown = true;
                foreach (Keys key in keyCombination.getKeys())
                {
                    if (keyboardState.IsKeyUp(key))
                    {
                        keyDown = false;
                    }
                }
                ActionMapping actionMap;
                if (keyMapping.TryGetValue(keyCombination, out actionMap))
                {
                    if (keyDown)
                    {
                        actionMap.keyDown();
                    }
                    else
                    {
                        actionMap.keyUp();
                    }
                }
                else
                {
                    // This shouldn't ever happen, but we should probably handle this properly.
                }
            }
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        /// <summary>
        /// Toggles the game between full screen and windowed mode
        /// </summary>
        protected void toggleFullscreen()
        {
            graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        protected void exitGame()
        {
            this.Exit();
        }
    }
}
