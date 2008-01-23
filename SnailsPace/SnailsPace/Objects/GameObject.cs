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
		public Vector2 size;
		public float rotation;
        public Vector2 velocity;
		public float maxVelocity;
        public bool affectedByGravity;
		public bool collidable = true;
		public bool horizontalFlip;

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
			return intersects(otherObject);
		}

		public Rectangle getBounds()
		{
			// Get all four corners in local space
			Vector2 leftTop = new Vector2(position.X, position.Y);
			Vector2 rightTop = new Vector2(position.X + size.X, position.Y);
			Vector2 leftBottom = new Vector2(position.X, position.Y + size.Y);
			Vector2 rightBottom = new Vector2(position.X + size.X, position.X + size.Y);

			Matrix transform =
					Matrix.CreateTranslation(new Vector3(-(new Vector2(size.X / 2.0f, size.Y / 2.0f)), 0.0f)) *
				// Matrix.CreateScale(block.Scale) *  would go here
					Matrix.CreateRotationZ(rotation) *
					Matrix.CreateTranslation(new Vector3(position, 0.0f));

			Vector2.Transform(ref leftTop, ref transform, out leftTop);
			Vector2.Transform(ref rightTop, ref transform, out rightTop);
			Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
			Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

			// Find the minimum and maximum extents of the rectangle in world space
			Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
									  Vector2.Min(leftBottom, rightBottom));
			Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
									  Vector2.Max(leftBottom, rightBottom));

			// Return that as a rectangle
			return new Rectangle((int)min.X, (int)min.Y,
								 (int)(max.X - min.X), (int)(max.Y - min.Y));
		}
    }
}
