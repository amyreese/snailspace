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

		// Set distance from the camera of the near and far clipping planes.
		static float nearClip = 1.0f;
		static float farClip = 2000.0f;

		VertexPositionTexture[] vertices;

		protected Effect effect;

		private Texture2D texture;

        public GameRenderer()
        {
			cameraPosition = new Vector3(0, 0, -5);
			cameraTargetPosition = new Vector3(0, 0, 0);
			cameraView = Matrix.Identity;
			setUpVertices();
			effect = SnailsPace.getInstance().Content.Load<Effect>("Resources/Effects/effects");
			texture = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/riemerstexture");
        }

        public void render(List<Objects.GameObject> objects, List<Objects.Text> strings)
        {
			SnailsPace.getInstance().GraphicsDevice.Clear(Color.CornflowerBlue);

			Viewport viewport = SnailsPace.getInstance().GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;

			cameraProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);

			effect.CurrentTechnique = effect.Techniques["Textured"];
			effect.Parameters["xView"].SetValue(cameraView);
			effect.Parameters["xProjection"].SetValue(cameraProjection);
			effect.Parameters["xWorld"].SetValue(Matrix.Invert(Matrix.Identity));

			
			effect.Parameters["xTexture"].SetValue(texture);

			float size = 1;
			float rotation = 0;
			Vector3 position = new Vector3(0, 0, 0);
			Matrix worldMatrix = Matrix.CreateScale(size) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(position);

			effect.Begin();
			foreach (EffectPass pass in effect.CurrentTechnique.Passes)
			{
				pass.Begin();
				SnailsPace.getInstance().GraphicsDevice.VertexDeclaration = new VertexDeclaration(SnailsPace.getInstance().GraphicsDevice, VertexPositionTexture.VertexElements);
				SnailsPace.getInstance().GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, vertices, 0, 2);
				pass.End();
			}
			effect.End();
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

    }
}
