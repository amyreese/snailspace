using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
	public class GameObjectBounds
	{
		#region GameObjectBounds private properties
		private Vector2 center;
		private Vector2[] points;
		private float rotation;
		#endregion

		#region GameObjectBounds public properties
		public float Left
		{
			get
			{
				float val = points[0].X;
				for (int i = 1; i < points.Length; i++)
					val = MathHelper.Min(val, points[i].X);
				return val;
			}
		}

		public float Right
		{
			get
			{
				float val = points[0].X;
				for (int i = 1; i < points.Length; i++)
					val = MathHelper.Max(val, points[i].X);
				return val;
			}
		}

		public float Top
		{
			get
			{
				float val = points[0].Y;
				for (int i = 1; i < points.Length; i++)
					val = MathHelper.Max(val, points[i].Y);
				return val;
			}
		}

		public float Bottom
		{
			get
			{
				float val = points[0].Y;
				for (int i = 1; i < points.Length; i++)
					val = MathHelper.Min(val, points[i].Y);
				return val;
			}
		}

		public float Width
		{
			get
			{
				return Math.Abs(points[1].X - points[0].X);
			}
		}

		public float Height
		{
			get
			{
				return Math.Abs(points[0].Y - points[2].Y);
			}
		}

		public float X
		{
			get
			{
				return Math.Min(points[0].X, points[2].X) + Width / 2;
			}
		}

		public float Y
		{
			get
			{
				return Math.Max(points[0].Y, points[1].Y) - Height / 2;
			}
		}

		/// <summary>
		/// Get the bounding box as an array of Vector2.
		/// </summary>
		public Vector2[] Points
		{
			get
			{
				return (Vector2[])points.Clone();
			}
		}
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="points">A bounding box specified by any number of points.</param>
		public GameObjectBounds(Vector2[] points)
		{
			this.points = points;
		}

		/// <summary>
		/// Construct a GameObjectBounds object from size, position, and rotation.
		/// </summary>
		/// <param name="size">The object's size.</param>
		/// <param name="position">The position of the center of the object.</param>
		/// <param name="rotation">The rotation of the object.</param>
		public GameObjectBounds(Vector2 size, Vector2 position, float rotation)
		{
			this.center = position;
			this.rotation = rotation;
			points = new Vector2[4];

			// Get all four corners in local space
			points[0] = new Vector2(-size.X / 2.0f, size.Y / 2.0f);
			points[1] = new Vector2(size.X / 2.0f, size.Y / 2.0f);
			points[2] = new Vector2(size.X / 2.0f, -size.Y / 2.0f);
			points[3] = new Vector2(-size.X / 2.0f, -size.Y / 2.0f);

			// Rotate and translate each point
			Matrix transform =
					Matrix.CreateRotationZ(rotation) *
					Matrix.CreateTranslation(new Vector3(position, 0.0f));

			for (int index = 0; index < points.Length; index++)
			{
				Vector2.Transform(ref points[index], ref transform, out points[index]);
			}
		}

		/// <summary>
		/// Move the GameObjectBounds by a specified offset.
		/// </summary>
		/// <param name="offset">The amount to move the GameObjectBounds.</param>
		internal void Move(Vector2 offset)
		{
			if (offset.Length() > 0)
			{
				center = center + offset;
				for (int index = 0; index < points.Length; index++)
				{
					points[index] += offset;
				}
			}
		}

		/// <summary>
		/// Rotate the GameObjectBounds by a specified rotation.
		/// </summary>
		/// <param name="newRotation">The new rotation for the GameObjectBounds.</param>
		internal void Rotate(float newRotation)
		{
			Matrix transform = Matrix.CreateRotationZ(-rotation) * Matrix.CreateRotationZ(newRotation);
			rotation = newRotation;
			for (int index = 0; index < points.Length; index++)
			{
				Vector2.Transform(ref points[index], ref transform, out points[index]);
			}
		}

		/// <summary>
		/// Determine if two GameObjectBounds objects will intersect.
		/// </summary>
		/// <param name="otherBounds">The other GameObjectBounds object.</param>
		/// <param name="movementVector">The movement vector of the other GameObjectBounds object.</param>
		/// <param name="output"></param>
		/// <returns>If these GameObjectBounds objects will intersect.</returns>
		internal bool WillIntersect(GameObjectBounds otherBounds, Vector2 movementVector, bool output)
		{
			Vector2[] intersectionPoints = new Vector2[points.Length];
			for (int index = 0; index < points.Length; index++)
			{
				intersectionPoints[index] = points[index] + movementVector;
			}
			for (int otherIndex = 0; otherIndex < otherBounds.points.Length; otherIndex++)
			{
				int otherIndex2 = otherIndex + 1;
				if (otherIndex2 == otherBounds.points.Length)
				{
					otherIndex2 = 0;
				}
				Vector2 c = otherBounds.points[otherIndex];
				Vector2 d = otherBounds.points[otherIndex2];
				for (int myIndex = 0; myIndex < intersectionPoints.Length; myIndex++)
				{
					int myIndex2 = myIndex + 1;
					if (myIndex2 == intersectionPoints.Length)
					{
						myIndex2 = 0;
					}
					Vector2 a = intersectionPoints[myIndex];
					Vector2 b = intersectionPoints[myIndex2];
					if (checkForIntersection(a, b, c, d))
					{
                        if (output) Core.Engine.collisionLine = new Vector2(c.X - d.X, c.Y - d.Y);
                        return true;
					}
					b = points[myIndex];
					if (checkForIntersection(a, b, c, d))
					{
                        if (output) Core.Engine.collisionLine = new Vector2(c.X - d.X, c.Y - d.Y);
                        return true;
					}
				}
			}
			if (containsPoint(otherBounds.center.X, otherBounds.center.Y))
			{
				return true;
			}
			Vector2 center = otherBounds.center + movementVector;
			return containsPoint(center.X, center.Y);
		}

		/// <summary>
		/// Determine if a point is contained within these GameObjectBounds.
		/// </summary>
		/// <param name="x">The point's X.</param>
		/// <param name="y">The point's Y.</param>
		/// <returns>True if the point is contained within these GameObjectBounds.</returns>
		private bool containsPoint(float x, float y)
		{
			int hits = 0;
			int length = points.Length;

			float lx = points[length - 1].X;
			float ly = points[length - 1].Y;

			float cx, cy;

			int j = length - 1;
			bool oddNodes = false;

			for (int i = 0; i < length; i++)
			{
				if (points[i].Y < y && points[j].Y >= y
				|| points[j].Y < y && points[i].Y >= y)
				{
					if (points[i].X + (y - points[i].Y) / (points[j].Y - points[i].Y) * (points[j].X - points[i].X) < x)
					{
						oddNodes = !oddNodes;
					}
				}
				j = i;
			}
			return oddNodes;
		}

		/// <summary>
		/// Check for intersection between lines, each defined by two points.
		/// </summary>
		/// <param name="a">The first point of the first line.</param>
		/// <param name="b">The second point of the first line.</param>
		/// <param name="c">The first point of the second line.</param>
		/// <param name="d">The second point of the second line.</param>
		/// <returns>If these lines intersect.</returns>
		private bool checkForIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			float bxax = b.X - a.X;
			float dycy = d.Y - c.Y;
			float byay = b.Y - a.Y;
			float dxcx = d.X - c.X;
			float denom = bxax * dycy - byay * dxcx;

			float aycy = a.Y - c.Y;
			float axcx = a.X - c.X;
			float r = (aycy * dxcx - axcx * dycy) / denom;
			if (0 <= r && r <= 1)
			{
				float s = (aycy * bxax - axcx * byay) / denom;
				if (0 <= s && s <= 1)
				{
					return true;
				}
			}
			return false;
		}
	}
}
