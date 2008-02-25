#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SnailsPace.Objects;
#endregion

namespace SnailsPace.Core
{
    /// <summary>
    /// This class handles initializing everything needed for playing a level, dealing with physics,
    /// artificial intelligence, and passing information along to the renderer so that everything can be drawn
    /// </summary>
    class Engine
    {

        #region Debugging toggles
        private bool collisionDetectionOn = true;
        private bool gravityEnabled = true;
        #endregion

        #region Sprites & Fonts
        // HUD sprites
        public static Texture2D healthBar;
        public static Texture2D healthIcon;
        public static Texture2D fuelBar;
        public static Texture2D fuelIcon;
		public static Texture2D bossHealthBar;
		public static Texture2D bossHealthShadow;

        // Fonts
        public static SpriteFont gameFont;
#if DEBUG
        public static SpriteFont debugFont;
#endif
        #endregion

        #region Game Settings & States
        private readonly Vector2 activityBoundsSize = new Vector2(2192, 1680);
        private readonly Vector2 gravity = new Vector2(0.0f, -512.0f);
        private bool enginePaused = false;
        #endregion

        #region Game Components
        // Game map
        public static Map map;

        // Player
        public static Core.Player player;

		// The level's boss (null when not fighting)
		public static Character boss;

        // Used to allow the most-recent frame's game time to be known by more things
        public static GameTime gameTime;

        // Sound Engine
        public static Sound sound;

        // Lua Engine
        public static GameLua lua;

        // Rendering Engine
        public Renderer gameRenderer;

        // Bullets
        public static List<Bullet> bullets;
        public static List<Explosion> explosions;

        // Pause Screen
        public GameObject pause;

        // Invisible map bounding objects
        public List<GameObject> mapBounds;

        // list of objects that are colliding in this frame
        private List<GameObject> collidingObjects;

        // Last collision vector
        public static Vector2 collisionLine;
        #endregion

        #region Constructor
        private static Engine instance;
        public static Engine GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Starts up a game engine for the specified map
        /// </summary>
        /// <param name="mapName">The name given to the map in the file system</param>
        public Engine(String mapName)
        {
            if (instance != null)
            {
                #region Cleanup the old instance
                instance = null;
                map = null;
                bullets = null;
                explosions = null;
                lua = null;
                sound = null;
                player = null;
				boss = null;
                System.GC.Collect();
                #endregion
            }
            instance = this;
            sound = SnailsPace.soundManager;
            
            // Stuff needed by maps
            loadFonts();
            
            // Initialize Lua, the Player, and the Map
            lua = new GameLua(mapName);
            map = new Map(mapName);

            // Setup lists for objects that will be used later
            bullets = new List<Bullet>();
            explosions = new List<Explosion>();
            collidingObjects = new List<GameObject>();

            // Load the Fonts, Sprites, and some helper Game Objects that will be needed.
            loadHUD();
            setupPauseOverlay();
            setupMapBounds();

            // Initialize the Renderer
            setupGameRenderer();
        }
        #endregion

        #region Renderer, Font, Sprite, and helper Game Object initialization
        /// <summary>
        /// Creates a game object to be used as an overlay when the game is paused
        /// </summary>
        private void setupPauseOverlay()
        {
            Sprite pauseSprite = new Sprite();
            pauseSprite.image = new Image();
            pauseSprite.image.filename = "Resources/Textures/PauseScreen";
            pauseSprite.image.blocks = new Vector2(1.0f, 1.0f);
            pauseSprite.image.size = new Vector2(800.0f, 600.0f);
            pauseSprite.visible = false;
            pauseSprite.effect = "Resources/Effects/effects";
            pause = new GameObject();
            pause.sprites = new Dictionary<string, Sprite>();
            pause.sprites.Add("Pause", pauseSprite);
            pause.position = new Vector2(0.0f, 0.0f);
            pause.layer = -300;
            pause.collidable = false;
        }

        /// <summary>
        /// Sets up invisible borders to prevent the player from moving off the edge of the map
        /// </summary>
        private void setupMapBounds()
        {
            mapBounds = new List<GameObject>();

            //Build each bounding wall
            Sprite mapBoundsSprite = new Sprite();
            mapBoundsSprite.image = new Image();
            mapBoundsSprite.image.filename = "Resources/Textures/BoundingBox";
            mapBoundsSprite.image.blocks = new Vector2(1.0f);
            mapBoundsSprite.visible = false;
            mapBoundsSprite.effect = "Resources/Effects/effects";

            Vector2 lastPoint = map.bounds[0];
            Vector2 currentPoint;
            GameObject mapBound;

            for (int i = 1; i < map.bounds.Count; i++)
            {
                currentPoint = map.bounds[i];

                mapBoundsSprite = mapBoundsSprite.clone();
                mapBound = new GameObject();

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

        /// <summary>
        /// Loads the fonts used in the game
        /// </summary>
        private void loadFonts()
        {
            gameFont = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Menu");
#if DEBUG
            debugFont = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Debug");
#endif
        }

        /// <summary>
        /// Loads the sprites used for the HUD
        /// </summary>
        private void loadHUD()
        {
            healthBar = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/HealthBar");
            healthIcon = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/HealthIcon");
            fuelBar = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/FuelBar");
            fuelIcon = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/FuelIcon");
			bossHealthBar = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/SaltBar");
			bossHealthShadow = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/BossHealthShadow");
        }

        /// <summary>
        /// Initializes the renderer, sets the renderer to focus on Helix, and tells
        /// it where the bounds of the map are so the camera doesn't move too far
        /// </summary>
        private void setupGameRenderer()
        {
            gameRenderer = new Renderer();
            gameRenderer.createTexturesAndEffects(allObjects());

            Renderer.cameraPosition = new Vector3(Player.helix.position, Renderer.normalCameraDistance);

            Renderer.cameraTarget = Player.helix;

            gameRenderer.cameraBounds = map.bounds.ToArray();
        }
        #endregion

        #region AI
        /// <summary>
        /// Handles input and initiates the AI for all the characters
        /// </summary>
        /// <param name="gameTime">The game time for this update</param>
        public void think(GameTime gameTime)
        {
            Engine.gameTime = gameTime;
            Input input = SnailsPace.inputManager;

            #region Adjust Debug Settings based on input
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
            #endregion

            #region Check for pausing, unpausing, and accessing the menu
            if (input.inputPressed("Pause"))
            {
                enginePaused = !enginePaused;
            }
            if (input.inputPressed("MenuToggle"))
            {
                enginePaused = true;
                SnailsPace.getInstance().changeState(SnailsPace.GameStates.MainMenu);
            }
            if (!SnailsPace.getInstance().IsActive)
            {
                enginePaused = true;
            }
            #endregion

            #region Adjust sounds and sprites based on paused state; Break out of think loop if paused
            pause.sprites["Pause"].visible = enginePaused;

            if (enginePaused)
            {
                sound.pause("music");
                sound.stop("alarm");
                sound.stop("jetpack");
                pause.position = new Vector2(Renderer.cameraPosition.X, Renderer.cameraPosition.Y);
                return;
            }
            else
            {
                sound.play("music", false);
				Player.time += gameTime.ElapsedGameTime.Milliseconds;
            }
            #endregion

            #region Let non-dying characters think, let dying characters die, cleanup dead characters
            List<Character>.Enumerator charEnum = new List<Character>(map.characters).GetEnumerator();
            while (charEnum.MoveNext())
            {
                //Is a character dead? Kill it!
                if (charEnum.Current.health <= 0)
                {
                    if (charEnum.Current.wasLivingLastFrame )
                    {
                        charEnum.Current.sprites["Die"].frame = charEnum.Current.sprites["Die"].animationStart;
                        Engine.lua.CallOn(charEnum.Current.state, "die", gameTime);
                        player.killedEnemy();
                        Engine.sound.play("kill");
                    }
                    charEnum.Current.wasLivingLastFrame = false;
                    if (charEnum.Current.sprites["Die"].frame == charEnum.Current.sprites["Die"].animationEnd)
                    {
                        map.characters.Remove(charEnum.Current);
                    }
                    
                    charEnum.Current.collidable = false;
                    charEnum.Current.setSprite("Die");
                }
                //If not, let it think (as long as it is close enough to Helix).
                else
                {
                    charEnum.Current.wasLivingLastFrame = true;
                    if (IsWithinBounds(charEnum.Current, Player.helix.position, activityBoundsSize))
                    {
                        charEnum.Current.think(gameTime);
                    }
                }
            }
            charEnum.Dispose();
            #endregion

            // Deal with Helix's autonomous behaviors and player input
            player.think(gameTime);
        }
        #endregion

        #region helper Functions

        #region Compiled Lists of Objects and Strings for the level
        /// <summary>
        /// Gets a list of all of the objects in the level
        /// </summary>
        /// <returns></returns>
        private List<GameObject> allObjects()
        {
            List<GameObject> objects = new List<GameObject>(map.objects);

            // The invisible bounds for the map
            objects.AddRange(mapBounds);

            #region Characters
            List<Character>.Enumerator characterEnum = map.characters.GetEnumerator();
            while (characterEnum.MoveNext())
            {
                objects.Add(characterEnum.Current);
            }
            characterEnum.Dispose();
            #endregion

            // Helix
            objects.AddRange(player.gameObjects());

            #region Bullets
            List<Bullet>.Enumerator bulletEnum = bullets.GetEnumerator();
            while (bulletEnum.MoveNext())
            {
                objects.Add(bulletEnum.Current);
            }
            bulletEnum.Dispose();
            #endregion

            #region Explosions
            List<Explosion>.Enumerator explosionEnum = explosions.GetEnumerator();
            while (explosionEnum.MoveNext())
            {
                objects.Add(explosionEnum.Current);
            }
            explosionEnum.Dispose();
            #endregion

            // The pause screen
            objects.Add(pause);

            return objects;
        }

        /// <summary>
        /// Gets a list of all of the strings that should be drawn
        /// </summary>
        /// <returns></returns>
        private List<Text> allStrings()
        {
            List<Text> strings = new List<Text>();
            #region Strings for Debugging
#if DEBUG
            int numDebugStrings = 0;
            List<String> debugStrings = new List<String>();
            if (SnailsPace.debugHelixPosition)
            {
                debugStrings.Add("Helix: (" + Player.helix.position.X + ", " + Player.helix.position.Y + ")");
            }
            if (SnailsPace.debugCameraPosition)
            {
                debugStrings.Add("Camera: (" + Renderer.cameraPosition.X + ", " + Renderer.cameraPosition.Y + ", " + Renderer.cameraPosition.Z + ")");
                Vector3 cameraTargetPosition = gameRenderer.getCameraTargetPosition();
                debugStrings.Add("Target: (" + cameraTargetPosition.X + ", " + cameraTargetPosition.Y + ", " + cameraTargetPosition.Z + ")");

                Vector3 distance = cameraTargetPosition - Renderer.cameraPosition;
                debugStrings.Add("Distance: (" + distance.X + ", " + distance.Y + ", " + distance.Z + ")");
                debugStrings.Add("Crosshair: (" + Player.crosshair.position.X + ", " + Player.crosshair.position.Y + ")");
            }
            List<String>.Enumerator debugStringEnum = debugStrings.GetEnumerator();
            while (debugStringEnum.MoveNext())
            {
                Text debugString = new Text();
                debugString.color = Color.Yellow;
                debugString.content = debugStringEnum.Current;
                debugString.font = debugFont;
                debugString.position = new Vector2(2 * debugFont.Spacing, debugFont.Spacing + numDebugStrings++ * debugFont.LineSpacing);
                debugString.scale = Vector2.One;
                strings.Add(debugString);
            }
            debugStringEnum.Dispose();
#endif
            #endregion
            strings.AddRange(player.textStrings());
            return strings;
        }
        #endregion

        #region Object Positioning & Movement
        public bool IsWithinBounds(GameObject objectToCheck, Vector2 boundsCenter, Vector2 boundsSize)
        {
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

        private bool IsMovingWithinBounds(GameObject movingObject, Vector2 objectMovement, Vector2 boundsSize, Vector2 boundsCenter)
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

        private Vector2 GetObjectVelocity(GameObject movingObject, float elapsedTime)
        {
            Vector2 objectVelocity = Vector2.Zero;
            Vector2 objectAccel = Vector2.Zero;
            // Calculate their velocity after acceleration
            if (movingObject.direction.Length() > 0)
            {
                movingObject.direction.Normalize();
                if (movingObject.desiredMaxVelocity <= 0 || movingObject.desiredMaxVelocity > movingObject.maxVelocity)
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
        #endregion

        #region Coordinate Manipulation
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
        #endregion

        #region Game State Manipulation
        /// <summary>
        /// The level has ended
        /// </summary>
        public void EndLevel()
        {
            ((Screens.LevelOverScreen)SnailsPace.getInstance().getScreen(SnailsPace.GameStates.LevelOver)).initializeScreen = true;
            ((Screens.LevelOverScreen)SnailsPace.getInstance().getScreen(SnailsPace.GameStates.LevelOver)).ready = false;
            SnailsPace.getInstance().changeState(SnailsPace.GameStates.LevelOver);
        }
        #endregion

        #endregion

        #region Collision Detection
        /// <summary>
        /// 
        /// </summary>
        /// <param name="movingObject"></param>
        /// <param name="collidableObjects"></param>
        /// <param name="motionVector"></param>
        /// <param name="remainingCollidableObjects"></param>
        /// <param name="populateRemaining"></param>
        /// <returns></returns>
        private GameObject CheckForCollision(GameObject movingObject, List<GameObject> collidableObjects, Vector2 motionVector )
        {
            if (movingObject.collidable && collisionDetectionOn)
            {
                List<GameObject>.Enumerator collideableObjEnumerator = collidableObjects.GetEnumerator();
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
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Move objects, as long as they don't collide with anything
        /// </summary>
        /// <param name="movingObject">The object that is moving</param>
        /// <param name="collidableObjects">Objects that might collide with the moving object</param>
        /// <param name="elapsedTime">The time (in seconds) since the last update</param>
        /// <param name="boundsSize">The size of the region that collision detection can reliably be performed</param>
        /// <param name="boundsCenter">The center of the region where collision detection can reliably be performed</param>
        private void MoveOrCollide(GameObject movingObject, List<GameObject> collidableObjects, float elapsedTime, Vector2 boundsSize, Vector2 boundsCenter)
        {
            // Make sure the object will actually be moving if it doesn't collide
            if ((gravityEnabled && movingObject.affectedByGravity) || movingObject.velocity.Length() > 0
                    || movingObject.direction.Length() > 0)
            {

                Vector2 objectVelocity = GetObjectVelocity(movingObject, elapsedTime);
                Vector2 objectMovement = Vector2.Multiply(objectVelocity, elapsedTime);

                #region Make sure the object isn't moving outside the bounds
                if (!IsMovingWithinBounds(movingObject, objectMovement, boundsSize, boundsCenter))
                {
                    if (movingObject is Bullet)
                    {
                        bullets.Remove((Bullet)movingObject);
                    }
                    return;
                }
                #endregion

                Vector2 resultingMovement = Vector2.Zero;
                bool upwardCreep = false;

                // Check to see if the object can move all the way without colliding
                GameObject collidedObject = CheckForCollision(movingObject, collidableObjects, objectMovement);
                if (collidedObject == null)
                {
                    // The object didn't collide, move all the way
                    resultingMovement = objectMovement;
                }
                else if (movingObject is Bullet)
                {
                    // If the object collides with a wall and is bounceable, then bounce!
                    if (!(collidedObject is Character) && movingObject.bounceable)
                    {
                        collisionLine.Normalize();
                        collisionLine.X = Math.Abs(collisionLine.X);
                        collisionLine.Y = Math.Abs(collisionLine.Y);
                        float mod = 0.8f, xmod = 0f, ymod = 0f;
                        float diff = Math.Abs(collisionLine.X - collisionLine.Y);

                        if (diff < 0.25)
                        {
                            xmod = -mod;
                            ymod = -mod;
                        }
                        else if (collisionLine.X > collisionLine.Y)
                        {
                            xmod = collisionLine.X * mod;
                            ymod = (1 - collisionLine.Y) * -mod;
                        }
                        else
                        {
                            xmod = (1 - collisionLine.X) * -mod;
                            ymod = collisionLine.Y * mod;
                        }

                        resultingMovement = new Vector2(objectMovement.X * xmod, objectMovement.Y * ymod);
                        objectVelocity = new Vector2(objectVelocity.X * xmod, objectVelocity.Y * ymod);
                    }
                    // The object collided, and was a bullet (damage taken care of below)
                    // If it hit a non-character, or if the bullet is not supposed to move through things, explode
                    else if (!(collidedObject is Character) || ((Bullet)movingObject).destroy)
                    {
                        #region Make the bullet explode!
                        Bullet b = (Bullet)movingObject;
                        bullets.Remove(b);
                        Explosion explosion = b.explosion;
                        explosion.position = b.position;
                        explosions.Add(explosion);
                        if (explosion.cue != "")
                        {
                            Engine.sound.play(explosion.cue);
                        }
                        #endregion
                    }
                }
                else
                {
                    #region Try to move the object a little closer to whatever it collided with

                    bool noSecondXCollision = true;
                    bool noThirdXCollision = true;
                    bool noSecondYCollision = true;
                    float xTick = objectMovement.X * 0.25f;
                    float yTick = objectMovement.Y * 0.25f;

                    // Until we can't get any closer...
                    while ((noThirdXCollision && (Math.Abs(resultingMovement.X) < Math.Abs(objectMovement.X)))
                        || (noSecondYCollision && (Math.Abs(resultingMovement.Y) < Math.Abs(objectMovement.Y))))
                    {
                        #region If we can move closer vertically, move a little closer
                        if (noSecondYCollision && yTick != 0)
                        {
                            resultingMovement.Y += yTick;
                            collidedObject = CheckForCollision(movingObject, collidableObjects, resultingMovement);
                            if (collidedObject != null)
                            {
                                noSecondYCollision = false;
                                resultingMovement.Y -= yTick;
                            }
                        }
                        #endregion

                        #region If we can move closer horizontally, move a little closer
                        if (noSecondXCollision && xTick != 0)
                        {
                            resultingMovement.X += xTick;
                            collidedObject = CheckForCollision(movingObject, collidableObjects, resultingMovement);
                            if (collidedObject != null)
                            {
                                noSecondXCollision = false;
                                resultingMovement.X -= xTick;
                            }
                        }
                        #endregion
                        #region If we failed to move closer horizontally before, move up a little while moving horizontally (walk up gentle slopes)
                        else if (!noSecondXCollision)
                        {
                            resultingMovement.X += xTick;
                            resultingMovement.Y += elapsedTime * 32;
                            collidedObject = CheckForCollision(movingObject, collidableObjects, resultingMovement);
                            if (collidedObject == null)
                            {
                                // We could move! Continue creeping closer to our target
                                noSecondXCollision = true;
                                upwardCreep = true;
                            }
                            else
                            {
                                // We couldn't move, stop trying to move horizontally
                                resultingMovement.X -= xTick;
                                resultingMovement.Y -= elapsedTime * 32;
                                noThirdXCollision = false;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                
                #region If it was Helix that moved, check to see if his movement caused him to start or stop flying
                if (movingObject is Helix)
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
                        if (!upwardCreep)
                        {
                            Player.helix.flying = true;
                        }
                    }
                }
                #endregion

                #region If the object didn't move, collide with whatever got in the way
                if (resultingMovement.Length() == 0)
                {
                    movingObject.collidedWith(collidedObject);
                    if (movingObject is Bullet)
                    {
                        // Keep moving at full speed
                        resultingMovement = objectMovement;
                    }
                    else
                    {
                        // Reset velocity and gravitational velocity
                        movingObject.velocity = Vector2.Zero;
                        movingObject.velocityFromGravity = Vector2.Zero;
                    }
                }
                #endregion

                #region Update the object's velocity, position, and velocity from gravity appropriately
                movingObject.position += resultingMovement;
                movingObject.velocity = objectVelocity;
                if (resultingMovement.Y >= 0)
                {
                    movingObject.velocityFromGravity = Vector2.Zero;
                }
                else
                {
                    if (movingObject.velocity.Y > movingObject.velocityFromGravity.Y)
                    {
                        movingObject.velocityFromGravity.Y = movingObject.velocity.Y;
                    }
                }
                if (resultingMovement.Y == 0)
                {
                    movingObject.velocity.Y = 0;
                }
                if (resultingMovement.X == 0)
                {
                    movingObject.velocity.X = 0;
                }
                if (movingObject is Bullet)
                {
                    if (movingObject.bounceable)
                    {
                        movingObject.bounceTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (movingObject.bounceTime < 0)
                        {
                            #region Make the bullet explode!
                            Bullet b = (Bullet)movingObject;
                            bullets.Remove(b);
                            Explosion explosion = b.explosion;
                            explosion.position = b.position;
                            explosions.Add(explosion);
                            if (explosion.cue != "")
                            {
                                Engine.sound.play(explosion.cue);
                            }
                            #endregion
                        }
                    }
                    if (movingObject.affectedByGravity)
                    {
                        Vector2 normalized = new Vector2(movingObject.velocity.X, movingObject.velocity.Y);
                        normalized.Normalize();

                        float mod = normalized.X >= 0 ? -1 : 1;
                        float tvel = movingObject.maxVelocity;
                        float vel = movingObject.velocity.Length();
                        float value = Math.Min(tvel / vel, 1.0f);

                        movingObject.rotation += mod * value * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        /// TODO: Make it so we don't need to recreate GOB's
                        movingObject.bounds = new GameObjectBounds(movingObject.size, movingObject.position, movingObject.rotation);
                    }
                }
                #endregion
            }
        }
        #endregion

        #region Physics
        /// <summary>
        /// Movement and Collision Detection
        /// </summary>
        /// <param name="gameTime">The game time for this update</param>
        public void physics(GameTime gameTime)
        {
            // If it's paused, don't do anything
            if (enginePaused)
            {
                return;
            }

            // Don't let things act as if more than half a second has elapsed (to minimize impact of lag & debugging)
            float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 0.5);

            #region Get rid of out of range bullets
            {
                List<Bullet>.Enumerator bulletEnumerator = new List<Bullet>(bullets).GetEnumerator();
                while (bulletEnumerator.MoveNext())
                {
                    if (bulletEnumerator.Current.range < Vector2.Distance(bulletEnumerator.Current.createPosition, bulletEnumerator.Current.position))
                    {
                        bullets.Remove(bulletEnumerator.Current);
                    }
                }
                bulletEnumerator.Dispose();
            }
            #endregion

            #region Collision Detection
            {
                List<GameObject> collidableObjects = new List<GameObject>();
                #region Determine what objects are close enough to Helix to even care about collision detection
                List<GameObject>.Enumerator objEnum = allObjects().GetEnumerator();
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
                            if (objEnum.Current is Bullet)
                            {
                                bullets.Remove((Bullet)objEnum.Current);
                            }
                        }
                    }
                }
                objEnum.Dispose();
                #endregion

                #region Move everything, checking for collisions
                List<GameObject>.Enumerator collideableObjectEnum = collidableObjects.GetEnumerator();
                while (collideableObjectEnum.MoveNext())
                {
                    MoveOrCollide(collideableObjectEnum.Current, collidableObjects, elapsedTime, activityBoundsSize, boundsCenter);
                }
                #endregion
            }
            #endregion

            #region Check explosions against characters
            {
                List<Character> characterList = new List<Character>(map.characters);
                characterList.Add(Player.helix);

                List<Explosion>.Enumerator explosionEnumerator = new List<Explosion>(explosions).GetEnumerator();
                while (explosionEnumerator.MoveNext())
                {
                    Explosion explosion = explosionEnumerator.Current;
                    if (explosion.damage <= 0)
                        continue;

                    List<Character>.Enumerator characterEnumerator = characterList.GetEnumerator();
                    while (characterEnumerator.MoveNext())
                    {
                        Character character = characterEnumerator.Current;

                        if (character.bounds.WillIntersect(explosion.bounds, Vector2.Zero, false))
                        {
                            if (!explosion.damaged.Contains(character))
                            {
                                character.takeDamage(explosion.damage);
                                explosion.damaged.Add(character);
                            }
                        }
                    }
                    characterEnumerator.Dispose();
                }
                explosionEnumerator.Dispose();
            }
            #endregion

            #region Check all triggers against Helix
            {
                List<Trigger>.Enumerator triggers = map.triggers.GetEnumerator();
                while (triggers.MoveNext())
                {
                    Trigger trigger = triggers.Current;

                    //if (trigger.bounds.containsPoint(Player.helix.position.X, Player.helix.position.Y))
                    if (trigger.bounds.WillIntersect(Player.helix.bounds, Vector2.Zero, false))
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
            }
            #endregion

            #region Animate everything
            {
                #region Map Objects
                List<GameObject>.Enumerator objEnumerator = map.objects.GetEnumerator();
                while (objEnumerator.MoveNext())
                {
                    Dictionary<string, Sprite>.ValueCollection.Enumerator sprtEnumerator = objEnumerator.Current.sprites.Values.GetEnumerator();
                    while (sprtEnumerator.MoveNext())
                    {
                        sprtEnumerator.Current.animate(gameTime);
                    }
                    sprtEnumerator.Dispose();
                }
                objEnumerator.Dispose();
                #endregion

                #region Bullets
                List<Bullet>.Enumerator bulletEnumerator = Engine.bullets.GetEnumerator();
                while (bulletEnumerator.MoveNext())
                {
                    Dictionary<string, Sprite>.ValueCollection.Enumerator sprtEnumerator = bulletEnumerator.Current.sprites.Values.GetEnumerator();
                    while (sprtEnumerator.MoveNext())
                    {
                        sprtEnumerator.Current.animate(gameTime);
                    }
                    sprtEnumerator.Dispose();
                }
                bulletEnumerator.Dispose();
                #endregion

                #region Characters
                List<Character>.Enumerator charEnumerator = map.characters.GetEnumerator();
                while (charEnumerator.MoveNext())
                {
                    #region Turn the character in the proper direction
                    if (charEnumerator.Current.velocity.X > 0)
                    {
                        charEnumerator.Current.horizontalFlip = false;
                    }
                    else if (charEnumerator.Current.velocity.X < 0)
                    {
                        charEnumerator.Current.horizontalFlip = true;
                    }
                    #endregion

                    #region Animate the character
                    Dictionary<string, Sprite>.ValueCollection.Enumerator sprtEnumerator = charEnumerator.Current.sprites.Values.GetEnumerator();
                    while (sprtEnumerator.MoveNext())
                    {
                        sprtEnumerator.Current.animate(gameTime);
                    }
                    sprtEnumerator.Dispose();
                    #endregion
                }
                charEnumerator.Dispose();
                #endregion

                #region Explosions
                List<Explosion>.Enumerator explosionEnumerator = new List<Explosion>(explosions).GetEnumerator();
                while (explosionEnumerator.MoveNext())
                {
                    if (!explosionEnumerator.Current.DoAnimation(gameTime))
                    {
                        // Get rid of the explosion - it's done exploding
                        explosions.Remove(explosionEnumerator.Current);
                    }
                }
                explosionEnumerator.Dispose();
                #endregion
            }
            #endregion
        }
        #endregion

        #region Rendering
        /// <summary>
        /// Tell the renderer it's time for it to do its thing
        /// </summary>
        /// <param name="gameTime">The time for the current update</param>
        public void render(GameTime gameTime)
        {
            gameRenderer.render(allObjects(), allStrings(), gameTime);
        }
        #endregion

    }
}
