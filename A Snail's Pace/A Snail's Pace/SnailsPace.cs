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
        public Matrix viewMatrix;
        public Matrix projectionMatrix;

        #region Constructor & Instancing
        public SnailsPace()
        {
            if (_instance != null)
            {
                throw new Exception("There was an attempt to create two Snail's Pace instances.");
            }
            _instance = this;
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);
            initializeGameScreens();
        }

        private static SnailsPace _instance;
        public static SnailsPace getInstance()
        {
            if (_instance == null)
            {
                throw new Exception("There was an attempt to get the Snail's Pace instance before it was created.");
            }
            return _instance;
        }
        #endregion

        #region Graphics and Content Managers

        private GraphicsDeviceManager _graphics;
        public GraphicsDeviceManager graphics
        {
            get
            {
                return _graphics;
            }
            private set
            {
                _graphics = value;
            }
        }

        private ContentManager _content;
        public ContentManager content
        {
            get
            {
                return _content;
            }
            private set
            {
                _content = value;
            }
        }

        #endregion

        #region Game States and Screens

        public enum GameStates
        {
            MainMenuLoading,
            MainMenu,
            GameLoading,
            Game
        }

        private GameStates currentGameState = GameStates.MainMenuLoading;

        public void changeState(GameStates toState)
        {
            if (!screens.ContainsKey(toState))
            {
                throw new Exception("Attempt to change to a state without a screen.");
            }
            currentGameState = toState;
        }

        private Dictionary<GameStates, Screen> screens;
        internal Screen getScreen(GameStates forState)
        {
            return screens[forState];
        }

        protected void initializeGameScreens()
        {
            screens = new Dictionary<GameStates, Screen>();
            screens.Add(GameStates.MainMenuLoading, new Screens.MainMenuLoadingScreen());
            screens.Add(GameStates.MainMenu, new Screens.MainMenuScreen());
            screens.Add(GameStates.GameLoading, new Screens.GameLoadingScreen());
            screens.Add(GameStates.Game, new Screens.GameScreen());
        }

        #endregion

        #region Debug Helpers
        /// <summary>
        /// Writes out a debug statement, if we're in debug mode
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("DEBUG")]
        public static void debug(string msg)
        {
            Debug.WriteLine(msg);
        }
        #endregion

        #region Initialization, Loading, and Unloading
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
            Dictionary<GameStates, Screen>.Enumerator screenEnumerator = screens.GetEnumerator();
            while (screenEnumerator.MoveNext())
            {
                screenEnumerator.Current.Value.LoadGraphicsContent(loadAllContent);
            }
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
            Dictionary<GameStates, Screen>.Enumerator screenEnumerator = screens.GetEnumerator();
            while (screenEnumerator.MoveNext())
            {
                screenEnumerator.Current.Value.UnloadGraphicsContent(unloadAllContent);
            }
            if (unloadAllContent)
            {
                content.Unload();
            }
        }
        #endregion

        #region Update & Draw
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            screens[currentGameState].Update(gameTime);
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
#if DEBUG
            frames = frames + 1;
            double fps = 0;
            fps = Math.Round(frames / gameTime.TotalRealTime.TotalSeconds);
            debug("Average FPS: " + fps);
            fps = Math.Round(1000 / gameTime.ElapsedRealTime.TotalMilliseconds);
            debug("FPS: " + fps);
            debug("Slow? " + gameTime.IsRunningSlowly);
#endif
            screens[currentGameState].Draw(gameTime);
            base.Draw(gameTime);
        }
        #endregion

        #region General Game Commands
        /// <summary>
        /// Toggles the game between full screen and windowed mode
        /// </summary>
        public void toggleFullscreen(GameTime gameTime)
        {
            graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public void exitGame(GameTime gameTime)
        {
            this.Exit();
        }

        public void startGame(GameTime gameTime)
        {
            changeState(GameStates.GameLoading);
        }

        public void goToMainMenu(GameTime gameTime)
        {
            changeState(GameStates.MainMenuLoading);
        }
        #endregion
    }
}
