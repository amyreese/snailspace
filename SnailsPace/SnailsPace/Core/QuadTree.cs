using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Core
{
	class QuadTree
	{
		private QuadTreeNode rootNode;

		public QuadTree(List<Objects.GameObject> containedObjects, Vector2 boundsSize, Vector2 boundsCenter, int maxDepth)
		{
			float left = boundsCenter.X - boundsSize.X / 2;
			float top = boundsCenter.Y - boundsSize.Y / 2;
			Rectangle coordinates = new Rectangle((int)left, (int)top, (int)boundsSize.X, (int)boundsSize.Y);
			rootNode = new QuadTreeNode( containedObjects, coordinates, maxDepth, "Root");
			rootNode.fillChildren();
		}

		public QuadTreeNode getRoot()
		{
			return rootNode;
		}

#if DEBUG
		public void print()
		{
			rootNode.print();
		}
#endif
	
	
	}
}
