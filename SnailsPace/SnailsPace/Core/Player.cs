using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SnailsPace.Objects;

namespace SnailsPace.Core
{
    class Player
    {
        private int points = 0;
        private Vector2 savedPosition = new Vector2(0, 0);

        public Objects.Helix helix;

        public Player()
            : this(new Vector2(0, 0))
        {    
        }

        public Player( Vector2 startPosition )
        {
            savedPosition = startPosition;
            helix = new Helix( startPosition );
        }

        public void think(GameTime gameTime)
        {
            helix.think(gameTime);
        }

        public List<GameObject> gameObjects()
        {
            return new List<GameObject>();
        }

        public List<Text> textStrings()
        {
            return new List<Text>();
        }

        public void addPoints(int points)
        {
            this.points += points;
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
