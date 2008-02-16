using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SnailsPace.Screens
{
    class LevelOverScreen : LoadingScreen
    {
        public LevelOverScreen(SnailsPace game)
            : base(game, SnailsPace.GameStates.MainMenuLoading)
        {
            ready = false;
        }

        private Texture2D screenImage;
        private SpriteFont font;
        private String pointsString;
        public bool firstDraw = true;

        protected override void LoadContent()
        {
            screenImage = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/GameLoadingScreen");
            font = Game.Content.Load<SpriteFont>("Resources/Fonts/LevelOver");
        }

        protected override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            Core.Input input = SnailsPace.inputManager;
            if (input.inputPressed("MenuToggle"))
            {
                ready = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (firstDraw)
            {
				((Menus.MainMenuScreen)snailsPace.getScreen(SnailsPace.GameStates.MainMenu)).gameStarted = false;
				
				Core.Engine.sound.stop("music");
                Core.Engine.sound.stop("alarm");
                Core.Engine.sound.stop("jetpack");
				
				pointsString = Core.Engine.player.GetFinalPoints();
                firstDraw = false;
            }
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer,
                Color.Red, 1.0f, 0);
            SpriteBatch batch = new SpriteBatch(GraphicsDevice);
            batch.Begin();
            {
                batch.Draw(screenImage, new Rectangle(0, 0, SnailsPace.getInstance().Window.ClientBounds.Width, SnailsPace.getInstance().Window.ClientBounds.Height), Color.White);
                batch.DrawString(font, "Your score is...", new Vector2(100, 25), Color.White);
                batch.DrawString(font, pointsString, new Vector2(50, 60), Color.White);
                batch.DrawString(font, "Press escape to continue...", new Vector2(250, 400), Color.White);
            }
            batch.End();
            batch.Dispose();
        }
    }
}
