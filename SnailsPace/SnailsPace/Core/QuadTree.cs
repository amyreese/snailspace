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
			rootNode = new QuadTreeNode(containedObjects, coordinates, maxDepth - 1);
		}

		public QuadTreeNode getRoot()
		{
			return rootNode;
		}
	}
}
