using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnailsPace.Objects
{
	/// <summary>
	/// Store information about an image, such as size, animation block size, and filename.
	/// </summary>
    class Image
    {
        // Image filename.
        public String filename;

        // Image pixel size and blocks.
        public Vector2 size;
        public Vector2 blocks;

		/// <summary>
		/// Performs a deep clone of the image
		/// </summary>
		public Image clone()
		{
			Image clonedImage = new Image();
			clonedImage.filename = filename;
			clonedImage.size = new Vector2( size.X, size.Y );
			clonedImage.blocks = new Vector2( blocks.X, blocks.Y );
			return clonedImage;
		}
    }
}
