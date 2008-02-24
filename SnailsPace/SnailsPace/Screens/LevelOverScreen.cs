using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SnailsPace.Screens
{
    /// <summary>
    /// A screen displayed at the end of a level
    /// </summary>
    class LevelOverScreen : LoadingScreen
    {
        /// <summary>
        /// Constructor for the screen
        /// </summary>
        /// <param name="game">The instance of Snails Pace</param>
        public LevelOverScreen(SnailsPace game)
            : base(game, SnailsPace.GameStates.MainMenu)
        {
            // This screen waits for input before being ready to transition
            ready = false;
        }

        // The image drawn in the background
        private Texture2D screenImage;

        // The font used for text
        private SpriteFont font;

        // The string that indicates point calculation
        private String pointsString;

        // True if this is the first cycle of drawing; used to disable sounds and get the points string
        public bool initializeScreen = true;

        /// <summary>
        /// Load the background image and font
        /// </summary>
        protected override void LoadContent()
        {
            screenImage = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/LevelEndScreen");
            font = Game.Content.Load<SpriteFont>("Resources/Fonts/LevelOver");
        }


		private String nextLevel;

        /// <summary>
        /// Checks to see if the user has indicated that they are done reading
        /// If this is the first time through, initialize the screen
        /// </summary>
        /// <param name="gameTime">GameTime for this update</param>
        public override void Update(GameTime gameTime)
        {
            if (initializeScreen)
            {
                Core.Engine.sound.stop("music");
                Core.Engine.sound.stop("alarm");
                Core.Engine.sound.stop("jetpack");
                pointsString = Core.Engine.player.GetFinalPoints(gameTime);
                initializeScreen = false;
				nextLevel = Core.Engine.player.nextLevel;

				if (nextLevel == null)
				{
					nextState = SnailsPace.GameStates.MainMenu;
					((Menus.MainMenuScreen)snailsPace.getScreen(SnailsPace.GameStates.MainMenu)).gameStarted = false;
				}
				else
				{
					nextState = SnailsPace.GameStates.GameLoading;
					((GameScreen)snailsPace.getScreen(SnailsPace.GameStates.Game)).ReloadEngine(nextLevel);
					Core.Player.time = 0;
					switch (nextLevel)
					{
						case "Tree Fort":
							Core.Player.timeLimit = 100;
							break;
						case "Garden":
							Core.Player.timeLimit = 360;
							break;
						case "Garden2":
							Core.Player.timeLimit = 300;
							break;
						case "Credits":
							Core.Player.timeLimit = 0;
							break;
						default:
							Core.Player.timeLimit = 500;
							break;
					}
				}

            }

            Core.Input input = SnailsPace.inputManager;
            if (input.inputPressed("MenuToggle"))
            {
                ready = true;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the background image, the player's score, and a string indicating how to leave the screen.
        /// </summary>
        /// <param name="gameTime">GameTime for this update</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.Red, 1.0f, 0);
            SpriteBatch batch = new SpriteBatch(GraphicsDevice);
            batch.Begin();
            {
                batch.Draw(screenImage, new Rectangle(0, 0, SnailsPace.getInstance().Window.ClientBounds.Width, SnailsPace.getInstance().Window.ClientBounds.Height), Color.White);
                batch.DrawString(font, "Your score is...", new Vector2(100, 25), Color.White);
                batch.DrawString(font, pointsString, new Vector2(50, 60), Color.White);
                batch.DrawString(font, "Press " + SnailsPace.inputManager.getKeyBinding("MenuToggle") + " to continue...", new Vector2(250, 500), Color.White);
            }
            batch.End();
            batch.Dispose();
        }
    }
}
