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
using SnailsPace.Graphics;
using SnailsPace.Input;
using SnailsPace.Config;
#endregion

namespace SnailsPace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SnailsPace : Microsoft.Xna.Framework.Game
	{
		#region Debug flags
#if DEBUG
		public const bool debugFramerate = true;
		public const bool debugKeyAssignments = true;
#endif
		#endregion

		public Matrix viewMatrix;
        public Matrix projectionMatrix;

        static InputManager inputManager;
        static GameConfig gameConfig;
        static VideoConfig videoConfig;

        #region Constructor & Instancing
        public SnailsPace()
        {
            if (_instance != null)
            {
                throw new Exception("There was an attempt to create two Snail's Pace instances.");
            }
            _instance = this;

            graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";

            inputManager = new InputManager();
            gameConfig = new GameConfig();
            videoConfig = new VideoConfig();

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
        #endregion

        #region Game States and Screens

        public enum GameStates
        {
            MainMenuLoading,
            MainMenu,
            SettingsMenu,
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
            screens[currentGameState].Enabled = false;
            screens[toState].Enabled = true;
            screens[currentGameState].Visible = false;
            screens[toState].Visible = true;
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
            screens.Add(GameStates.MainMenuLoading, new Screens.MainMenuLoadingScreen(this));
            screens.Add(GameStates.MainMenu, new Screens.Menus.MainMenuScreen(this));
            screens.Add(GameStates.SettingsMenu, new Screens.Menus.SettingsMenuScreen(this));
            screens.Add(GameStates.GameLoading, new Screens.GameLoadingScreen(this));
            screens.Add(GameStates.Game, new Screens.GameScreen(this));
            Dictionary<GameStates, Screen>.Enumerator screenEnumerator = screens.GetEnumerator();
            while (screenEnumerator.MoveNext())
            {
                Components.Add(screenEnumerator.Current.Value);
                screenEnumerator.Current.Value.Enabled = screenEnumerator.Current.Key == currentGameState;
                screenEnumerator.Current.Value.Visible = screenEnumerator.Current.Value.Enabled;
            }
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
            int width = (int)videoConfig.getDouble("width");
            int height = (int)videoConfig.getDouble("height");
            String fullscreen = videoConfig.getString("fullscreen").ToLower();

            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferMultiSampling = false;
            graphics.IsFullScreen = (fullscreen == "yes" ? true : false);
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
			if (debugFramerate)
			{
				frames = frames + 1;
				double fps = 0;
				fps = Math.Round(frames / gameTime.TotalRealTime.TotalSeconds);
				debug("Average FPS: " + fps);
				fps = Math.Round(1000 / gameTime.ElapsedRealTime.TotalMilliseconds);
				debug("FPS: " + fps);
				debug("Slow? " + gameTime.IsRunningSlowly);
			}
#endif
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
