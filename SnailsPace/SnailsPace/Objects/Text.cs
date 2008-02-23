using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Objects
{
	/// <summary>
	/// A class to encapsulate information about text.
	/// </summary>
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

		/// <summary>
		/// Default constructor.
		/// </summary>
        public Text()
        {
        }

		/// <summary>
		/// Construct a Text object with the following properties.
		/// </summary>
		/// <param name="content">The text.</param>
		/// <param name="font">The desired font.</param>
		/// <param name="position">The text's position.</param>
		/// <param name="scale">The text's size.</param>
		/// <param name="color">The text's color.</param>
        public Text(String content, SpriteFont font, Vector2 position, Vector2 scale, Color color)
        {
            this.content = content;
            this.font = font;
            this.position = position;
            this.scale = scale;
            this.rotation = 0f;
            this.color = color;
        }

		/// <summary>
		/// Construct a Text object with the following properties.
		/// </summary>
		/// <param name="content">The text.</param>
		/// <param name="font">The desired font.</param>
		/// <param name="position">The text's position.</param>
		/// <param name="scale">The text's size.</param>
        public Text(String content, SpriteFont font, Vector2 position, Vector2 scale) : this(content, font, position, scale, Color.White )
        {
        }
    }
}
