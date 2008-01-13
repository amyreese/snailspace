using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace
{
    class GameEngine
    {
        // Game font
        public SpriteFont gameFont;
#if DEBUG
        public SpriteFont debugFont;
#endif

        // Game map
        public Objects.Map map;

        // Player
        public Objects.Helix helix;

        // Bullets
        public List<Objects.Bullet> bullets;

		// Renderer
		public GameRenderer gameRenderer;

        // Constructors
        public GameEngine(String map)
        {
            GameLua lua = new GameLua();
            
            // TODO: Load the map object from Lua
			this.map = new Objects.Map();
			this.map.objects = new List<Objects.GameObject>();
			this.map.characters = new List<Objects.Character>();

			// TODO: Initialize Helix;
            helix = new Objects.Helix();
            helix.sprites = new Dictionary<string, Objects.Sprite>();
            Objects.Sprite helSprite = new Objects.Sprite();
            helSprite.image = new Objects.Image();
            helSprite.image.filename = "Resources/Textures/HelixTable";
            helSprite.image.blocks = new Vector2(4.0f, 4.0f);
            helSprite.image.size = new Vector2(128.0f, 128.0f);
            helSprite.visible = true;
            helSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
            helix.sprites.Add("Snail", helSprite);
            helix.velocity = new Vector2(3.0f, 2.0f);


            Objects.Helix helix2 = new Objects.Helix();
            helix2.position = new Vector2(0.5f, -1.0f);
            helix2.sprites = new Dictionary<string, Objects.Sprite>();
            helix2.sprites.Add("Snail", helSprite);
            helix2.layer = 1;
            this.map.objects.Add(helix2);

            Objects.Sprite backgroundSprite = new Objects.Sprite();
            backgroundSprite.image = new Objects.Image();
            backgroundSprite.image.filename = "Resources/Textures/GardenPanorama";
            backgroundSprite.image.blocks = new Vector2(1.0f, 1.0f);
            backgroundSprite.image.size = new Vector2(4096.0f, 2048.0f);
            backgroundSprite.visible = true;
            backgroundSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
            Objects.GameObject bkg = new Objects.GameObject();
            bkg.sprites = new Dictionary<string, Objects.Sprite>();
            bkg.sprites.Add("Bkg", backgroundSprite);
            bkg.position = new Vector2(40.0f, 16.0f);
            bkg.layer = 50;
            this.map.objects.Add(bkg);

            backgroundSprite = new Objects.Sprite();
            backgroundSprite.image = new Objects.Image();
            backgroundSprite.image.filename = "Resources/Textures/GardenPanorama";
            backgroundSprite.image.blocks = new Vector2(1.0f, 1.0f);
            backgroundSprite.image.size = new Vector2(4096.0f, 64.0f);
            backgroundSprite.visible = true;
            backgroundSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
            bkg = new Objects.GameObject();
            bkg.sprites = new Dictionary<string, Objects.Sprite>();
            bkg.sprites.Add("Bkg", backgroundSprite);
            bkg.position = new Vector2(40.0f, -1.0f);
            bkg.layer = 5;
            this.map.objects.Add(bkg);

            helix2 = new Objects.Helix();
            helix2.position = new Vector2(1.0f, 1.0f);
            helix2.sprites = new Dictionary<string, Objects.Sprite>();
            helix2.sprites.Add("Snail", helSprite);
            helix2.layer = 2;
            this.map.objects.Add(helix2);

            bullets = new List<Objects.Bullet>();

            gameFont = SnailsPace.getInstance().Content.Load<SpriteFont>("MenuFont");
#if DEBUG
            debugFont = SnailsPace.getInstance().Content.Load<SpriteFont>("debug");
#endif
			gameRenderer = new GameRenderer();
			gameRenderer.createTextures(allObjects());
        }

        public void think(GameTime gameTime)
        {
            // TODO: iterate through map.characters calling think() on each one.
			List<Objects.Character>.Enumerator charEnum = map.characters.GetEnumerator();
			while (charEnum.MoveNext())
			{
				charEnum.Current.think(gameTime);
			}

			KeyboardState keyboardState = Keyboard.GetState();
			if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
			{
                float movement = helix.velocity.X * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
				helix.position.X -= movement;
				gameRenderer.cameraTargetPosition.X -= movement;
			}
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                float movement = helix.velocity.X * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
                helix.position.X += movement;
                gameRenderer.cameraTargetPosition.X += movement;
            }
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                float movement = helix.velocity.Y * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
                helix.position.Y += movement;
                gameRenderer.cameraTargetPosition.Y += movement;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                float movement = helix.velocity.Y * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
                helix.position.Y -= movement;
                gameRenderer.cameraTargetPosition.Y -= movement;
            }

            // TODO: handle player inputs to change Helix's attributes.
			helix.think(gameTime);
        }

		public void physics(GameTime gameTime)
        {
            // TODO: iterate through map.characters and this.bullets using collision detection to move everything.

            // TODO: iterate through map.triggers and map.characters to find which triggers to execute
        }

		public void render(GameTime gameTime)
        {
            // TODO: iterate through map.objects, map.characters, and this.bullets to gather all visible sprites
            // and then send the list of sprites to the rendering system.
			// TODO: add bullets
            List<Objects.Text> strings = new List<Objects.Text>();

#if DEBUG
            int numDebugStrings = 0;
            if (SnailsPace.debugHelixPosition)
            {
                Objects.Text debugString = new Objects.Text();
                debugString.color = Color.Yellow;
                debugString.content = "Helix: (" + helix.position.X + ", " + helix.position.Y + ")";
                debugString.font = debugFont;
                debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
                debugString.rotation = 0;
                debugString.scale = Vector2.One;
                strings.Add(debugString);
            }
            if (SnailsPace.debugCameraPosition)
            {
                Objects.Text debugString = new Objects.Text();
                debugString.color = Color.Yellow;
                debugString.content = "Camera: (" + gameRenderer.cameraPosition.X + ", " + gameRenderer.cameraPosition.Y + ", " + gameRenderer.cameraPosition.Z + ")";
                debugString.font = debugFont;
                debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
                debugString.rotation = 0;
                debugString.scale = Vector2.One;
                strings.Add(debugString);

                debugString = new Objects.Text();
                debugString.color = Color.Yellow;
                debugString.content = "Target: (" + gameRenderer.cameraTargetPosition.X + ", " + gameRenderer.cameraTargetPosition.Y + ", " + gameRenderer.cameraTargetPosition.Z + ")";
                debugString.font = debugFont;
                debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
                debugString.rotation = 0;
                debugString.scale = Vector2.One;
                strings.Add(debugString);

                debugString = new Objects.Text();
                debugString.color = Color.Yellow;
                Vector3 distance = gameRenderer.cameraTargetPosition - gameRenderer.cameraPosition;
                debugString.content = "Distance: (" + distance.X + ", " + distance.Y + ", " + distance.Z + ")";
                debugString.font = debugFont;
                debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
                debugString.rotation = 0;
                debugString.scale = Vector2.One;
                strings.Add(debugString);
            }
#endif
			gameRenderer.render(allObjects(), strings, gameTime);
        }

		private List<Objects.GameObject> allObjects()
		{
			List<Objects.GameObject> objects = new List<Objects.GameObject>(map.objects);
			objects.Add(helix);
			return objects;
		}
    }
}
