using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
	public class GameObjectBounds
	{
		private Vector2[] points;
		public Vector2[] GetPoints()
		{
			return (Vector2[])points.Clone();
		}
		internal GameObjectBounds(Vector2 size, Vector2 position, float rotation)
		{
			points = new Vector2[4];

			// Get all four corners in local space
			points[0] = new Vector2(-size.X / 2.0f, size.Y / 2.0f);
			points[1] = new Vector2(size.X / 2.0f, size.Y / 2.0f);
			points[2] = new Vector2(size.X / 2.0f, -size.Y / 2.0f);
			points[3] = new Vector2(-size.X / 2.0f, -size.Y / 2.0f);

			Matrix transform =
					Matrix.CreateRotationZ(rotation) *
					Matrix.CreateTranslation(new Vector3(position, 0.0f));

			for (int index = 0; index < points.Length; index++)
			{
				Vector2.Transform(ref points[index], ref transform, out points[index]);
			}
		}


		internal bool Intersects(GameObjectBounds otherBounds)
		{
			for (int myIndex = 0; myIndex < points.Length; myIndex++)
			{
				int myIndex2 = myIndex + 1;
				if (myIndex2 == points.Length)
				{
					myIndex2 = 0;
				}
				for (int otherIndex = 0; otherIndex < otherBounds.points.Length; otherIndex++)
				{
					int otherIndex2 = otherIndex + 1;
					if (otherIndex2 == otherBounds.points.Length)
					{
						otherIndex2 = 0;
					}

					Vector2 a = points[myIndex];
					Vector2 b = points[myIndex2];
					Vector2 c = otherBounds.points[otherIndex];
					Vector2 d = otherBounds.points[otherIndex2];
					float denom = ((b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X));
					float r = ((a.Y - c.Y) * (d.X - c.X) - (a.X - c.X) * (d.Y - c.Y)) / denom;
					float s = ((a.Y - c.Y) * (b.X - a.X) - (a.X - c.X) * (b.Y - a.Y)) / denom;
					if (0 <= r && r <= 1 && 0 <= s && s <= 1)
					{
						return true;
					}
				}
			}
			return false;
		}

		public float Width
		{
			get
			{
				return Math.Abs( points[1].X - points[0].X );
			}
		}

		public float Height
		{
			get
			{
				return Math.Abs( points[0].Y - points[2].Y );
			}
		}

		public float X
		{
			get
			{
				return Math.Min( points[0].X, points[2].X ) + Width / 2;
			}
		}

		public float Y
		{
			get
			{
				return Math.Max( points[0].Y, points[1].Y) - Height / 2;
			}
		}
	}
}
