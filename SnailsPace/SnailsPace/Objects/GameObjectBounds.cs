using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Objects
{
	class GameObjectBounds
	{
		protected Rectangle boundingRectangle;

		public GameObjectBounds(Rectangle rectangle)
		{
			boundingRectangle = rectangle;
		}

		public bool intersects(GameObjectBounds otherObject)
		{
			return boundingRectangle.Intersects(otherObject.boundingRectangle);
		}

		public void move(Vector2 v)
		{
			boundingRectangle.X += (int)Math.Round(v.X);
			boundingRectangle.Y += (int)Math.Round(v.Y);
		}
	}
}
