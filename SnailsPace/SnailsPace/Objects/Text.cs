using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Objects
{
    class Text
    {
        // Text position and such
        public Vector2 position;
        public float rotation;
		public Vector2 scale;

        // Text properties
        public String content;
        public SpriteFont font;
        public Color color;
    }
}
