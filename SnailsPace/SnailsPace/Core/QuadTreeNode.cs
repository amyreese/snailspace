using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Core
{
	class QuadTreeNode
	{
		private QuadTreeNode NW = null;
		private QuadTreeNode NE = null;
		private QuadTreeNode SW = null;
		private QuadTreeNode SE = null;
		
		private Rectangle NWRect;
		private Rectangle NERect;
		private Rectangle SWRect;
		private Rectangle SERect;

		protected List<Objects.GameObject> containedObjects;
		private Rectangle coordinates;
		private int maxDepth;
		public String name;

		private List<QuadTreeNode> nodeList = null;

		public QuadTreeNode(List<Objects.GameObject> containedObjects, Rectangle coordinates, int maxDepth, String name)
		{
			this.coordinates = coordinates;
			this.containedObjects = containedObjects;
			this.maxDepth = maxDepth;
			this.name = name;

			nodeList = new List<QuadTreeNode>();
		}

		public void fillChildren()
		{
			if (maxDepth > 1)
			{
				createCornerRectangles();
				createChildNodes();
				List<Objects.GameObject>.Enumerator objectEnumerator = containedObjects.GetEnumerator();

				while (objectEnumerator.MoveNext())
				{
					Objects.GameObject current = objectEnumerator.Current;

					if (NWRect.Contains(current.getRectangle()) || NWRect.Intersects(current.getRectangle()))
					{
						//Debug.WriteLine("Object " + current.name + " added to NW rectangle.");
						//Debug.WriteLine(current.name + " Dimensions: TL( " + current.getRectangle().Left + ", " + current.getRectangle().Top + " ), BR( " + current.getRectangle().Right + ", " + current.getRectangle().Bottom + " )");
						NW.containedObjects.Add(current);
					}
					if (NERect.Contains(current.getRectangle()) || NERect.Intersects(current.getRectangle()))
					{
						//Debug.WriteLine("Object " + current.name + " added to NE rectangle.");
						//Debug.WriteLine(current.name + " Dimensions: TL( " + current.getRectangle().Left + ", " + current.getRectangle().Top + " ), BR( " + current.getRectangle().Right + ", " + current.getRectangle().Bottom + " )");
						NE.containedObjects.Add(current);
					}
					if (SWRect.Contains(current.getRectangle()) || SWRect.Intersects(current.getRectangle()))
					{
						//Debug.WriteLine("Object " + current.name + " added to SW rectangle.");
						//Debug.WriteLine(current.name + " Dimensions: TL( " + current.getRectangle().Left + ", " + current.getRectangle().Top + " ), BR( " + current.getRectangle().Right + ", " + current.getRectangle().Bottom + " )");
						SW.containedObjects.Add(current);
					}
					if (SERect.Contains(current.getRectangle()) || SERect.Intersects(current.getRectangle()))
					{
						//Debug.WriteLine("Object " + current.name + " added to SE rectangle.");
						//Debug.WriteLine(current.name + " Dimensions: TL( " + current.getRectangle().Left + ", " + current.getRectangle().Top + " ), BR( " + current.getRectangle().Right + ", " + current.getRectangle().Bottom + " )");
						SE.containedObjects.Add(current);
					}

				}
				NW.fillChildren();
				NE.fillChildren();
				SW.fillChildren();
				SE.fillChildren();
			}
			
		}
		public void createChildNodes()
		{
			NW = new QuadTreeNode( new List<Objects.GameObject>(), NWRect, maxDepth - 1, "NW Child of " + name + "."  );
			nodeList.Add(NW);
			NE = new QuadTreeNode(new List<Objects.GameObject>(), NERect, maxDepth - 1, "NE Child of " + name + "." );
			nodeList.Add(NE);
			SW = new QuadTreeNode(new List<Objects.GameObject>(), SWRect, maxDepth - 1, "SW Child of " + name + "." );
			nodeList.Add(SW);
			SE = new QuadTreeNode(new List<Objects.GameObject>(), SERect, maxDepth - 1, "SE Child of " + name + "." );
			nodeList.Add(SE);
		}

		private void createCornerRectangles()
		{
			NWRect = new Rectangle(coordinates.X, coordinates.Y, coordinates.Width / 2, coordinates.Height / 2);
			NERect = new Rectangle(coordinates.X + coordinates.Width / 2, coordinates.Y, coordinates.Width / 2, coordinates.Height / 2);
			SWRect = new Rectangle(coordinates.X, coordinates.Y + coordinates.Height / 2, coordinates.Width / 2, coordinates.Height / 2);
			SERect = new Rectangle(coordinates.X + coordinates.Width / 2, coordinates.Y + coordinates.Height / 2, coordinates.Width / 2, coordinates.Height / 2);
		}

		public List<QuadTreeNode> getNodes()
		{
			return nodeList;
		}

		public List<Objects.GameObject> getContainedObjects()
		{
			return containedObjects;
		}

        [Conditional("DEBUG")]
		public void print()
		{
			Debug.WriteLine(name + ". Contains " + containedObjects.Count + " objects.");
			List<Objects.GameObject>.Enumerator objectEnumerator = containedObjects.GetEnumerator();
			if (containedObjects.Count < 25)
			{
				while (objectEnumerator.MoveNext())
				{
					Debug.WriteLine(objectEnumerator.Current.name);
				}
			}
			Debug.WriteLine("Dimensions: TL( " + coordinates.Left + ", " + coordinates.Top + " ), BR( " + coordinates.Right + ", " + coordinates.Bottom + " )" );
			if (NW != null)
			{
				NW.print();
			}
			if (NE != null)
			{
				NE.print();
			}
			if (SW != null)
			{
				SW.print();
			}
			if (SE != null)
			{
				SE.print();
			}
		}
	}
}
