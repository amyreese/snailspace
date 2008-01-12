using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Graphics.TestGraphics
{
    class TransparencyTestSprite : GenericBulletSprite
    {
        public TransparencyTestSprite(ContentManager content, Vector2 size, Vector2 center, float depth)
            : base(content, size, center, depth, content.Load<Texture2D>("Resources/Textures/blendingtexture"))
        {
        }

        protected override void prepareEffect(GraphicsDevice graph, Effect effect)
        {
            graph.RenderState.AlphaBlendEnable = true;
            graph.RenderState.SourceBlend = Blend.One; ;
            graph.RenderState.DestinationBlend = Blend.One;
            base.prepareEffect(graph, effect);
        }

        protected override void cleanupEffect(GraphicsDevice graph, Effect effect)
        {
            graph.RenderState.AlphaBlendEnable = false;
        }
    }
}
