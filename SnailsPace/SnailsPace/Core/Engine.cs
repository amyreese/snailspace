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
        public static Sound sound;

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
		public static List<Objects.Bullet> bullets;
		public static List<Objects.Explosion> explosions;

		// Pause Screen
		public Objects.GameObject pause;

		// Renderer
		public Renderer gameRenderer;

		// Invisible map bounding objects
		public List<Objects.GameObject> mapBounds;

		// list of objects that are colliding in this frame
		private List<Objects.GameObject> collidingObjects;

		// HUD sprites
		public static Texture2D healthBar;
		public static Texture2D healthIcon;
		public static Texture2D fuelBar;
		public static Texture2D fuelIcon;

		private Vector2 activityBoundsSize = new Vector2(2192, 1680);

		// Constructors
		public Engine(String mapName)
		{
            sound = SnailsPace.soundManager;

			// Initialize Lua, the Player, and the Map
			lua = new GameLua(mapName);
			map = new Objects.Map(mapName);

			bullets = new List<Objects.Bullet>();
			explosions = new List<Objects.Explosion>();
			collidingObjects = new List<Objects.GameObject>();

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

		bool tada = false;
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
				{
					deadChars.Add(charEnum.Current);
					player.killedEnemy();
                    Engine.sound.play("kill");
				}
				//If not, let it think.
				else
				{
					if (IsWithinBounds(charEnum.Current, Player.helix.position, activityBoundsSize))
					{
						charEnum.Current.think(gameTime);
					}
				}
			}
			charEnum.Dispose();

			List<Objects.Character>.Enumerator deadCharEnum = deadChars.GetEnumerator();
			while (deadCharEnum.MoveNext())
			{
				map.characters.Remove(deadCharEnum.Current);
			}
			deadCharEnum.Dispose();

#if DEBUG
			if (input.inputPressed("DebugFramerate"))
			{
				SnailsPace.debugFramerate = !SnailsPace.debugFramerate;
				Console.WriteLine("Debug Framerate: " + SnailsPace.debugFramerate);
			}
			if (input.inputPressed("DebugCollisions"))
			{
				SnailsPace.debugCollisions = !SnailsPace.debugCollisions;
				Console.WriteLine("Debug Collisions: " + SnailsPace.debugCollisions);
			}
			if (input.inputPressed("DebugCulling"))
			{
				SnailsPace.debugCulling = !SnailsPace.debugCulling;
				Console.WriteLine("Debug Culling: " + SnailsPace.debugCulling);
			}
			if (input.inputPressed("DebugBoundingBoxes"))
			{
				SnailsPace.debugBoundingBoxes = !SnailsPace.debugBoundingBoxes;
				Console.WriteLine("Debug Bounding Boxes: " + SnailsPace.debugBoundingBoxes);
			}
			if (input.inputPressed("DebugFlying"))
			{
				SnailsPace.debugFlying = !SnailsPace.debugFlying;
				Console.WriteLine("Debug Flying: " + SnailsPace.debugFlying);
			}
			if (input.inputPressed("DebugCameraPosition"))
			{
				SnailsPace.debugCameraPosition = !SnailsPace.debugCameraPosition;
				Console.WriteLine("Debug Camera Position: " + SnailsPace.debugCameraPosition);
			}
			if (input.inputPressed("DebugHelixPosition"))
			{
				SnailsPace.debugHelixPosition = !SnailsPace.debugHelixPosition;
				Console.WriteLine("Debug Helix Position: " + SnailsPace.debugHelixPosition);
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
				Console.WriteLine("Debug Triggers: " + SnailsPace.debugTriggers);
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

		private readonly Vector2 gravity = new Vector2(0.0f, -512.0f);

		private bool IsMovingWithinBounds(Objects.GameObject movingObject, Vector2 objectMovement, Vector2 boundsSize, Vector2 boundsCenter)
		{
			if (movingObject.position.X < boundsCenter.X)
			{
				if (objectMovement.X < 0)
				{
					float left = movingObject.position.X - movingObject.size.X / 2;
					float leftDiff = left - boundsCenter.X;
					float leftDiffAbs = Math.Abs(leftDiff);
					if (leftDiffAbs > boundsSize.X / 2)
					{
						return false;
					}
				}
			}
			else
			{
				if (objectMovement.X > 0)
				{
					float right = movingObject.position.X + movingObject.size.X / 2;
					float rightDiff = right - boundsCenter.X;
					float rightDiffAbs = Math.Abs(rightDiff);
					if (rightDiffAbs > boundsSize.X / 2)
					{
						return false;
					}
				}
			}
			if (movingObject.position.Y < boundsCenter.Y)
			{
				if (objectMovement.Y < 0)
				{
					float bottom = movingObject.position.Y - movingObject.size.Y / 2;
					float bottomDiff = bottom - boundsCenter.Y;
					float bottomDiffAbs = Math.Abs(bottomDiff);
					if (bottomDiffAbs > boundsSize.Y / 2)
					{
						return false;
					}
				}
			}
			else
			{
				if (objectMovement.Y > 0)
				{
					float top = movingObject.position.Y + movingObject.size.Y / 2;
					float topDiff = top - boundsCenter.Y;
					float topDiffAbs = Math.Abs(topDiff);
					if (topDiffAbs > boundsSize.Y / 2)
					{
						return false;
					}
				}
			} 
			return true;
		}
		private void MoveOrCollide(Objects.GameObject movingObject, List<Objects.GameObject> collidableObjects, float elapsedTime, Vector2 boundsSize, Vector2 boundsCenter)
		{
			if ((gravityEnabled && movingObject.affectedByGravity) || movingObject.velocity.Length() > 0 || movingObject.direction.Length() > 0)
			{
				
				Vector2 objectVelocity = GetObjectVelocity(movingObject, elapsedTime);
				Vector2 objectMovement = Vector2.Multiply(objectVelocity, elapsedTime);
				Vector2 resultingMovement = Vector2.Zero;

				if (!IsMovingWithinBounds( movingObject, objectMovement, boundsSize, boundsCenter ))
				{
					if (movingObject is Objects.Bullet)
					{
						bulletsToClear.Add((Objects.Bullet)movingObject);
					}
					return;
				}
				bool upwardCreep = false;

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
					bool noThirdXCollision = true;
					bool noSecondYCollision = true;
					float xTick = objectMovement.X * 0.25f;
					float yTick = objectMovement.Y * 0.25f;
					while ((noThirdXCollision && (Math.Abs(resultingMovement.X) < Math.Abs(objectMovement.X))) || (noSecondYCollision && (Math.Abs(resultingMovement.Y) < Math.Abs(objectMovement.Y))))
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
						else if( !noSecondXCollision )
						{
							resultingMovement.X += xTick;
							resultingMovement.Y += elapsedTime * 32;
							collidedObject = CheckForCollision(movingObject, remainingCollidableObjects, resultingMovement);
							if (collidedObject == null)
							{
								noSecondXCollision = true;
								upwardCreep = true;
							}
							else
							{
								resultingMovement.X -= xTick;
								resultingMovement.Y -= elapsedTime * 32;
								noThirdXCollision = false;
							}
						}
					}
				}
				if (movingObject is Objects.Helix)
				{
					if (resultingMovement.Y == 0)
					{
						if (objectVelocity.Y < 0)
						{
							Player.helix.flying = false;
						}
					}
					else
					{
						if( !upwardCreep ) {
							Player.helix.flying = true;
						}
					}
				}
				if (resultingMovement.Length() == 0)
				{
					movingObject.collidedWith(collidedObject);
					movingObject.velocity = Vector2.Zero;
					movingObject.velocityFromGravity = Vector2.Zero;
				}
				else
				{
					movingObject.position += resultingMovement;
					movingObject.velocity = objectVelocity;
					if (resultingMovement.Y >= 0)
					{
						movingObject.velocityFromGravity = Vector2.Zero;
					}
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

		private Vector2 GetObjectVelocity(Objects.GameObject movingObject, float elapsedTime)
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
				Vector2 objectVelocityLessGravity = objectVelocity - movingObject.velocityFromGravity;
				if (objectVelocityLessGravity.Y > movingObject.maxVelocity - movingObject.terminalVelocity)
				{
					if (objectVelocity.Length() > movingObject.desiredMaxVelocity)
					{
						objectVelocity.Normalize();
						objectVelocity = objectVelocity * movingObject.desiredMaxVelocity;
					}
				}
				else
				{
					if (objectVelocityLessGravity.Length() > movingObject.desiredMaxVelocity)
					{
						objectVelocityLessGravity.Normalize();
						objectVelocityLessGravity = objectVelocityLessGravity * movingObject.desiredMaxVelocity;
						objectVelocity = objectVelocityLessGravity + movingObject.velocityFromGravity;
					}
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
					Vector2 velocityFromGravity = gravity * elapsedTime;
					objectVelocity += velocityFromGravity;
					movingObject.velocityFromGravity += velocityFromGravity;
					if (-objectVelocity.Y > movingObject.terminalVelocity)
					{
						objectVelocity.Y = -movingObject.terminalVelocity;
						movingObject.velocityFromGravity = new Vector2(0, -movingObject.terminalVelocity);
					}
				}
			}
			return objectVelocity;
		}
		
		
		public bool IsWithinBounds( Objects.GameObject objectToCheck, Vector2 boundsCenter, Vector2 boundsSize ) {
			float left = objectToCheck.position.X - objectToCheck.size.X / 2;
			float right = objectToCheck.position.X + objectToCheck.size.X / 2;
			float top = objectToCheck.position.Y + objectToCheck.size.Y / 2;
			float bottom = objectToCheck.position.Y - objectToCheck.size.Y / 2;
			float leftDiff = left - boundsCenter.X;
			float leftDiffAbs = Math.Abs(leftDiff);
			float rightDiff = right - boundsCenter.X;
			float rightDiffAbs = Math.Abs(rightDiff);
			float topDiff = top - boundsCenter.Y;
			float topDiffAbs = Math.Abs(topDiff);
			float bottomDiff = bottom - boundsCenter.Y;
			float bottomDiffAbs = Math.Abs(bottomDiff);
			if (((leftDiffAbs < boundsSize.X / 2) || (rightDiffAbs < boundsSize.X / 2) || (rightDiff < 0 && leftDiff > 0 || rightDiff > 0 && leftDiff < 0))
				&& ((topDiffAbs < boundsSize.Y / 2) || (bottomDiffAbs < boundsSize.Y / 2) || (topDiff < 0 && bottomDiff > 0 || topDiff > 0 && bottomDiff < 0)))
			{
				return true;
			}
			return false;
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
				Vector2 boundsCenter = Player.helix.position;
				while (objEnum.MoveNext())
				{
					if (objEnum.Current.collidable)
					{
						if (IsWithinBounds(objEnum.Current, boundsCenter, activityBoundsSize))
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
					Rectangle visibleScreen = new Rectangle((int)(Player.helix.position.X - 600), (int)(Player.helix.position.Y) - 600, 1400, 1400);
					QuadTree quad = new QuadTree(collidableObjects, activityBoundsSize, boundsCenter, 2);
					quad.print();
					QuadTreeCollide(quad.getRoot(), elapsedTime, activityBoundsSize, boundsCenter);
					detectCollisionsInNode(quad.getRoot(), elapsedTime);
					int i = 2;
				}

				if (noQuadTree)
				{
					List<Objects.GameObject>.Enumerator collideableObjectEnum = collidableObjects.GetEnumerator();
					while (collideableObjectEnum.MoveNext())
					{
						MoveOrCollide(collideableObjectEnum.Current, collidableObjects, elapsedTime, activityBoundsSize, boundsCenter);
					}

				}

				// Clear out exploded bullets
				List<Objects.Bullet>.Enumerator destroyedBulletEnumerator = bulletsToClear.GetEnumerator();
				while (destroyedBulletEnumerator.MoveNext())
				{
					bullets.Remove(destroyedBulletEnumerator.Current);
					Objects.Explosion explosion = new Objects.Explosion();
					explosion.position = destroyedBulletEnumerator.Current.position;
					explosions.Add(explosion);
				}
				destroyedBulletEnumerator.Dispose();
				bulletsToClear.Clear();
			}

			// Check all triggers against Helix
			List<Objects.Trigger>.Enumerator triggers = map.triggers.GetEnumerator();
			while (triggers.MoveNext())
			{
				Objects.Trigger trigger = triggers.Current;

				//if (trigger.bounds.containsPoint(Player.helix.position.X, Player.helix.position.Y))
				if (trigger.bounds.WillIntersect(Player.helix.bounds, Vector2.Zero))
				{
					if (!trigger.inside)
					{
						trigger.triggerIn(Player.helix);
					}
					trigger.trigger(Player.helix);
				}
				else
				{
					if (trigger.inside)
					{
						trigger.triggerOut(Player.helix);
					}
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

				List<Objects.Explosion>.Enumerator explosionEnumerator = explosions.GetEnumerator();
				List<Objects.Explosion> explosionsToRemove = new List<Objects.Explosion>();
				while (explosionEnumerator.MoveNext())
				{
					if (!explosionEnumerator.Current.DoAnimation( gameTime ))
					{
						explosionsToRemove.Add(explosionEnumerator.Current);
					}
				}
				explosionEnumerator.Dispose();
				explosionEnumerator = explosionsToRemove.GetEnumerator();
				while (explosionEnumerator.MoveNext())
				{
					explosions.Remove(explosionEnumerator.Current );
				}
				explosionEnumerator.Dispose();

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
					String nodeName = node.name;
					Vector2 objectVelocity = GetObjectVelocity(objectEnumerator.Current, elapsedTime);
					Vector2 objectMovement = Vector2.Multiply(objectVelocity, elapsedTime);
					Objects.GameObject collided = CheckForCollision(objectEnumerator.Current, containedObjects, objectMovement);
					if ( collided != null )
					{
#if DEBUG
						Debug.WriteLine("Collision Found: ");
						Debug.WriteLine("   Node: " + nodeName);
						Debug.WriteLine("   Objects: " + objectEnumerator.Current.name + ", " + collided.name);
#endif 
						collidingObjects.Add(objectEnumerator.Current);
					}
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

		private void QuadTreeCollide( QuadTreeNode node, float elapsedTime, Vector2 boundsSize, Vector2 boundsCenter )
		{
			List<QuadTreeNode> children = node.getNodes();
			if (children.Count == 0)
			{
				List<Objects.GameObject> containedObjects = node.getContainedObjects();
				List<Objects.GameObject>.Enumerator objectEnumerator = containedObjects.GetEnumerator();
				while (objectEnumerator.MoveNext())
				{
					MoveOrCollide(objectEnumerator.Current, containedObjects, elapsedTime, boundsSize, boundsCenter);
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

			objects.AddRange(mapBounds);

			List<Objects.Character>.Enumerator characterEnum = map.characters.GetEnumerator();
			while (characterEnum.MoveNext())
			{
				objects.Add(characterEnum.Current);
			}
			characterEnum.Dispose();

			objects.AddRange(player.gameObjects());

			List<Objects.Bullet>.Enumerator bulletEnum = bullets.GetEnumerator();
			while (bulletEnum.MoveNext())
			{
				objects.Add(bulletEnum.Current);
			}
			bulletEnum.Dispose();

			List<Objects.Explosion>.Enumerator explosionEnum = explosions.GetEnumerator();
			while (explosionEnum.MoveNext())
			{
				objects.Add(explosionEnum.Current);
			}
			explosionEnum.Dispose();

            objects.Add(pause);

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
