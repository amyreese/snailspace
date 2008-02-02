using System;
using System.Diagnostics;
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
		public static GameTime gameTime;

		// Game font
		public SpriteFont gameFont;
#if DEBUG
		public SpriteFont debugFont;
#endif

		// Game map
		public static GameLua lua;
		public static Objects.Map map;

		// Player
		public static Core.Player player;

		// Bullets
		public static Objects.Sprite bulletSprite;
		public static List<Objects.Bullet> bullets;

		// Pause Screen
		public Objects.GameObject pause;

		// Renderer
		public Renderer gameRenderer;

		// Invisible map bounding objects
		public List<Objects.GameObject> mapBounds;

		// HUD sprites
		public static Texture2D healthBar;
		public static Texture2D healthIcon;
		public static Texture2D fuelBar;
		public static Texture2D fuelIcon;

		// Constructors
		public Engine(String mapName)
		{
			// Initialize Lua, the Player, and the Map
			lua = new GameLua(mapName);
			map = new Objects.Map(mapName);

			bullets = new List<Objects.Bullet>();

			bulletSprite = new Objects.Sprite();
			bulletSprite.image = new Objects.Image();
			bulletSprite.image.filename = "Resources/Textures/Bullet";
			bulletSprite.image.blocks = new Vector2(1.0f, 1.0f);
			bulletSprite.image.size = new Vector2(16.0f, 8.0f);
			bulletSprite.visible = true;
			bulletSprite.effect = "Resources/Effects/effects";

			loadFonts();
			loadHUD();
			setupPauseOverlay();
			setupGameRenderer();
			setupMapBounds();
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

		private void setupMapBounds()
		{
			mapBounds = new List<Objects.GameObject>();

			//Build each bounding wall
			Objects.Sprite mapBoundsSprite = new Objects.Sprite();
			mapBoundsSprite.image = new Objects.Image();
			mapBoundsSprite.image.filename = "Resources/Textures/BoundingBox";
			mapBoundsSprite.image.blocks = new Vector2(1.0f);
			mapBoundsSprite.visible = false;
			mapBoundsSprite.effect = "Resources/Effects/effects";

			Vector2 lastPoint = map.bounds[0];
			Vector2 currentPoint;
			Objects.GameObject mapBound;

			for (int i = 1; i < map.bounds.Count; i++)
			{
				currentPoint = map.bounds[i];

				mapBoundsSprite = mapBoundsSprite.clone();
				mapBound = new Objects.GameObject();

				if (lastPoint.X == currentPoint.X)
				{
					mapBoundsSprite.image.size = new Vector2(10.0f, MathHelper.Distance(currentPoint.Y, lastPoint.Y));
					mapBound.sprites.Add("BoundingBox", mapBoundsSprite);
					mapBound.size = mapBoundsSprite.image.size;
					mapBound.position = new Vector2(currentPoint.X, (currentPoint.Y + lastPoint.Y) / 2.0f);
				}
				else if (lastPoint.Y == currentPoint.Y)
				{
					mapBoundsSprite.image.size = new Vector2(MathHelper.Distance(currentPoint.X, lastPoint.X), 10.0f);
					mapBound.sprites.Add("BoundingBox", mapBoundsSprite);
					mapBound.size = mapBoundsSprite.image.size;
					mapBound.position = new Vector2((currentPoint.X + lastPoint.X) / 2.0f, currentPoint.Y);
				}

				mapBound.collidable = true;

				mapBounds.Add(mapBound);

				lastPoint = currentPoint;
			}
		}

		private void loadFonts()
		{
			gameFont = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Menu");
#if DEBUG
			debugFont = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Debug");
#endif
		}

		private void loadHUD()
		{
			healthBar = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/HealthBar");
			healthIcon = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/HealthIcon");
			fuelBar = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/FuelBar");
			fuelIcon = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/FuelIcon");
		}

		private void setupGameRenderer()
		{
			gameRenderer = new Renderer();
			//            gameRenderer.createTexturesAndEffects(allObjects());

			Vector2 offsetPosition = new Vector2(0, 0);
			Renderer.cameraPosition = new Vector3(); //Player.helix.position + offsetPosition, Renderer.cameraTargetOffset.Z * 1.5f);

			Renderer.cameraTarget = Player.helix;
			Renderer.cameraTargetOffset.X = -64;
			Renderer.cameraTargetOffset.Y = 192;

			gameRenderer.cameraBounds = map.bounds.ToArray();
		}

		public void think(GameTime gameTime)
		{
			Engine.gameTime = gameTime;
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
				pause.position = new Vector2(Renderer.cameraPosition.X, Renderer.cameraPosition.Y);
				return;
			}

			// TODO: iterate through map.characters calling think() on each one.
			List<Objects.Character>.Enumerator charEnum = map.characters.GetEnumerator();
			List<Objects.Character> deadChars = new List<Objects.Character>();
			while (charEnum.MoveNext())
			{
				//Is a character dead? Kill it!
				if (charEnum.Current.health <= 0)
					deadChars.Add(charEnum.Current);

				//If not, let it think.
				else
					charEnum.Current.think(gameTime);
			}
			charEnum.Dispose();

			List<Objects.Character>.Enumerator deadCharEnum = deadChars.GetEnumerator();
			while (deadCharEnum.MoveNext())
				map.characters.Remove(deadCharEnum.Current);
			deadCharEnum.Dispose();

#if DEBUG
			if (input.inputPressed("DebugFramerate"))
			{
				SnailsPace.debugFramerate = !SnailsPace.debugFramerate;
			}
			if (input.inputPressed("DebugCollisions"))
			{
				SnailsPace.debugCollisions = !SnailsPace.debugCollisions;
			}
			if (input.inputPressed("DebugCulling"))
			{
				SnailsPace.debugCulling = !SnailsPace.debugCulling;
			}
			if (input.inputPressed("DebugBoundingBoxes"))
			{
				SnailsPace.debugBoundingBoxes = !SnailsPace.debugBoundingBoxes;
			}
			if (input.inputPressed("DebugFlying"))
			{
				SnailsPace.debugFlying = !SnailsPace.debugFlying;
			}
			if (input.inputPressed("DebugCameraPosition"))
			{
				SnailsPace.debugCameraPosition = !SnailsPace.debugCameraPosition;
			}
			if (input.inputPressed("DebugHelixPosition"))
			{
				SnailsPace.debugHelixPosition = !SnailsPace.debugHelixPosition;
			}
			if (input.inputDown("DebugZoomIn"))
			{
				Renderer.debugZoom -= 0.25f;
				if (Renderer.debugZoom < 1.0f)
				{
					Renderer.debugZoom = 1.0f;
				}
				Renderer.farClip = 500.0f + 2 * Renderer.normalCameraDistance * Renderer.debugZoom;
			}
			if (input.inputDown("DebugZoomOut"))
			{
				Renderer.debugZoom += 0.25f;
				Renderer.farClip = 500.0f + 2 * Renderer.normalCameraDistance * Renderer.debugZoom;
			}
			if (input.inputPressed("DebugTriggers"))
			{
				SnailsPace.debugTriggers = !SnailsPace.debugTriggers;
			}
#endif

			player.think(gameTime);
		}

		public static Vector2 mouseToGame(Vector2 mousePosition)
		{
			int screenWidth = SnailsPace.videoConfig.getInt("width");
			int screenHeight = SnailsPace.videoConfig.getInt("height");
			float scaleX = Renderer.cameraPosition.Z / 1.8f;
			float scaleY = Renderer.cameraPosition.Z / -2.4f;

			Vector2 gamePosition = new Vector2(Renderer.cameraPosition.X, Renderer.cameraPosition.Y);
			gamePosition.X += scaleX * screenSignedPercentage(mousePosition.X, screenWidth);
			gamePosition.Y += scaleY * screenSignedPercentage(mousePosition.Y, screenHeight);

			return gamePosition;
		}

		public static float screenSignedPercentage(float position, int size)
		{
			int halfSize = size / 2;
			float halfPosition = position - halfSize;
			float percentage = halfPosition / (float)halfSize;

			return percentage;
		}

		private List<Objects.Bullet> bulletsToClear = new List<Objects.Bullet>();
		private Objects.GameObject CheckForCollision(Objects.GameObject movingObject, List<Objects.GameObject> collidableObjects, Vector2 motionVector,
			out List<Objects.GameObject> remainingCollidableObjects)
		{
			return CheckForCollision(movingObject, collidableObjects, motionVector, out remainingCollidableObjects, true);
		}
		private Objects.GameObject CheckForCollision(Objects.GameObject movingObject, List<Objects.GameObject> collidableObjects, Vector2 motionVector)
		{
			List<Objects.GameObject> x;
			return CheckForCollision(movingObject, collidableObjects, motionVector, out x, false);
		}
		private Objects.GameObject CheckForCollision(Objects.GameObject movingObject, List<Objects.GameObject> collidableObjects, Vector2 motionVector,
			out List<Objects.GameObject> remainingCollidableObjects, bool populateRemaining)
		{
			remainingCollidableObjects = new List<Objects.GameObject>(collidableObjects);
			if (movingObject.collidable && collisionDetectionOn)
			{
				List<Objects.GameObject>.Enumerator collideableObjEnumerator = collidableObjects.GetEnumerator();
				while (collideableObjEnumerator.MoveNext())
				{
					if (collideableObjEnumerator.Current.collidable && collideableObjEnumerator.Current != movingObject)
					{
						if (movingObject.willIntersect(motionVector, collideableObjEnumerator.Current))
						{
							if (movingObject.canCollideWith(collideableObjEnumerator.Current))
							{
#if DEBUG
								if (SnailsPace.debugCollisions)
								{
									SnailsPace.debug("Collision: " + movingObject.position);
								}
#endif
								return collideableObjEnumerator.Current;
							}
							else
							{
								if (populateRemaining)
								{
									remainingCollidableObjects.Remove(collideableObjEnumerator.Current);
								}
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
			if ((gravityEnabled && movingObject.affectedByGravity) || movingObject.velocity.Length() > 0 || movingObject.direction.Length() > 0)
			{
				Vector2 objectVelocity = Vector2.Zero;
				Vector2 objectAccel = Vector2.Zero;
				// Calculate their velocity after acceleration
				if (movingObject.direction.Length() > 0)
				{
					movingObject.direction.Normalize();
					if (movingObject.desiredMaxVelocity == 0)
					{
						movingObject.desiredMaxVelocity = movingObject.maxVelocity;
					}
					objectAccel = movingObject.direction * movingObject.acceleration * elapsedTime;
					objectVelocity = objectAccel + movingObject.velocity;
					if (objectVelocity.Length() > movingObject.velocity.Length() && objectVelocity.Length() > movingObject.desiredMaxVelocity)
					{
						objectVelocity.Normalize();
						objectVelocity = objectVelocity * movingObject.desiredMaxVelocity;
					}
				}
				else
				{
					objectVelocity = movingObject.velocity;
					if (objectVelocity.X == 0)
					{
						// stay at 0
					}
					else if (objectVelocity.X > 0)
					{
						objectVelocity.X -= movingObject.horizontalFriction * elapsedTime;
						if (objectVelocity.X < 0)
						{
							objectVelocity.X = 0;
						}
					}
					else
					{
						objectVelocity.X += movingObject.horizontalFriction * elapsedTime;
						if (objectVelocity.X > 0)
						{
							objectVelocity.X = 0;
						}
					}
				}

				// Calculate their velocity after gravity;
				if (gravityEnabled && movingObject.affectedByGravity)
				{
					if (-objectVelocity.Y < movingObject.terminalVelocity)
					{
						objectVelocity += gravity * elapsedTime;
						if (-objectVelocity.Y > movingObject.terminalVelocity)
						{
							objectVelocity.Y = -movingObject.terminalVelocity;
						}
					}
				}
				Console.WriteLine( objectVelocity.Length() );
				Vector2 objectMovement = Vector2.Multiply(objectVelocity, elapsedTime);
				Vector2 resultingMovement = Vector2.Zero;

				List<Objects.GameObject> remainingCollidableObjects;
				Objects.GameObject collidedObject = CheckForCollision(movingObject, collidableObjects, objectMovement, out remainingCollidableObjects);
				if (collidedObject == null)
				{
					resultingMovement = objectMovement;
				}
				else if (movingObject is Objects.Bullet)
				{
					bulletsToClear.Add((Objects.Bullet)movingObject);
				}
				else if (movingObject is Objects.Character)
				{
					bool noSecondXCollision = true;
					bool noSecondYCollision = true;
					float xTick = objectMovement.X * 0.25f;
					float yTick = objectMovement.Y * 0.25f;

					while ((noSecondXCollision && (Math.Abs(resultingMovement.X) < Math.Abs(objectMovement.X))) || (noSecondYCollision && (Math.Abs(resultingMovement.Y) < Math.Abs(objectMovement.Y))))
					{
						if (noSecondYCollision && yTick != 0)
						{
							resultingMovement.Y += yTick;
							collidedObject = CheckForCollision(movingObject, remainingCollidableObjects, resultingMovement);
							if (collidedObject != null)
							{
								noSecondYCollision = false;
								resultingMovement.Y -= yTick;
							}
						}
						if (noSecondXCollision && xTick != 0)
						{
							resultingMovement.X += xTick;
							collidedObject = CheckForCollision(movingObject, remainingCollidableObjects, resultingMovement);
							if (collidedObject != null)
							{
								noSecondXCollision = false;
								resultingMovement.X -= xTick;
							}
						}
					}
				}
				if (resultingMovement.Y == 0)
				{
					if (movingObject is Objects.Helix)
					{
						if (objectVelocity.Y < 0)
						{
							Player.helix.flying = false;
						}
					}
				}
				if (resultingMovement.Y > 0)
				{
					if (movingObject is Objects.Helix)
					{
						Player.helix.flying = true;
					}
				}
				if (resultingMovement.Length() == 0)
				{
					movingObject.collidedWith(collidedObject);
					movingObject.velocity = Vector2.Zero;
				}
				else
				{
					movingObject.position += resultingMovement;
					movingObject.velocity = objectVelocity;
					if (resultingMovement.Y == 0)
					{
						movingObject.velocity.Y = 0;
					}
					if (resultingMovement.X == 0)
					{
						movingObject.velocity.X = 0;
					}
				}
			}
		}

		public void physics(GameTime gameTime)
		{
			if (enginePaused)
			{
				return;
			}
			float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 0.5);




			// Collision Detection
			{
				List<Objects.GameObject> collidableObjects = new List<Objects.GameObject>();
				List<Objects.GameObject>.Enumerator objEnum = allObjects().GetEnumerator();
				float maxXDiff = 700;
				float maxYDiff = 700;
				while (objEnum.MoveNext())
				{
					if (objEnum.Current.collidable)
					{
						float left = objEnum.Current.position.X - objEnum.Current.size.X / 2;
						float right = objEnum.Current.position.X + objEnum.Current.size.X / 2;
						float top = objEnum.Current.position.Y + objEnum.Current.size.Y / 2;
						float bottom = objEnum.Current.position.Y - objEnum.Current.size.Y / 2;
						float leftDiff = left - Player.helix.position.X;
						float leftDiffAbs = Math.Abs(leftDiff);
						float rightDiff = right - Player.helix.position.X;
						float rightDiffAbs = Math.Abs(rightDiff);
						float topDiff = top - Player.helix.position.Y;
						float topDiffAbs = Math.Abs(topDiff);
						float bottomDiff = bottom - Player.helix.position.Y;
						float bottomDiffAbs = Math.Abs(bottomDiff);
						if (((leftDiffAbs < maxXDiff) || (rightDiffAbs < maxXDiff) || (rightDiff < 0 && leftDiff > 0 || rightDiff > 0 && leftDiff < 0))
							&& ((topDiffAbs < maxXDiff) || (bottomDiffAbs < maxYDiff) || (topDiff < 0 && bottomDiff > 0 || topDiff > 0 && bottomDiff < 0)))
						{
							collidableObjects.Add(objEnum.Current);
						}
						else
						{
							if (objEnum.Current is Objects.Bullet)
							{
								bulletsToClear.Add((Objects.Bullet)objEnum.Current);
							}
						}
					}
				}
				objEnum.Dispose();

				bool noQuadTree = true;
				if (!noQuadTree)
				{
					Rectangle visibleScreen = new Rectangle((int)(Player.helix.position.X - 600), (int)(Player.helix.position.Y) - 600, 1200, 1200);
					QuadTree quad = new QuadTree(collidableObjects, visibleScreen, 2);
#if DEBUG
					//Debug.WriteLine( helix.name + ": (" + helix.position.X + "," + helix.position.Y + ")" );
					//quad.print();
#endif
					detectCollisionsInNode(quad.getRoot(), elapsedTime);
				}

				if (noQuadTree)
				{
					List<Objects.GameObject>.Enumerator collideableObjectEnum = collidableObjects.GetEnumerator();
					while (collideableObjectEnum.MoveNext())
					{
						MoveOrCollide(collideableObjectEnum.Current, collidableObjects, elapsedTime);
					}

				}

				// Clear out exploded bullets
				List<Objects.Bullet>.Enumerator destroyedBulletEnumerator = bulletsToClear.GetEnumerator();
				while (destroyedBulletEnumerator.MoveNext())
				{
					bullets.Remove(destroyedBulletEnumerator.Current);
					// TODO: explosion!!!
				}
				destroyedBulletEnumerator.Dispose();
			}

			// Check all triggers against Helix
            List<Objects.Trigger>.Enumerator triggers = map.triggers.GetEnumerator();
			while (triggers.MoveNext())
			{
				Objects.Trigger trigger = triggers.Current;

                if (trigger.bounds.containsPoint(Player.helix.position.X, Player.helix.position.Y))
                {
                    trigger.trigger(Player.helix);
                }
            }
            triggers.Dispose();

			// Animate everything
			{
				List<Objects.GameObject>.Enumerator objEnumerator = map.objects.GetEnumerator();
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

				List<Objects.Character>.Enumerator charEnumerator = map.characters.GetEnumerator();
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
					String objectName = node.name;
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
			List<Objects.GameObject> objects = new List<Objects.GameObject>();

#if DEBUG
			int numDebugStrings = 0;
			if (SnailsPace.debugHelixPosition)
			{
				Objects.Text debugString = new Objects.Text();
				debugString.color = Color.Yellow;
				debugString.content = "Helix: (" + Player.helix.position.X + ", " + Player.helix.position.Y + ")";
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
				debugString.content = "Camera: (" + Renderer.cameraPosition.X + ", " + Renderer.cameraPosition.Y + ", " + Renderer.cameraPosition.Z + ")";
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
				Vector3 distance = cameraTargetPosition - Renderer.cameraPosition;
				debugString.content = "Distance: (" + distance.X + ", " + distance.Y + ", " + distance.Z + ")";
				debugString.font = debugFont;
				debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
				debugString.rotation = 0;
				debugString.scale = Vector2.One;
				strings.Add(debugString);

				debugString = new Objects.Text();
				debugString.color = Color.Yellow;
				debugString.content = "Crosshair: (" + Player.crosshair.position.X + ", " + Player.crosshair.position.Y + ")";
				debugString.font = debugFont;
				debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
				debugString.rotation = 0;
				debugString.scale = Vector2.One;
				strings.Add(debugString);
			}
#endif
			strings.AddRange(allStrings());
			objects.AddRange(allObjects());
			gameRenderer.render(objects, strings, gameTime);
		}

		private List<Objects.GameObject> allObjects()
		{
			List<Objects.GameObject> objects = new List<Objects.GameObject>(map.objects);
			//objects.AddRange(map.objects);
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

			List<Objects.GameObject>.Enumerator boundsEnum = mapBounds.GetEnumerator();
			while (boundsEnum.MoveNext())
			{
				objects.Add(boundsEnum.Current);
			}
			boundsEnum.Dispose();

			objects.Add(pause);
			objects.AddRange(player.gameObjects());
			return objects;
		}

		private List<Objects.Text> allStrings()
		{
			List<Objects.Text> strings = new List<Objects.Text>();
			strings.AddRange(player.textStrings());
			return strings;
		}
	}
}
