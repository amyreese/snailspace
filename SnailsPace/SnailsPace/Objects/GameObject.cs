using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using LuaInterface;

namespace SnailsPace.Objects
{
    class GameObject
    {
        // The game object's position and velocity.
        public Vector2 position;
		public Vector2 size;
		public float rotation;
        public Vector2 velocity;
		public float maxVelocity;
        public bool affectedByGravity;
		public bool collidable = true;
		public bool horizontalFlip;

        // Object state for Lua
        public LuaTable state;

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

		public bool intersects(GameObject otherObject)
		{
			return getBounds().Intersects(otherObject.getBounds());
		}

		public bool willIntersect(Vector2 movementVector, GameObject otherObject)
		{
			// TODO: Do this better - we shouldn't be adjusting the object's position all the time for this
			position += movementVector;
			bool retval = intersects(otherObject);
			position -= movementVector;
			return retval;
		}

		public GameObjectBounds getBounds()
		{
			return new GameObjectBounds(size, position, rotation);
		}
    }
}
