using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace A_Snail_s_Pace.Graphics
{
    public abstract class GenericSprite
    {
        /// <summary>
        /// ZBuffer values
        /// </summary>
        protected const float ZBUFF_DIFF = 0.1f;
        protected const float ZBUFF_MINOR_DIFF = 0.01f;
        protected const float BackgroundZ = 0.0f;
        protected const float PlatformZ = BackgroundZ + ZBUFF_DIFF;
        protected const float PlayerZ = PlatformZ + ZBUFF_DIFF;
        protected const float EnemiesZ = PlayerZ + ZBUFF_DIFF;
        protected const float BulletsZ = EnemiesZ + ZBUFF_DIFF;

        protected Vector3 size;
        protected Vector2 center;
        private Vector3 position;
        private float zIndex;
        private float rotation;
        protected Texture2D texture;
        VertexPositionTexture[] vertices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="center"></param>
        /// <param name="zIndex"></param>
        public GenericSprite(ContentManager content, Vector2 size, Vector2 center, float zIndexBase, float depth, Texture2D texture)
        {
            if (zIndexBase % ZBUFF_DIFF != 0)
            {
                zIndexBase = zIndexBase - zIndexBase % ZBUFF_DIFF;
                SnailsPace.debug("Attempt to create a GenericSprite with a bad zIndexBase: " + this.GetType());
            }
            if (depth >= ZBUFF_DIFF)
            {
                depth = zIndexBase + ZBUFF_DIFF - ZBUFF_MINOR_DIFF;
                SnailsPace.debug("Attempt to create a GenericSprite with a bad depth: " + this.GetType());
            }
            this.size = new Vector3(size, 1);
            this.center = center;
            this.zIndex = zIndexBase + depth;
            this.texture = texture;
            rotation = 0;
            recalculatePosition();
            loadEffects(content);
            setUpVertices();
        }

        /// <summary>
        /// Recalculate the actual position of the sprite, for drawing purposes
        /// </summary>
        private void recalculatePosition()
        {
            this.position = new Vector3(center.X - size.X / 2, center.Y - size.Y / 2, zIndex);
        }

        private void setUpVertices()
        {
            vertices = new VertexPositionTexture[4];
            vertices[0].Position = new Vector3(-1.0f, 1.0f, 0.0f);
            vertices[0].TextureCoordinate.X = 0;
            vertices[0].TextureCoordinate.Y = 0;

            vertices[1].Position = new Vector3(1.0f, 1.0f, 0f);
            vertices[1].TextureCoordinate.X = 1;
            vertices[1].TextureCoordinate.Y = 0;

            vertices[2].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            vertices[2].TextureCoordinate.X = 0;
            vertices[2].TextureCoordinate.Y = 1;

            vertices[3].Position = new Vector3(1.0f, -1.0f, 0.0f);
            vertices[3].TextureCoordinate.X = 1;
            vertices[3].TextureCoordinate.Y = 1;
        }

        public void moveLeft(float amount)
        {
            move(new Vector2(-amount, 0));
        }

        public void moveRight(float amount)
        {
            move(new Vector2(amount, 0));
        }

        public void moveUp(float amount)
        {
            move(new Vector2(0, amount));
        }

        public void moveDown(float amount)
        {
            move(new Vector2(0, -amount));
        }

        protected void move(Vector2 adjustment)
        {
            moveTo(adjustment + center);
        }

        protected void moveTo(Vector2 newPosition)
        {
            this.center = newPosition;
            recalculatePosition();
        }

        public void rotateClockwise(float radians)
        {
            rotate(-radians);
        }
        public void rotateCounterClockwise(float radians)
        {
            rotate(radians);
        }
        protected void rotate(float radians)
        {
            rotation += radians;
        }

        protected Effect effect;
        protected void loadEffects(ContentManager content)
        {
            effect = content.Load<Effect>("Effects/effects");
            effect.Parameters["xView"].SetValue(SnailsPace.viewMatrix);
            effect.Parameters["xProjection"].SetValue(SnailsPace.projectionMatrix);
        }

        protected virtual void prepareEffect(GraphicsDevice graph, Effect effect)
        {
            effect.CurrentTechnique = effect.Techniques["Textured"];
            Matrix worldMatrix = Matrix.CreateScale(size) * Matrix.CreateRotationZ(rotation) *
                Matrix.CreateTranslation(position);
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            effect.Parameters["xTexture"].SetValue(texture);
        }

        protected virtual void cleanupEffect(GraphicsDevice graph, Effect effect)
        {
        }

        public void draw(GraphicsDevice graph)
        {
            prepareEffect(graph, effect);
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                graph.VertexDeclaration = new VertexDeclaration(graph, VertexPositionTexture.VertexElements);
                graph.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, vertices, 0, 2);
                pass.End();
            }
            effect.End();
            cleanupEffect( graph, effect );
        }
    }
}
