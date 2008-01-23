using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
    class GameObject
    {
        // The game object's position and velocity.
        public Vector2 position;
        public float rotation;
        public Vector2 velocity;
		public float maxVelocity;
        public bool affectedByGravity;
		public bool collidable = true;
		public bool horizontalFlip;

		public GameObjectBounds bounds;

        // The sprite's layer and parallax.
        public float layer;       // 0: background ... 5: foreground
        
        // Sprites that make up the game object.
        public Dictionary<String, Sprite> sprites;     
   
        /**
         * Initialize object's sprite dictionary.
         */
        public GameObject()
        {
            sprites = new Dictionary<string,Sprite>();
        }

		public void setSprite(String sprtName)
		{
			Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = this.sprites.Values.GetEnumerator();
			while (sprtEnumerator.MoveNext())
			{
				sprtEnumerator.Current.visible = false;
			}
			this.sprites[sprtName].visible = true;
		}

		public virtual bool collidedWith(GameObject otherObject)
		{
			// Do nothing?
			return true;
		}
    }
}
