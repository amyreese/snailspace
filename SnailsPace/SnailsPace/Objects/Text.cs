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

        public Text()
        {
        }

        public Text(String content, SpriteFont font, Vector2 position, Vector2 scale)
        {
            this.content = content;
            this.font = font;
            this.position = position;
            this.scale = scale;
            this.rotation = 0f;
            this.color = Color.White;
        }
    }
}
