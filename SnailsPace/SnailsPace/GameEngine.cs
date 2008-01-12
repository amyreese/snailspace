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
            lua.init();

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

			Objects.Sprite backgroundSprite = new Objects.Sprite();
			backgroundSprite.image = new Objects.Image();
			backgroundSprite.image.filename = "Resources/Textures/GardenPanorama";
			backgroundSprite.image.blocks = new Vector2(1.0f, 1.0f);
			backgroundSprite.image.size = new Vector2(2048.0f, 1024.0f);
			backgroundSprite.visible = true;
			backgroundSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
			Objects.GameObject bkg = new Objects.GameObject();
			bkg.sprites = new Dictionary<string, Objects.Sprite>();
			bkg.sprites.Add("Bkg", backgroundSprite);
			this.map.objects.Add(bkg);

			bullets = new List<Objects.Bullet>();

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
				helix.position.X -= 0.15f;
				gameRenderer.cameraTargetPosition.X -= 0.15f;
			}
			else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
			{
				helix.position.X += 0.15f;
				gameRenderer.cameraTargetPosition.X += 0.15f;
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
			gameRenderer.render(allObjects(), null, gameTime);
        }

		private List<Objects.GameObject> allObjects()
		{
			List<Objects.GameObject> objects = new List<Objects.GameObject>(map.objects);
			objects.Add(helix);
			return objects;
		}
    }
}
