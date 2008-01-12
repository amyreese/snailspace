using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SnailsPace
{
    class GameRenderer
    {
        // Camera information
        private Vector3 cameraPosition;
        private Vector3 cameraTargetPosition;
        private Matrix cameraView;
		private Matrix cameraProjection;
		public const float normalCameraDistance = 5.0f;
		public const float minimumCameraMovement = 0.001f;

		// Set distance from the camera of the near and far clipping planes.
		static float nearClip = 0.1f;
		static float farClip = 2000.0f;

		VertexPositionTexture[] vertices;

		private Texture2D texture;

        public GameRenderer()
        {
			cameraPosition = new Vector3(-100, 0, normalCameraDistance);
			cameraTargetPosition = new Vector3(0, 0, normalCameraDistance);
			cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + new Vector3(0,0,-1), Vector3.Up);
			setUpVertices();
			texture = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/riemerstexture");
        }

        public void render(List<Objects.GameObject> objects, List<Objects.Text> strings, GameTime gameTime)
        {
			SnailsPace.getInstance().GraphicsDevice.Clear(Color.CornflowerBlue);
			if (!cameraPosition.Equals(cameraTargetPosition))
			{
				Vector3 cameraPositionMovement = ( ( cameraTargetPosition - cameraPosition ) / 2.0f ) * ( Math.Min((float)gameTime.ElapsedRealTime.TotalSeconds, 1) * 2.0f );
				cameraPosition = cameraPosition + cameraPositionMovement;
				if (cameraPositionMovement.X < minimumCameraMovement)
				{
					cameraPosition.X = cameraTargetPosition.X;
				}
				if (cameraPositionMovement.Y < minimumCameraMovement)
				{
					cameraPosition.Y = cameraTargetPosition.Y;
				}
				if (cameraPositionMovement.Z < minimumCameraMovement)
				{
					cameraPosition.Z = cameraTargetPosition.Z;
				}
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
							Vector3 objectPosition = new Vector3(objectEnumerator.Current.position, -objectEnumerator.Current.layer);
							Vector3 objectScale = new Vector3(1, 1, 1);

							Matrix worldMatrix = Matrix.CreateScale(objectScale) * Matrix.CreateRotationZ(objectEnumerator.Current.rotation) *
								Matrix.CreateTranslation(objectPosition);
							spriteEnumerator.Current.effect.CurrentTechnique = spriteEnumerator.Current.effect.Techniques["Textured"];
							spriteEnumerator.Current.effect.Parameters["xView"].SetValue(cameraView);
							spriteEnumerator.Current.effect.Parameters["xProjection"].SetValue(cameraProjection);
							spriteEnumerator.Current.effect.Parameters["xWorld"].SetValue(worldMatrix);

							// TODO: pull the appropraite texture
							spriteEnumerator.Current.effect.Parameters["xTexture"].SetValue(texture);

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
