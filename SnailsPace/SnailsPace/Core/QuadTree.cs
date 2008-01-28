using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Core
{
	class QuadTree
	{
		private QuadTreeNode rootNode;

		public QuadTree(List<Objects.GameObject> containedObjects, Rectangle coordinates, int maxDepth)
		{
			rootNode = new QuadTreeNode( containedObjects, coordinates, maxDepth, "Root");
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
