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
		public float horizontalFriction = 640.0f;

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
			return bounds.WillIntersect(otherObject.bounds, movementVector);
		}

		public Rectangle getRectangle()
		{
			return new Rectangle((int)(position.X - (size.X / 2)), (int)(position.Y + (size.Y / 2)), (int)(size.X), (int)(size.Y));
		}
    }
}
