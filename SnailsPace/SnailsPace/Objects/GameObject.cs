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
		public float desiredMaxVelocity;
		public float maxVelocity;
		public float terminalVelocity = 640.0f;
		public Vector2 direction;
		public float acceleration = 640.0f;
		public float horizontalFriction = 1280.0f;

        public bool affectedByGravity;
		public bool collidable = true;
		public bool horizontalFlip;

		private GameObjectBounds _bounds;
		public GameObjectBounds bounds
		{
			get
			{
				if (_bounds == null)
				{
					_bounds = new GameObjectBounds(size, position, rotation);
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

		public virtual bool canCollideWith(GameObject otherObject)
		{
			return true;
		}

		public virtual void collidedWith(GameObject otherObject)
		{
			// Do nothing?
		}

		public bool willIntersect(Vector2 movementVector, GameObject otherObject)
		{
            float left = position.X - size.X / 2;
            left = Math.Min(left, left + movementVector.X);
            float otherRight = otherObject.position.X + otherObject.size.X / 2;
            if (left > otherRight)
            {
                return false;
            }
            float right = position.X + size.X / 2;
            right = Math.Max(right, right + movementVector.X);
            float otherLeft = otherObject.position.X - otherObject.size.X / 2;
            if (otherLeft > right)
            {
                return false;
            }

            float bottom = position.Y - size.Y / 2;
            bottom = Math.Min(bottom, bottom + movementVector.Y);
            float otherTop = otherObject.position.Y + otherObject.size.Y / 2;
            if (bottom > otherTop)
            {
                return false;
            }

            float top = position.Y + size.Y / 2;
            top = Math.Max(top, top + movementVector.Y);
            float otherBottom = otherObject.position.Y - otherObject.size.Y / 2;
            if (otherBottom > top)
            {
                return false;
            }
			return bounds.WillIntersect(otherObject.bounds, movementVector);
		}

		public Rectangle getRectangle()
		{
			return new Rectangle((int)(position.X - (size.X / 2)), (int)(position.Y + (size.Y / 2)), (int)(size.X), (int)(size.Y));
		}
    }
}
