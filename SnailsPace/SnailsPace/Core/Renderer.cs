using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace.Core
{
    class Renderer
    {
        // Camera information
        public Vector3 cameraPosition;

		public Objects.GameObject cameraTarget;
		public Vector3 cameraTargetOffset;

        public Matrix cameraView;
        public Matrix cameraProjection;
        public const float normalCameraDistance = 25.0f;
        public const float minimumCameraMovement = 0.5f;
        public const float cameraSpeed = 1.5f;
        public const float textureScale = 64.0f;

        // Set distance from the camera of the near and far clipping planes.
        static float nearClip = 0.1f;
        static float farClip = 2000.0f;

        VertexPositionTexture[] vertices;

        private Dictionary<String, Texture2D> texture;

        public Renderer()
        {
            cameraPosition = new Vector3(-10, 20, normalCameraDistance);
            cameraTargetOffset = new Vector3(0, 0, normalCameraDistance);
            cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + new Vector3(0, 0, -1), Vector3.Up);
            setUpVertices();

            //TODO: Iterate through game objects, take filename from sprite image, create texture 2D for it, put into dictionary, maps image filename to texture2D

        }

        public void createTextures(List<Objects.GameObject> objects)
        {
            texture = new Dictionary<string, Texture2D>();
            List<Objects.GameObject>.Enumerator objectEnumerator = objects.GetEnumerator();
            while (objectEnumerator.MoveNext())
            {
                Dictionary<String, Objects.Sprite>.ValueCollection.Enumerator spriteEnumerator = objectEnumerator.Current.sprites.Values.GetEnumerator();
                while (spriteEnumerator.MoveNext())
                {
                    if (!texture.ContainsKey(spriteEnumerator.Current.image.filename))
                    {
                        Texture2D temp = SnailsPace.getInstance().Content.Load<Texture2D>(spriteEnumerator.Current.image.filename) as Texture2D;
                        texture.Add(spriteEnumerator.Current.image.filename, temp);
                    }
                }
            }
        }

        private float calculateCameraMovement(float distance, float elapsedTime)
        {
            float absDistance = Math.Abs(distance);
            float minMovement = elapsedTime * minimumCameraMovement;
            if (absDistance < minMovement)
            {
                return distance;
            }
            else
            {
                return (Math.Sign(distance) * Math.Max((absDistance * cameraSpeed) * elapsedTime, minMovement));
            }
        }

		public Vector3 getCameraTargetPosition()
		{
			Vector3 targetPosition = cameraTarget == null ? Vector3.Zero : new Vector3(cameraTarget.position, 0);
			return new Vector3(cameraTarget.position, 0) + cameraTargetOffset;
		}

        public void render(List<Objects.GameObject> objects, List<Objects.Text> strings, GameTime gameTime)
        {
            SnailsPace.getInstance().GraphicsDevice.RenderState.DepthBufferEnable = true;
            SnailsPace.getInstance().GraphicsDevice.RenderState.DepthBufferWriteEnable = true;
            SnailsPace.getInstance().GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);
			Vector3 cameraTargetPosition = getCameraTargetPosition();
            if (!cameraPosition.Equals(cameraTargetPosition))
            {
                float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 1);
                Vector3 cameraDifference = cameraTargetPosition - cameraPosition;
                Vector3 cameraPositionMovement = Vector3.Zero;
                cameraPositionMovement.X = calculateCameraMovement(cameraDifference.X, elapsedTime);
                cameraPositionMovement.Y = calculateCameraMovement(cameraDifference.Y, elapsedTime);
                cameraPositionMovement.Z = calculateCameraMovement(cameraDifference.Z, elapsedTime); ;
                cameraPosition = cameraPosition + cameraPositionMovement;
            }
            Viewport viewport = SnailsPace.getInstance().GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;
            cameraProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
            cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + new Vector3(0, 0, -1), Vector3.Up);

            if (objects != null)
            {
                List<Objects.GameObject>.Enumerator objectEnumerator = objects.GetEnumerator();
                while (objectEnumerator.MoveNext())
                {
                    Dictionary<String, Objects.Sprite>.ValueCollection.Enumerator spriteEnumerator = objectEnumerator.Current.sprites.Values.GetEnumerator();
                    while (spriteEnumerator.MoveNext())
                    {
                        if (spriteEnumerator.Current.visible)
                        {
                            Vector3 objectPosition = new Vector3(objectEnumerator.Current.position, 0);
                            Vector3 objectScale = new Vector3(spriteEnumerator.Current.image.size, 1);
                            objectScale = new Vector3(spriteEnumerator.Current.image.size / textureScale, 1);
                            int xBlock = (int)(spriteEnumerator.Current.frame % spriteEnumerator.Current.image.blocks.X);
                            int yBlock = (int)((spriteEnumerator.Current.frame - xBlock) / spriteEnumerator.Current.image.blocks.X);
                            vertices[0].TextureCoordinate.X = (xBlock + 1) / spriteEnumerator.Current.image.blocks.X;
                            vertices[0].TextureCoordinate.Y = (yBlock + 1) / spriteEnumerator.Current.image.blocks.Y;
                            vertices[1].TextureCoordinate.X = xBlock / spriteEnumerator.Current.image.blocks.X;
                            vertices[1].TextureCoordinate.Y = (yBlock + 1) / spriteEnumerator.Current.image.blocks.Y;
                            vertices[2].TextureCoordinate.X = (xBlock + 1) / spriteEnumerator.Current.image.blocks.X;
                            vertices[2].TextureCoordinate.Y = yBlock / spriteEnumerator.Current.image.blocks.Y;
                            vertices[3].TextureCoordinate.X = xBlock / spriteEnumerator.Current.image.blocks.X;
                            vertices[3].TextureCoordinate.Y = yBlock / spriteEnumerator.Current.image.blocks.Y;
                            vertices[0].Position.Z = -objectEnumerator.Current.layer;
                            vertices[1].Position.Z = -objectEnumerator.Current.layer;
                            vertices[2].Position.Z = -objectEnumerator.Current.layer;
                            vertices[3].Position.Z = -objectEnumerator.Current.layer;

                            Matrix worldMatrix = Matrix.CreateScale(objectScale) * Matrix.CreateRotationZ(objectEnumerator.Current.rotation) *
                                Matrix.CreateTranslation(objectPosition);
                            spriteEnumerator.Current.effect.CurrentTechnique = spriteEnumerator.Current.effect.Techniques["Textured"];
                            spriteEnumerator.Current.effect.Parameters["xView"].SetValue(cameraView);
                            spriteEnumerator.Current.effect.Parameters["xProjection"].SetValue(cameraProjection);
                            spriteEnumerator.Current.effect.Parameters["xWorld"].SetValue(worldMatrix);

                            // TODO: pull the appropraite part of the texture
                            spriteEnumerator.Current.effect.Parameters["xTexture"].SetValue(texture[spriteEnumerator.Current.image.filename]);

                            spriteEnumerator.Current.effect.Begin();
                            foreach (EffectPass pass in spriteEnumerator.Current.effect.CurrentTechnique.Passes)
                            {
                                pass.Begin();
                                SnailsPace.getInstance().GraphicsDevice.VertexDeclaration = new VertexDeclaration(SnailsPace.getInstance().GraphicsDevice, VertexPositionTexture.VertexElements);
                                SnailsPace.getInstance().GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, vertices, 0, 2);
                                pass.End();
                            }
                            spriteEnumerator.Current.effect.End();
                        }
                    }
                }
            }

            if (strings != null)
            {
                SpriteBatch batch = new SpriteBatch(SnailsPace.getInstance().GraphicsDevice);
                List<Objects.Text>.Enumerator textEnumerator = strings.GetEnumerator();
                batch.Begin();
                while (textEnumerator.MoveNext())
                {
                    batch.DrawString(textEnumerator.Current.font, textEnumerator.Current.content, textEnumerator.Current.position, textEnumerator.Current.color, textEnumerator.Current.rotation, Vector2.Zero, textEnumerator.Current.scale, SpriteEffects.None, 0);
                }
                batch.End();
            }
        }

        private void setUpVertices()
        {
            vertices = new VertexPositionTexture[4];
            vertices[3].Position = new Vector3(-1.0f, 1.0f, 0.0f);
            vertices[3].TextureCoordinate.X = 0;
            vertices[3].TextureCoordinate.Y = 0;

            vertices[2].Position = new Vector3(1.0f, 1.0f, 0f);
            vertices[2].TextureCoordinate.X = 1;
            vertices[2].TextureCoordinate.Y = 0;

            vertices[1].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            vertices[1].TextureCoordinate.X = 0;
            vertices[1].TextureCoordinate.Y = 1;

            vertices[0].Position = new Vector3(1.0f, -1.0f, 0.0f);
            vertices[0].TextureCoordinate.X = 1;
            vertices[0].TextureCoordinate.Y = 1;
        }

    }
}
