using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace SnailsPace.Graphics.TestGraphics
{
    class TextureTestSprite : GenericBackgroundSprite
    {
        public TextureTestSprite(ContentManager content, Vector2 size, Vector2 center, float depth)
            : base(content, size, center, depth, content.Load<Texture2D>("Resources/Textures/riemerstexture"))
        {
        }

    }
}
