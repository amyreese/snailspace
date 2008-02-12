using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnailsPace.Objects;

namespace SnailsPace.Core
{
    class Player
    {
        private int points = 0;

        public static Objects.Helix helix;
        public static Objects.GameObject crosshair;
        public static GameObject saveObject;

        public static bool dead = false;
        public double deathTimer = 0;

        public Player()
            : this(new Vector2(0, 0))
        {    
        }

        public Player(Vector2 startPosition)
            : this(startPosition, "generic")
        {
        }

        public Player(Vector2 startPosition, String weaponName)
            : base()
        {
            saveObject = new GameObject();
            saveObject.affectedByGravity = false;
            saveObject.collidable = false;

            save(startPosition);
            helix = new Helix( startPosition, weaponName );

            // Crosshair creation
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

            strings = new List<Text>();
            pointsText = new Text();
            pointsText.position = new Vector2(400, 0);
            pointsText.font = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Score");
            pointsText.color = Color.White;
            pointsText.scale = Vector2.One;
            pointsText.content = points.ToString();
            strings.Add(pointsText);
            recalculatePoints();
            Engine.player = this;
        }

        public void think(GameTime gameTime)
        {
            // Update things that depend on mouse position
			crosshair.position = Engine.mouseToGame(SnailsPace.inputManager.mousePosition);

            if (helix.health <= 0)
            {
                if (!dead)
                {
                    dead = true;
                    deathTimer = 0;
                    helix.health = 0;
                    helix.fuel = 0;
                    helix.collidable = false;

                    helix.sprites["NoFuel"].visible = false;
                    Engine.sound.stop("alarm");
                    
                    Engine.sound.play("death");
                }
                else
                {
                    deathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (deathTimer > 3000)
                    {
                        dead = false;
                        SnailsPace.inputManager.reset();
                        
                        load();
                    }
                    else if (deathTimer > 2000)
                    {
                        Renderer.cameraTarget = saveObject;
                        Renderer.cameraTargetOffset = new Vector3();
                    }
                    else if (deathTimer > 1500)
                    {
                        Dictionary<String, Sprite>.Enumerator sprites = helix.sprites.GetEnumerator();
                        while (sprites.MoveNext())
                        {
                            sprites.Current.Value.visible = false;
                        }
                    }
                }
            }
            else
            {
                helix.think(gameTime);
            }
        }

        public List<GameObject> gameObjects()
        {
            List<GameObject>objects = new List<GameObject>();
            
            objects.Add(helix);
            objects.Add(crosshair);

            return objects;
        }

        private Text pointsText;
        private List<Text> strings;
        public List<Text> textStrings()
        {
            return strings;
        }

        private int shotsFired = 0;
        public void shotBullet()
        {
            shotsFired++;
            recalculatePoints();
        }

        private int enemiesHit = 0;
        public void enemyHit()
        {
            enemiesHit++;
            recalculatePoints();
        }

        private int timesHit = 0;
        public void gotHit()
        {
            timesHit++;
            recalculatePoints();
        }

        private int enemiesKilled = 0;
        public void killedEnemy()
        {
            enemiesKilled++;
            recalculatePoints();
        }

        public int deaths = 0;

        public void recalculatePoints()
        {
            points = enemiesKilled * 100 - timesHit * 10 - deaths * 100;
            if (shotsFired > 0)
            {
                points += (int)((100.0f * enemiesHit * enemiesHit) / shotsFired );
            }
            pointsText.content = "Score: " + points.ToString();
        }

        public void save(Vector2 position)
        {
            saveObject.position = position;
        }

        public void load()
        {
            helix.position = saveObject.position;
            helix.velocity = new Vector2();

            helix.health = helix.maxHealth;
            helix.fuel = helix.maxFuel;

            helix.collidable = true;
            helix.horizontalFlip = false;
            helix.sprites["Gun"].horizontalFlip = false;

            Engine.sound.play("ready");

            Renderer.cameraTarget = helix;
            Renderer.cameraTargetOffset = new Vector3();
        }
    }
}
