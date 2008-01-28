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
			if ( maxDepth > 1 )
			{
				createCornerRectangles();
				createChildNodes();
				List<Objects.GameObject>.Enumerator objectEnumerator = containedObjects.GetEnumerator();

				while ( objectEnumerator.MoveNext() ) 
				{
					Objects.GameObject current = objectEnumerator.Current;
					
					if ( NWRect.Intersects( current.getRectangle() ) )
					{
						NW.containedObjects.Add( current );
					}
					if (NERect.Intersects( current.getRectangle() ))
					{
						NE.containedObjects.Add(current);
					}
					if (SWRect.Intersects(current.getRectangle() ))
					{
						SW.containedObjects.Add(current);
					}
					if (SERect.Intersects(current.getRectangle() ))
					{
						SE.containedObjects.Add(current);
					}
				}
			}
		}

		public void createChildNodes()
		{
			NW = new QuadTreeNode( new List<Objects.GameObject>(), NWRect, maxDepth - 1, "NW Child of " + name + "."  );
			nodeList.Add(NW);
			NE = new QuadTreeNode(new List<Objects.GameObject>(), NWRect, maxDepth - 1, "NE Child of " + name + "." );
			nodeList.Add(NE);
			SW = new QuadTreeNode(new List<Objects.GameObject>(), NWRect, maxDepth - 1, "SW Child of " + name + "." );
			nodeList.Add(SW);
			SE = new QuadTreeNode(new List<Objects.GameObject>(), NWRect, maxDepth - 1, "SE Child of " + name + "." );
			nodeList.Add(SE);
		}

		private void createCornerRectangles()
		{
			SWRect = new Rectangle(coordinates.X, coordinates.Y, coordinates.Width / 2, coordinates.Height / 2);
			SERect = new Rectangle(coordinates.X + coordinates.Width / 2, coordinates.Y, coordinates.Width / 2, coordinates.Height / 2);
			NWRect = new Rectangle(coordinates.X, coordinates.Y + coordinates.Height / 2, coordinates.Width / 2, coordinates.Height / 2);
			NERect = new Rectangle(coordinates.X + coordinates.Width / 2, coordinates.Y + coordinates.Height / 2, coordinates.Width / 2, coordinates.Height / 2);
		}

		public List<QuadTreeNode> getNodes()
		{
			return nodeList;
		}

		public List<Objects.GameObject> getContainedObjects()
		{
			return containedObjects;
		}
#if DEBUG
		public void print()
		{
			Debug.WriteLine(name + ". Contains " + containedObjects.Count + " objects.");
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
#endif
	}
}
