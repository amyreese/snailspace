using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
	/// <summary>
	/// Simplifies bounding box creation from Lua classes.
	/// </summary>
	public class GameObjectBoundsBuilder
	{
		private List<Vector2> points = new List<Vector2>();

		/// <summary>
		/// Add a point to the new bounding box.
		/// </summary>
		/// <param name="point">A point.</param>
		public void AddPoint(Vector2 point)
		{
			points.Add(point);
		}

		/// <summary>
		/// Create a GameObjectBounds object from the added points.
		/// </summary>
		/// <returns>A GameObjectBounds object.</returns>
		public GameObjectBounds BuildBounds()
		{
			return new GameObjectBounds(points.ToArray());
		}
	}
}
