using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
	public class GameObjectBoundsBuilder
	{
		private List<Vector2> points = new List<Vector2>();

		public void AddPoint(Vector2 point)
		{
			points.Add(point);
		}
		public GameObjectBounds BuildBounds()
		{
			return new GameObjectBounds(points.ToArray());
		}
	}
}
