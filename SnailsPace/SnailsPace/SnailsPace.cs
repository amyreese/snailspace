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
using SnailsPace.Core;
using SnailsPace.Config;
using SnailsPace.Screens;
#endregion

namespace SnailsPace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SnailsPace : Microsoft.Xna.Framework.Game
	{
		#region Debug Flags & Variables
#if DEBUG
        // Flags
		public static bool debugFramerate = false;
        public static bool debugCameraPosition = false;
        public static bool debugHelixPosition = false;
        public static bool debugCulling = false;
        public static bool debugEffectAndTextureLoading = false;
        public static bool debugCollisions = false;
        public static bool debugBoundingBoxes = false;
        public static bool debugFlying = false;
        public static bool debugTriggers = false;

        // Framerate variables
        private int frames = 0;
        private int currentFramerateIndex = 0;
        private double[] framerates = new double[50];
#endif
        #endregion

		#region Cheat Flags
		public static bool cheatInfiniteHealth = false;
		public static bool cheatInfiniteFuel = false;
		public static bool cheatInfiniteAmmo = false;
		public static bool cheatAllWeapons = false;
		#endregion

		#region Managers for input, sound, and configrations
		internal static Input inputManager;
        internal static Sound soundManager;
        internal static GameConfig gameConfig;
        internal static VideoConfig videoConfig;
		internal static Objects.HighScoreList highScoreList;
        #endregion

        #region Constructor & Instancing
        public SnailsPace()
        {
            if (_instance != null)
            {
                throw new Exception("There was an attempt to create two Snail's Pace instances.");
            }
            _instance = this;

            graphics = new GraphicsDeviceManager(this);
            inputManager = new Input();
            soundManager = new Sound();
            gameConfig = new GameConfig();
            videoConfig = new VideoConfig();
			highScoreList = new Objects.HighScoreList();

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

        #region GraphicsManager
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
            MainMenu,
			LevelSelectMenu,
            KeyBindingsMenu,
            GameLoading,
            Game,
            LevelOver,
			HighScoreListMenu,
			HighScores,
			CheatMenu
        }

		private GameStates currentGameState = GameStates.MainMenu;

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
            String levelSplit = gameConfig.getString("levelSplit");
            String levelSubsplit = gameConfig.getString("levelSubsplit");
            String[] levels = gameConfig.getString("levels").Split(levelSplit.ToCharArray());
            String[][] levelsSetting = new String[levels.Length][];
            for( int index = 0; index < levels.Length; index++ ) {
                levelsSetting[index] = levels[index].Split(levelSubsplit.ToCharArray());
            }
            screens = new Dictionary<GameStates, Screen>();
			screens.Add(GameStates.MainMenu, new Screens.Menus.MainMenuScreen(this));
			screens.Add(GameStates.LevelSelectMenu, new Screens.Menus.LevelSelectScreen(this, levelsSetting));
			screens.Add(GameStates.KeyBindingsMenu, new Screens.Menus.KeyBindingsMenuScreen(this));
            screens.Add(GameStates.GameLoading, new Screens.GameLoadingScreen(this));
            screens.Add(GameStates.Game, new Screens.GameScreen(this));
            screens.Add(GameStates.LevelOver, new Screens.LevelOverScreen(this));
			screens.Add(GameStates.HighScoreListMenu, new Screens.Menus.HighScoreMenuScreen(this));
			screens.Add(GameStates.HighScores, new Screens.HighScoreScreen(this, "Test"));
			screens.Add(GameStates.CheatMenu, new Screens.Menus.CheatMenuScreen(this));

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
            IsMouseVisible = false;
			IsFixedTimeStep = false;
            graphics.ApplyChanges();
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
            inputManager.update();
            soundManager.update();
            base.Update(gameTime);
        }

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
				fps = Math.Round(1000 / gameTime.ElapsedRealTime.TotalMilliseconds);
				framerates[currentFramerateIndex++] = fps;
				if (currentFramerateIndex >= framerates.Length)
				{
					currentFramerateIndex = 0;
				}
				double avgFps = 0;
				for (int index = 0; index < framerates.Length; index++)
				{
					avgFps += framerates[index];
				}
				avgFps /= framerates.Length;
				debug("Average FPS: " + avgFps);
				debug("FPS: " + fps);
				if (gameTime.IsRunningSlowly)
				{
					debug("Slow!!!");
				}
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
        #endregion
    }
}
