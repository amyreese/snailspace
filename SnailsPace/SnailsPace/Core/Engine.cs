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
		// Toggle for collision detection
		bool collisionDetectionOn = true;

		// Toggle for gravity
		bool gravityEnabled = true;

		// Engine state
		bool enginePaused = false;

		// Game font
		public SpriteFont gameFont;
#if DEBUG
		public SpriteFont debugFont;
#endif

		// Game map
		public static GameLua lua;
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
			lua = new GameLua(map);

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
            lua["helix"] = helix;
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
			gun.layerOffset = -1.0f;

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

			helix.maxVelocity = 640.0f;
			helix.layer = 0;
			helix.affectedByGravity = true;

			helix.size = walk.image.size;
			helix.position = new Vector2(0, 0);

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
			pause.collidable = false;
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
			crosshair.collidable = false;
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

			Vector2 offsetPosition = new Vector2(0, 0);
			gameRenderer.cameraPosition = new Vector3(helix.position + offsetPosition, gameRenderer.cameraTargetOffset.Z * 1.5f);

			gameRenderer.cameraTarget = helix;
			gameRenderer.cameraTargetOffset.X = -64;
			gameRenderer.cameraTargetOffset.Y = 192;
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
				pause.position = new Vector2(gameRenderer.cameraPosition.X, gameRenderer.cameraPosition.Y);
				return;
			}


			// TODO: iterate through map.characters calling think() on each one.
			List<Objects.Character>.Enumerator charEnum = map.characters.GetEnumerator();
			while (charEnum.MoveNext())
			{
				charEnum.Current.think(gameTime);
			}
			charEnum.Dispose();


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
					helix.setSprite("Walk", "Gun");
					helix.sprites["Walk"].animate(gameTime);
					helix.horizontalFlip = true;
					helix.sprites["Gun"].horizontalFlip = true;
				}
				else if (input.inputDown("Right"))
				{
					helix.velocity.X = 1;
					helix.setSprite("Walk", "Gun");
					helix.sprites["Walk"].animate(gameTime);
					helix.horizontalFlip = false;
					helix.sprites["Gun"].horizontalFlip = false;
				}

				if (input.inputDown("Up") && input.inputDown("Down"))
				{
					//do nothing
					helix.setSprite("Fly", "Gun");
				}
				else if (input.inputDown("Up"))
				{
					helix.velocity.Y = 1;
					helix.setSprite("Fly", "Gun");
					helix.sprites["Fly"].animate(gameTime);
				}
				else if (input.inputDown("Down"))
				{
					helix.velocity.Y = -1;
					helix.setSprite("Fly", "Gun");
					helix.sprites["Fly"].animate(gameTime);
				}
			}


			// Update things that depend on mouse position
			{
                crosshair.position = mouseToGame(input.mousePosition);
			}

			if (input.inputDown("Fire"))
			{
				if (helix.fireCooldown <= 0)
				{
					Objects.Bullet bullet = new Objects.Bullet();
					bullet.sprites = new Dictionary<string, Objects.Sprite>();
					bullet.sprites.Add("Bullet", bulletSprite);
					bullet.size = bulletSprite.image.size;
					bullet.velocity = new Vector2(crosshair.position.X - helix.position.X, crosshair.position.Y - helix.position.Y);
					bullet.velocity.Normalize();
					bullet.rotation = helix.sprites["Gun"].rotation;
					bullet.position = helix.position + Vector2.Multiply(bullet.velocity, 32 * 1.15f);
					bullet.maxVelocity = 320.0f;
					bullet.layer = -0.001f;
					bullet.isPCBullet = true;
					bullets.Add(bullet);
					helix.fireCooldown = 2;
				}
				else
				{
					helix.fireCooldown--;
				}
			}

			// TODO: handle player inputs to change Helix's attributes.
			helix.think(gameTime);
		}

        private Vector2 mouseToGame(Vector2 mousePosition)
        {
            int screenWidth = SnailsPace.videoConfig.getInt("width");
            int screenHeight = SnailsPace.videoConfig.getInt("height");
            float scaleX = gameRenderer.cameraPosition.Z / 1.8f;
            float scaleY = gameRenderer.cameraPosition.Z / -2.4f;

            Vector2 gamePosition = new Vector2(gameRenderer.cameraPosition.X, gameRenderer.cameraPosition.Y);
            gamePosition.X += scaleX * screenSignedPercentage(mousePosition.X, screenWidth);
            gamePosition.Y += scaleY * screenSignedPercentage(mousePosition.Y, screenHeight);

            return gamePosition;
        }

        private float screenSignedPercentage(float position, int size)
        {
            int halfSize = size / 2;
            float halfPosition = position - halfSize;
            float percentage = halfPosition / (float) halfSize;

            return percentage;
        }

		private List<Objects.Bullet> bulletsToClear = new List<Objects.Bullet>();
		private Objects.GameObject CheckForCollision(Objects.GameObject movingObject, List<Objects.GameObject> collidableObjects, Vector2 motionVector)
		{
			if (movingObject.collidable && collisionDetectionOn)
			{
				List<Objects.GameObject>.Enumerator collideableObjEnumerator = collidableObjects.GetEnumerator();
				while (collideableObjEnumerator.MoveNext())
				{
					if (collideableObjEnumerator.Current.collidable && collideableObjEnumerator.Current != movingObject)
					{
						if (movingObject.willIntersect(motionVector, collideableObjEnumerator.Current))
						{
							if (movingObject.collidedWith(collideableObjEnumerator.Current))
							{
								SnailsPace.debug("Collision: " + movingObject.position);
								if (movingObject is Objects.Bullet)
								{
									bulletsToClear.Add((Objects.Bullet)movingObject);
								}

								return collideableObjEnumerator.Current;
							}
						}
					}
				}
			}
			return null;
		}

		private readonly Vector2 gravity = new Vector2(0.0f, -256.0f);
		private void MoveOrCollide(Objects.GameObject movingObject, List<Objects.GameObject> collidableObjects, float elapsedTime)
		{
			Vector2 objectVelocity = new Vector2(movingObject.velocity.X, movingObject.velocity.Y);
			if (( gravityEnabled && movingObject.affectedByGravity )|| objectVelocity.Length() > 0 )
			{
				if (objectVelocity.Length() > 0)
				{
					objectVelocity.Normalize();
					objectVelocity = Vector2.Multiply(objectVelocity, movingObject.maxVelocity);
				}
				if ( gravityEnabled && movingObject.affectedByGravity)
				{
					objectVelocity += gravity;
				}
				objectVelocity = Vector2.Multiply(objectVelocity, elapsedTime);
				Objects.GameObject collidedObject = CheckForCollision(movingObject, collidableObjects, objectVelocity);
				if (collidedObject == null)
				{
					movingObject.position += objectVelocity;
				}
			}
		}

		public void physics(GameTime gameTime)
		{
			if (enginePaused)
			{
				return;
			}
			float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 1);


			List<Objects.GameObject> allObjs = allObjects();
			// Collision Detection
			{

				bool noQuadTree = true;
				if (!noQuadTree)
				{
					Rectangle visibleScreen = new Rectangle(helix.getRectangle().X - 400, helix.getRectangle().Y - 300, 800, 600);
					QuadTree quad = new QuadTree(allObjs, visibleScreen, 6 );
					detectCollisionsInNode(quad.getRoot(), elapsedTime);
				}

				if (noQuadTree)
				{
					// Helix Collision Detection
					MoveOrCollide(helix, allObjs, elapsedTime);

					// Enemy Collision Detection
					List<Objects.Character>.Enumerator charEnumerator = map.characters.GetEnumerator();
					while (charEnumerator.MoveNext())
					{
						MoveOrCollide(charEnumerator.Current, allObjs, elapsedTime);
					}
					charEnumerator.Dispose();

					// Bullet Collision Detection
					List<Objects.Bullet>.Enumerator bulletEnumerator = bullets.GetEnumerator();
					while (bulletEnumerator.MoveNext())
					{
						// TODO: Make bullets that are too far away from Helix collide with nothing
						MoveOrCollide(bulletEnumerator.Current, allObjs, elapsedTime);
					}
					bulletEnumerator.Dispose();
				}
				// Clear out exploded bullets
				
				
				List<Objects.Bullet>.Enumerator destroyedBulletEnumerator = bulletsToClear.GetEnumerator();
				while (destroyedBulletEnumerator.MoveNext())
				{
					bullets.Remove(destroyedBulletEnumerator.Current);
					// TODO: explosion?
				}
				destroyedBulletEnumerator.Dispose();
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

				List<Objects.Character>.Enumerator charEnumerator = this.map.characters.GetEnumerator();
				while (charEnumerator.MoveNext())
				{

					if (charEnumerator.Current.velocity.X >= 0)
					{
						charEnumerator.Current.horizontalFlip = false;
					}
					else
					{
						charEnumerator.Current.horizontalFlip = true;
					}

					Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = charEnumerator.Current.sprites.Values.GetEnumerator();
					while (sprtEnumerator.MoveNext())
					{
						sprtEnumerator.Current.animate(gameTime);
					}
					sprtEnumerator.Dispose();
				}
				charEnumerator.Dispose();
			}
		}

		private void detectCollisionsInNode(QuadTreeNode node, float elapsedTime)
		{
			List<QuadTreeNode> children = node.getNodes();
			if (children.Count == 0)
			{
				List<Objects.GameObject> containedObjects = node.getContainedObjects();
				List<Objects.GameObject>.Enumerator objectEnumerator = containedObjects.GetEnumerator();
				while (objectEnumerator.MoveNext())
				{
					MoveOrCollide(objectEnumerator.Current, containedObjects, elapsedTime);
				}
			}
			else
			{
				List<QuadTreeNode>.Enumerator childrenEnumerator = children.GetEnumerator();
				while (childrenEnumerator.MoveNext())
				{
					detectCollisionsInNode(childrenEnumerator.Current, elapsedTime);
				}
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
			objects.AddRange(map.objects);
			List<Objects.Character>.Enumerator characterEnum = map.characters.GetEnumerator();
			while (characterEnum.MoveNext())
			{
				objects.Add(characterEnum.Current);
			}
			characterEnum.Dispose();
			List<Objects.Bullet>.Enumerator bulletEnum = bullets.GetEnumerator();
			while (bulletEnum.MoveNext())
			{
				objects.Add(bulletEnum.Current);
			}
			bulletEnum.Dispose();
			objects.Add(helix);
			objects.Add(pause);
			objects.Add(crosshair);
			return objects;
		}
	}
}
