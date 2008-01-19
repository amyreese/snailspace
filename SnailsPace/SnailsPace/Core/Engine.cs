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
		public Objects.Sprite bulletSprite;
		public List<Objects.Bullet> bullets;

		// Pause Screen
		public Objects.GameObject pause;

		// Crosshair
		public Objects.GameObject crosshair;

		// Renderer
		public Renderer gameRenderer;

		// Constructors
		public Engine(String map)
		{
			GameLua lua = new GameLua();

			bullets = new List<Objects.Bullet>();

			bulletSprite = new Objects.Sprite();
			bulletSprite.image = new Objects.Image();
			bulletSprite.image.filename = "Resources/Textures/Bullet";
			bulletSprite.image.blocks = new Vector2(1.0f, 1.0f);
			bulletSprite.image.size = new Vector2(16.0f, 8.0f);
			bulletSprite.visible = true;
			bulletSprite.effect = "Resources/Effects/effects";

			// TODO: Load the map object from Lua
			this.map = new Objects.Map(map);

			// TODO: Initialize Helix;
			helix = new Objects.Helix();
			helix.sprites = new Dictionary<string, Objects.Sprite>();

			Objects.Sprite walk = new Objects.Sprite();
            walk.image = new Objects.Image();
            walk.image.filename = "Resources/Textures/HelixTable";
            walk.image.blocks = new Vector2(4.0f, 4.0f);
            walk.image.size = new Vector2(128.0f, 128.0f);
            walk.visible = true;
            walk.effect = "Resources/Effects/effects";

			Objects.Sprite fly = new Objects.Sprite();
			fly.image = walk.image;
			fly.visible = false;
			fly.effect = walk.effect;

			Objects.Sprite gun = new Objects.Sprite();
			gun.image = walk.image;
			gun.visible = true;
			gun.effect = walk.effect;
			gun.layerOffset = -0.01f;

			helix.sprites.Add("Walk", walk);
			helix.sprites["Walk"].animationStart = 0;
			helix.sprites["Walk"].animationEnd = 7;
			helix.sprites["Walk"].frame = 0;
			helix.sprites["Walk"].animationDelay = 1.0f / 15.0f;
			helix.sprites["Walk"].timer = 0f;

			helix.sprites.Add("Fly", fly);
			helix.sprites["Fly"].animationStart = 8;
			helix.sprites["Fly"].animationEnd = 11;
			helix.sprites["Fly"].frame = 8;
			helix.sprites["Fly"].animationDelay = 1.0f / 15.0f;
			helix.sprites["Fly"].timer = 0f;

			helix.sprites.Add("Gun", gun);
			helix.sprites["Gun"].animationStart = 12;
			helix.sprites["Gun"].animationEnd = 15;
			helix.sprites["Gun"].frame = 12;
			helix.sprites["Gun"].animationDelay = 1.0f / 15.0f;
			helix.sprites["Gun"].timer = 0f;

			helix.maxVelocity = 3.5f;
			helix.layer = 0;

			loadFonts();
			setupPauseOverlay();
			setupCrosshair();
			setupGameRenderer();
		}

		private void setupPauseOverlay()
		{
			Objects.Sprite pauseSprite = new Objects.Sprite();
			pauseSprite.image = new Objects.Image();
			pauseSprite.image.filename = "Resources/Textures/PauseScreen";
			pauseSprite.image.blocks = new Vector2(1.0f, 1.0f);
			pauseSprite.image.size = new Vector2(800.0f, 600.0f);
			pauseSprite.visible = false;
			pauseSprite.effect = "Resources/Effects/effects";
			pause = new Objects.GameObject();
			pause.sprites = new Dictionary<string, Objects.Sprite>();
			pause.sprites.Add("Pause", pauseSprite);
			pause.position = new Vector2(0.0f, 0.0f);
			pause.layer = -3;
		}

		private void setupCrosshair()
		{
			Objects.Sprite crosshairSprite = new Objects.Sprite();
			crosshairSprite.image = new Objects.Image();
			crosshairSprite.image.filename = "Resources/Textures/Crosshair";
			crosshairSprite.image.blocks = new Vector2(1.0f, 1.0f);
			crosshairSprite.image.size = new Vector2(64.0f, 64.0f);
			crosshairSprite.visible = true;
			crosshairSprite.effect = "Resources/Effects/effects";
			crosshair = new Objects.GameObject();
			crosshair.sprites = new Dictionary<string, Objects.Sprite>();
			crosshair.sprites.Add("Crosshair", crosshairSprite);
			crosshair.position = new Vector2(0.0f, 0.0f);
			crosshair.layer = 0;
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
			//            gameRenderer.createTexturesAndEffects(allObjects());

			Vector2 offsetPosition = new Vector2(50, 25);
			gameRenderer.cameraPosition = new Vector3(helix.position + offsetPosition, gameRenderer.cameraTargetOffset.Z * 1.5f);

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


			// Deal with Helix's movement
			{
				helix.velocity = Vector2.Zero;
				if (input.inputDown("Left") && input.inputDown("Right"))
				{
					// do nothing
					
				}
				else if (input.inputDown("Left"))
				{
					helix.velocity.X = -1;
					helix.sprites["Fly"].visible = false;
					helix.sprites["Walk"].visible = true;
					helix.sprites["Walk"].animate(gameTime);
				}
				else if (input.inputDown("Right"))
				{
					helix.velocity.X = 1;
					helix.sprites["Fly"].visible = false;
					helix.sprites["Walk"].visible = true;
					helix.sprites["Walk"].animate(gameTime);
				}

				if (input.inputDown("Up") && input.inputDown("Down"))
				{
					//do nothing
					helix.sprites["Fly"].visible = true;
					helix.sprites["Walk"].visible = false;
				}
				else if (input.inputDown("Up"))
				{
					helix.velocity.Y = 1;
					helix.sprites["Fly"].visible = true;
					helix.sprites["Walk"].visible = false;
					helix.sprites["Fly"].animate(gameTime);
				}
				else if (input.inputDown("Down"))
				{
					helix.velocity.Y = -1;
					helix.sprites["Fly"].visible = true;
					helix.sprites["Walk"].visible = false;
					helix.sprites["Fly"].animate(gameTime);
				}
			}


			// Update things that depend on mouse position
			{
				crosshair.position.X = mouseToScreenX(input.MouseX);
				crosshair.position.Y = mouseToScreenY(input.MouseY);
			}

			if (input.inputDown("Fire"))
			{
				Objects.Bullet bullet = new Objects.Bullet();
				bullet.sprites = new Dictionary<string, Objects.Sprite>();
				bullet.sprites.Add("Bullet", bulletSprite);
				bullet.position = helix.position;
				bullet.rotation = helix.sprites["Gun"].rotation;
				bullet.maxVelocity = 10.0f;
				bullet.velocity = new Vector2(crosshair.position.X - helix.position.X, crosshair.position.Y - helix.position.Y);
				bullet.layer = -0.001f;
				bullets.Add(bullet);
			}

			// TODO: handle player inputs to change Helix's attributes.
			helix.think(gameTime);
		}

		private float mouseToScreenX(int mouseX)
		{
			return mouseX / gameRenderer.cameraPosition.Z + gameRenderer.cameraPosition.X - 16;
		}

		private float mouseToScreenY(int mouseY)
		{
			return -mouseY / gameRenderer.cameraPosition.Z + gameRenderer.cameraPosition.Y + 12;
		}

		public void physics(GameTime gameTime)
		{
			if (enginePaused)
			{
				return;
			}
			float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 1);


			// TODO: iterate through map.characters and this.bullets using collision detection to move everything.
			{
				List<Objects.GameObject>.Enumerator objEnumerator = allObjects().GetEnumerator();
				while (objEnumerator.MoveNext())
				{
					Vector2 objectVelocity = new Vector2(objEnumerator.Current.velocity.X, objEnumerator.Current.velocity.Y);
					if (objectVelocity.Length() > 0)
					{
						objectVelocity.Normalize();
						objectVelocity = Vector2.Multiply(objectVelocity, objEnumerator.Current.maxVelocity);
						objectVelocity = Vector2.Multiply(objectVelocity, elapsedTime);
						objEnumerator.Current.position += objectVelocity;
					}
				}
			}

			// TODO: iterate through map.triggers and map.characters to find which triggers to execute

			// Helix's gun
			helix.sprites["Gun"].rotation = ((crosshair.position.X - helix.position.X) < 0 ? MathHelper.Pi : 0) + (float)Math.Atan((crosshair.position.Y - helix.position.Y) / (crosshair.position.X - helix.position.X));

			// Animate everything
			{

				List<Objects.GameObject>.Enumerator objEnumerator = this.map.objects.GetEnumerator();
				while (objEnumerator.MoveNext())
				{
					Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = objEnumerator.Current.sprites.Values.GetEnumerator();
					while (sprtEnumerator.MoveNext())
					{
						sprtEnumerator.Current.animate(gameTime);
					}
					sprtEnumerator.Dispose();
				}
				objEnumerator.Dispose();
			}
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

				debugString = new Objects.Text();
				debugString.color = Color.Yellow;
				debugString.content = "Crosshair: (" + crosshair.position.X + ", " + crosshair.position.Y + ")";
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
			List<Objects.Bullet>.Enumerator bulletEnum = bullets.GetEnumerator();
			while (bulletEnum.MoveNext())
			{
				objects.Add(bulletEnum.Current);
			}
			objects.Add(helix);
			objects.Add(pause);
			objects.Add(crosshair);
			return objects;
		}
	}
}
