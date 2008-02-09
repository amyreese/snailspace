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
        private Vector2 savedPosition = new Vector2(0, 0);

        public static Objects.Helix helix;
        public static Objects.GameObject crosshair;

        public Player()
            : this(new Vector2(0, 0))
        {    
        }

        public Player( Vector2 startPosition )
        {
            savedPosition = startPosition;
            helix = new Helix( startPosition );

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
                deaths++;
				helix.position = load();
				helix.health = helix.maxHealth;
			}

            helix.think(gameTime);
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
            savedPosition = position;
        }

        public Vector2 load()
        {
            return savedPosition;
        }
    }
}
