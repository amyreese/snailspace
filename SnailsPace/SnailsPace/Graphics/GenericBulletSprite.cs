using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Graphics
{
    class GenericBulletSprite : GenericSprite
    {
        protected GenericBulletSprite(ContentManager content, Vector2 size, Vector2 center, float depth, Texture2D texture)
            : base(content, size, center, BulletsZ, depth, texture)
        {
        }
    }
}
