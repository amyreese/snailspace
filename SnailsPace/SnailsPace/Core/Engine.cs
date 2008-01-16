using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Core
{
    class Engine
    {
        // Engine state
        Boolean enginePaused = false;

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

		// Pause Screen
		public Objects.GameObject pause;

		// Renderer
		public Renderer gameRenderer;

        // Constructors
        public Engine(String map)
        {
            GameLua lua = new GameLua();

			bullets = new List<Objects.Bullet>();
			
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
			helix.sprites["Snail"].animationStart = 0;
			helix.sprites["Snail"].animationEnd = 15;
			helix.sprites["Snail"].frame = 0;
			helix.sprites["Snail"].animationDelay = 1.0f / 15.0f;
			helix.sprites["Snail"].timer = 0f;
            helix.velocity = new Vector2(3.0f, 2.0f);


            Objects.Helix helix2 = new Objects.Helix();
            helix2.position = new Vector2(0.5f, -1.0f);
            helix2.sprites = new Dictionary<string, Objects.Sprite>();
            helix2.sprites.Add("Snail", helSprite);
            helix2.layer = 1;
            this.map.objects.Add(helix2);

            Objects.Sprite backgroundSprite = new Objects.Sprite();
            backgroundSprite.image = new Objects.Image();
            backgroundSprite.image.filename = "Resources/Textures/Garden";
            backgroundSprite.image.blocks = new Vector2(1.0f, 1.0f);
			backgroundSprite.image.size = new Vector2(8192.0f, 6144.0f); //TODO: Fix to not skew horizontally
            backgroundSprite.visible = true;
            backgroundSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
            Objects.GameObject bkg = new Objects.GameObject();
            bkg.sprites = new Dictionary<string, Objects.Sprite>();
            bkg.sprites.Add("Bkg", backgroundSprite);
            bkg.position = new Vector2(75.0f, 70.0f);
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

			Objects.Sprite pauseSprite = new Objects.Sprite();
			pauseSprite.image = new Objects.Image();
			pauseSprite.image.filename = "Resources/Textures/PauseScreen";
			pauseSprite.image.blocks = new Vector2(1.0f, 1.0f);
			pauseSprite.image.size = new Vector2(800.0f, 600.0f);
			pauseSprite.visible = false;
			pauseSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
			pause = new Objects.GameObject();
			pause.sprites = new Dictionary<string, Objects.Sprite>();
			pause.sprites.Add("Pause", pauseSprite);
			pause.position = new Vector2(0.0f, 0.0f);
			pause.layer = -3;

			

			loadFonts();
			setupGameRenderer();
        }

		private void loadFonts()
		{
			gameFont = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Menu");
#if DEBUG
			debugFont = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Debug");
#endif
		}

		private void setupGameRenderer()
		{
			gameRenderer = new Renderer();
			gameRenderer.createTextures(allObjects());

			Vector2 offsetPosition = new Vector2(50, 25);
			
			gameRenderer.cameraPosition = new Vector3( helix.position + offsetPosition, gameRenderer.cameraTargetOffset.Z * 1.5f);

			gameRenderer.cameraTarget = helix;
			gameRenderer.cameraTargetOffset.X = -2;
			gameRenderer.cameraTargetOffset.Y = 6;
		}

        public void think(GameTime gameTime)
        {
            Input input = SnailsPace.inputManager;

            if (input.inputPressed("Pause"))
            {
                enginePaused = !enginePaused;
            }
            if (input.inputPressed("MenuToggle"))
            {
                enginePaused = true;
                SnailsPace.getInstance().changeState(SnailsPace.GameStates.MainMenu);
            }

			pause.sprites["Pause"].visible = enginePaused;

			if (enginePaused)
			{
				pause.position.X = gameRenderer.cameraPosition.X;
				pause.position.Y = gameRenderer.cameraPosition.Y;
				return;
			}


            // TODO: iterate through map.characters calling think() on each one.
			List<Objects.Character>.Enumerator charEnum = map.characters.GetEnumerator();
			while (charEnum.MoveNext())
			{
				charEnum.Current.think(gameTime);
			}


			if (input.inputDown("Left") && input.inputDown("Right"))
			{
				// do nothing
			} else if (input.inputDown("Left"))
			{
                float movement = helix.velocity.X * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
				helix.position.X -= movement;
				helix.sprites["Snail"].animate(gameTime);

			} else if (input.inputDown("Right"))
            {
                float movement = helix.velocity.X * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
                helix.position.X += movement;
				helix.sprites["Snail"].animate(gameTime);
            }

			if (input.inputDown("Up") && input.inputDown("Down"))
			{
				//do nothing
			} else if (input.inputDown("Up"))
            {
                float movement = helix.velocity.Y * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
                helix.position.Y += movement;

            } else if (input.inputDown("Down"))
            {
                float movement = helix.velocity.Y * Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1);
                helix.position.Y -= movement;
            }

            // TODO: handle player inputs to change Helix's attributes.
			helix.think(gameTime);
        }

		public void physics(GameTime gameTime)
        {
            if (enginePaused)
            {
                return;
            }

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
				Vector3 cameraTargetPosition = gameRenderer.getCameraTargetPosition();
				debugString.content = "Target: (" + cameraTargetPosition.X + ", " + cameraTargetPosition.Y + ", " + cameraTargetPosition.Z + ")";
                debugString.font = debugFont;
                debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
                debugString.rotation = 0;
                debugString.scale = Vector2.One;
                strings.Add(debugString);

                debugString = new Objects.Text();
                debugString.color = Color.Yellow;
				Vector3 distance = cameraTargetPosition - gameRenderer.cameraPosition;
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
			objects.Add(pause);
			return objects;
		}
    }
}
