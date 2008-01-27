using System;
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

		private List<QuadTreeNode> nodeList = null;

		public QuadTreeNode(List<Objects.GameObject> containedObjects, Rectangle coordinates, int maxDepth)
		{
			this.coordinates = coordinates;
			this.containedObjects = containedObjects;
			this.maxDepth = maxDepth;
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
			NW = new QuadTreeNode( new List<Objects.GameObject>(), NWRect, maxDepth - 1 );
			nodeList.Add(NW);
			NE = new QuadTreeNode( new List<Objects.GameObject>(), NWRect, maxDepth - 1 );
			nodeList.Add(NE);
			SW = new QuadTreeNode( new List<Objects.GameObject>(), NWRect, maxDepth - 1 );
			nodeList.Add(SW);
			SE = new QuadTreeNode( new List<Objects.GameObject>(), NWRect, maxDepth - 1 );
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
	}
}
