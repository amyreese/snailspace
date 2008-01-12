using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
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
			this.map.objects = new List<global::SnailsPace.Objects.GameObject>();

			// TODO: Initialize Helix;
			helix = new Objects.Helix();
			helix.sprites = new Dictionary<string, global::SnailsPace.Objects.Sprite>();
			Objects.Sprite helSprite = new Objects.Sprite();
			helSprite.image = new Objects.Image();
			helSprite.image.filename = "Resources/Textures/HelixTable";
			helSprite.image.blocks = new Vector2(4.0f, 4.0f);
			helSprite.image.size = new Vector2(10.0f, 10.0f);
			helSprite.visible = true;
			helSprite.effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
			helix.sprites.Add("Snail", helSprite);

			bullets = new List<Objects.Bullet>();

			gameRenderer = new GameRenderer();
        }

        public void think(GameTime gameTime)
        {
            // TODO: iterate through map.characters calling think() on each one.

            // TODO: handle player inputs to change Helix's attributes.
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
			List<Objects.GameObject> objects = new List<Objects.GameObject>(map.objects);
			objects.Add(helix);
			// TODO: add bullets
			gameRenderer.render(objects, null);
        }
    }
}
