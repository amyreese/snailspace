using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using LuaInterface;

namespace SnailsPace.Objects
{
    class GameObject
	{
		#region GameObject properties
		// The game object's position and velocity.
		private Vector2 _position;
		public Vector2 position
		{
			get
			{
				return _position;
			}
			set
			{
				bounds.Move(value - _position);
				_position = value;
			}
		}

		public Vector2 startPosition;
		public String behavior;
		private String _name;
		public String name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public Vector2 size;
        public Vector2 scale = new Vector2(1,1);

		private float _rotation;
		public float rotation {
			get {
				return _rotation;
			}
			set {
				bounds.Rotate(value);
				_rotation = value;
			}
		}

        public Vector2 velocity;
		public Vector2 velocityFromGravity;
		public float desiredMaxVelocity;
		public float maxVelocity;
		public float terminalVelocity = 512.0f;
		public Vector2 direction;
		public float acceleration = 3840.0f;
		public float horizontalFriction = 2560.0f;

        public bool affectedByGravity;
		public bool collidable = true;
		public bool horizontalFlip;

        public bool bounceable = false;
        public double bounceTime = 0;

		private GameObjectBounds _bounds;
		public GameObjectBounds bounds
		{
			get
			{
				if (_bounds == null)
				{
					_bounds = new GameObjectBounds(Vector2.Multiply(size, scale), position, rotation);
				}
				return _bounds;
			}
			set
			{
				_bounds = value;
			}
		}

        // Object state for Lua
        public LuaTable state;

        // The sprite's layer and parallax.
        public float layer;       // 0: background ... 5: foreground
        
        // Sprites that make up the game object.
        public Dictionary<String, Sprite> sprites;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
        public GameObject()
        {
            sprites = new Dictionary<string,Sprite>();
        }

		/// <summary>
		/// Set the GameObject's visible sprite.
		/// </summary>
		/// <param name="sprtName">The name of the visible sprite.</param>
        public void setSprite(String sprtName)
        {
            Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = sprites.Values.GetEnumerator();
            while (sprtEnumerator.MoveNext())
            {
                sprtEnumerator.Current.visible = false;
            }
            if (sprites.ContainsKey(sprtName))
            {
                sprites[sprtName].visible = true;
            }
        }

		/// <summary>
		/// Set the GameObject's visible sprites.
		/// </summary>
		/// <param name="sprtName">The name of the first visible sprite.</param>
		/// <param name="sprtName2">The name of the second visible sprite.</param>
        public void setSprites(String sprtName, String sprtName2)
        {
            Dictionary<string, Objects.Sprite>.ValueCollection.Enumerator sprtEnumerator = sprites.Values.GetEnumerator();
            while (sprtEnumerator.MoveNext())
            {
                sprtEnumerator.Current.visible = false;
            }
            if (sprites.ContainsKey(sprtName))
            {
                sprites[sprtName].visible = true;
            }
            if (sprites.ContainsKey(sprtName2))
            {
                sprites[sprtName2].visible = true;
            }
        }

		/// <summary>
		/// Determine if this GameObject can collide with another GameObject.
		/// </summary>
		/// <param name="otherObject">The other GameObject.</param>
		/// <returns>If these objects can collide with each other.</returns>
		public virtual bool canCollideWith(GameObject otherObject)
		{
			return true;
		}

		/// <summary>
		/// Function called when this GameObject collides with another GameObject.
		/// </summary>
		/// <param name="otherObject">The other GameObject.</param>
		public virtual void collidedWith(GameObject otherObject)
		{
			// Do nothing?
		}

		/// <summary>
		/// Determines whether another GameObject will collide with this GameObject if it follows a specified movement vector.
		/// </summary>
		/// <param name="movementVector">The movement vector of the other GameObject.</param>
		/// <param name="otherObject">The other GameObject.</param>
		/// <returns>True if these GameObjects will intersect.</returns>
		public bool willIntersect(Vector2 movementVector, GameObject otherObject)
		{	
			// Check for obvious intersections in each direction.
            float left = position.X - size.X / 2;
            left = Math.Min(left, left + movementVector.X);
            if (left > otherObject.bounds.Right)
            {
                return false;
            }

			float right = position.X + size.X / 2;
            right = Math.Max(right, right + movementVector.X);
            if (otherObject.bounds.Left > right)
            {
                return false;
            }

			float bottom = position.Y - size.Y / 2;
            bottom = Math.Min(bottom, bottom + movementVector.Y);
            if (bottom > otherObject.bounds.Top)
            {
                return false;
            }

			float top = position.Y + size.Y / 2;
            top = Math.Max(top, top + movementVector.Y);
            if (otherObject.bounds.Bottom > top)
            {
                return false;
            }

			// If no obvious intersections were found, check point by point using the GameObjectBounds.
			return bounds.WillIntersect(otherObject.bounds, movementVector, this is Bullet);
		}

		/// <summary>
		/// Get a rectangle object that surrounds this GameObject.
		/// </summary>
		/// <returns>A bounding rectangle for this GameObject.</returns>
		public Rectangle getRectangle()
		{
			return new Rectangle((int)(position.X - (size.X / 2)), (int)(position.Y + (size.Y / 2)), (int)(size.X), (int)(size.Y));
		}
    }
}
