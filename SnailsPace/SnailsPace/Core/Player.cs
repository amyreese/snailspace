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

        public static Helix helix;
        public static GameObject crosshair;
        public static GameObject weapon;
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
            Sprite crosshairSprite = new Sprite();
			crosshairSprite.image = new Image();
			crosshairSprite.image.filename = "Resources/Textures/Crosshair";
			crosshairSprite.image.blocks = new Vector2(1.0f, 1.0f);
			crosshairSprite.image.size = new Vector2(64.0f, 64.0f);
			crosshairSprite.visible = true;
			crosshairSprite.effect = "Resources/Effects/effects";
			crosshair = new GameObject();
			crosshair.sprites = new Dictionary<string, Sprite>();
			crosshair.sprites.Add("Crosshair", crosshairSprite);
			crosshair.position = new Vector2(0.0f, 0.0f);
			crosshair.layer = 0;
			crosshair.collidable = false;

            // Weapon
            weapon = new GameObject();
            weapon.sprites = new Dictionary<string, Sprite>();
            weapon.sprites.Add("Weapon", helix.weapon.sprite);
            weapon.position = helix.position;
            weapon.layer = -5;
            weapon.collidable = false;

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

            // Handle life and death
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

            Sprite weaponSprite = helix.weapon.sprite.clone();
            
            weapon.horizontalFlip = weaponSprite.horizontalFlip = helix.horizontalFlip;
            weaponSprite.rotation = ((crosshair.position.X - helix.position.X) < 0 ? MathHelper.Pi : 0) + (float)Math.Atan((crosshair.position.Y - helix.position.Y) / (crosshair.position.X - helix.position.X));
            weapon.sprites["Weapon"] = weaponSprite;
            weapon.position = helix.position;
            
            objects.Add(helix);
            objects.Add(weapon);
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

            Engine.sound.play("ready");

            Renderer.cameraTarget = helix;
            Renderer.cameraTargetOffset = new Vector3();
        }
    }
}
