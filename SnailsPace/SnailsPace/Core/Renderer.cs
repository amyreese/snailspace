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

		public Rectangle cameraBounds;

		public Matrix cameraView;
		public Matrix cameraProjection;
		public static float debugZoom = 1.0f;
		public static float normalCameraDistance = 1000.0f;
		public const float minimumCameraMovement = 0.5f;
		public const float cameraSpeed = 2.0f;

		// Set distance from the camera of the near and far clipping planes.
		public static float nearClip = 0.1f;
		public static float farClip = 500.0f + 2 * normalCameraDistance;
		
		VertexPositionTexture[] vertices;
#if DEBUG
		List<VertexPositionColor[]> boundingBoxVertices;
		Color boundingBoxColor = new Color(255, 0, 0, 128);
		Color boundingBoxCenterColor = new Color(0, 0, 0, 64);
#endif

		private Dictionary<String, Texture2D> textures;
		private Dictionary<String, Effect> effects;

		public Renderer()
		{
			cameraPosition = new Vector3(-10, 20, normalCameraDistance);
			cameraTargetOffset = new Vector3(0, 0, normalCameraDistance);
			cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + new Vector3(0, 0, -1), Vector3.Up);
			setUpVertices();

			textures = new Dictionary<string, Texture2D>();
			effects = new Dictionary<string, Effect>();
		}

		public void createTexturesAndEffects(List<Objects.GameObject> objects)
		{
			List<Objects.GameObject>.Enumerator objectEnumerator = objects.GetEnumerator();
			while (objectEnumerator.MoveNext())
			{
				Dictionary<String, Objects.Sprite>.ValueCollection.Enumerator spriteEnumerator = objectEnumerator.Current.sprites.Values.GetEnumerator();
				while (spriteEnumerator.MoveNext())
				{
					getOrCreateTexture(spriteEnumerator.Current);
					getOrCreateEffect(spriteEnumerator.Current);
				}
				spriteEnumerator.Dispose();
			}
			objectEnumerator.Dispose();
		}
		private Texture2D getOrCreateTexture(Objects.Sprite sprite)
		{
			return getOrCreateTexture(sprite.image.filename);
		}
		private Texture2D getOrCreateTexture(String texture)
		{
			if (!textures.ContainsKey(texture))
			{
				Texture2D temp = SnailsPace.getInstance().Content.Load<Texture2D>(texture);
				textures.Add(texture, temp);
#if DEBUG
				if (SnailsPace.debugEffectAndTextureLoading)
				{
					SnailsPace.debug("Texture loaded: " + texture);
				}
#endif
				return temp;
			}
			else
			{
				return textures[texture];
			}
		}
		private Effect getOrCreateEffect(Objects.Sprite sprite)
		{
			return getOrCreateEffect(sprite.effect);
		}
		private Effect getOrCreateEffect(String effect)
		{
			if (!effects.ContainsKey(effect))
			{
				Effect temp = SnailsPace.getInstance().Content.Load<Effect>(effect);
				effects.Add(effect, temp);
#if DEBUG
				if (SnailsPace.debugEffectAndTextureLoading)
				{
					SnailsPace.debug("Effect loaded: " + effect);
				}
#endif
				return temp;
			}
			else
			{
				return effects[effect];
			}
		}

		private float calculateCameraMovement(float distance, float elapsedTime)
		{
			if (float.IsNaN(distance))
			{
				distance = 0;
			}
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
			SnailsPace.getInstance().GraphicsDevice.RenderState.CullMode = CullMode.None;
			SnailsPace.getInstance().GraphicsDevice.RenderState.DepthBufferEnable = true;
			SnailsPace.getInstance().GraphicsDevice.RenderState.DepthBufferWriteEnable = true;
			SnailsPace.getInstance().GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);
			Vector3 cameraTargetPosition = getCameraTargetPosition();
			cameraTargetPosition.Z = cameraTargetPosition.Z * debugZoom;
			if (!cameraPosition.Equals(cameraTargetPosition))
			{
				float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 1);
				Vector3 cameraDifference = cameraTargetPosition - cameraPosition;
				Vector3 cameraPositionMovement = Vector3.Zero;
				cameraPositionMovement.X = calculateCameraMovement(cameraDifference.X, elapsedTime);
				cameraPositionMovement.Y = calculateCameraMovement(cameraDifference.Y, elapsedTime);
				cameraPositionMovement.Z = calculateCameraMovement(cameraDifference.Z, elapsedTime);
				cameraPosition = cameraPosition + cameraPositionMovement;

				//// Keep camera in specified bounds
				//if (cameraPosition.X < cameraBounds.Left)
				//    cameraPosition.X = cameraBounds.Left;
				//else if (cameraPosition.X > cameraBounds.Right)
				//    cameraPosition.X = cameraBounds.Right;

				//if (cameraPosition.Y < cameraBounds.Top)
				//    cameraPosition.Y = cameraBounds.Top;
				//else if (cameraPosition.Y > cameraBounds.Bottom)
				//    cameraPosition.Y = cameraBounds.Bottom;
			}

			Viewport viewport = SnailsPace.getInstance().GraphicsDevice.Viewport;
			float aspectRatio = (float)viewport.Width / (float)viewport.Height;
			cameraProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
			cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + new Vector3(0, 0, -1), Vector3.Up);
			BoundingFrustum viewFrustum = new BoundingFrustum(cameraView * cameraProjection);

#if DEBUG
			boundingBoxVertices = new List<VertexPositionColor[]>();
#endif
			if (objects != null)
			{
				List<Objects.GameObject>.Enumerator objectEnumerator = objects.GetEnumerator();

				SnailsPace.getInstance().GraphicsDevice.VertexDeclaration = new VertexDeclaration(SnailsPace.getInstance().GraphicsDevice, VertexPositionTexture.VertexElements);
				while (objectEnumerator.MoveNext())
				{
					drawObject(objectEnumerator.Current, viewFrustum);
#if DEBUG
					if (SnailsPace.debugBoundingBoxes && objectEnumerator.Current.collidable)
					{
						Objects.GameObjectBounds boundingBox = objectEnumerator.Current.bounds;
						Vector2[] boxVertices = boundingBox.GetPoints();
						VertexPositionColor[] visualBoxVertices = new VertexPositionColor[boxVertices.Length + 2];
						visualBoxVertices[0].Color = boundingBoxCenterColor;
						visualBoxVertices[0].Position = new Vector3(objectEnumerator.Current.position, 1);
						visualBoxVertices[visualBoxVertices.Length - 1].Position = new Vector3(boxVertices[0], 1);
						visualBoxVertices[visualBoxVertices.Length - 1].Color = boundingBoxColor;
						for (int boxVertexIndex = 0; boxVertexIndex < boxVertices.Length; boxVertexIndex++)
						{
							visualBoxVertices[boxVertexIndex + 1].Position = new Vector3(boxVertices[boxVertexIndex], 1);
							visualBoxVertices[boxVertexIndex + 1].Color = boundingBoxColor;
						}
						boundingBoxVertices.Add(visualBoxVertices);
					}
#endif
				}
				objectEnumerator.Dispose();
#if DEBUG
				// TODO this probably isn't how we want to do this if we end up using more than one effect
				Effect effect = getOrCreateEffect("Resources/Effects/effects");
				effect.CurrentTechnique = effect.Techniques["Colored"];
				effect.Parameters["xView"].SetValue(cameraView);
				effect.Parameters["xProjection"].SetValue(cameraProjection);
				effect.Parameters["xWorld"].SetValue(Matrix.Identity);
				effect.Begin();

				SnailsPace.getInstance().GraphicsDevice.VertexDeclaration = new VertexDeclaration(SnailsPace.getInstance().GraphicsDevice, VertexPositionColor.VertexElements);
				List<VertexPositionColor[]>.Enumerator boundingBoxEnumerator = boundingBoxVertices.GetEnumerator();
				while (boundingBoxEnumerator.MoveNext())
				{
					IEnumerator<EffectPass> effectPassEnumerator = effect.CurrentTechnique.Passes.GetEnumerator();
					while (effectPassEnumerator.MoveNext())
					{
						effectPassEnumerator.Current.Begin();
						// The vertex declaration needs to be set before it gets here or it will fail.
						SnailsPace.getInstance().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleFan, boundingBoxEnumerator.Current, 0, boundingBoxEnumerator.Current.Length - 2);
						effectPassEnumerator.Current.End();
					}
					effectPassEnumerator.Dispose();
				}
				boundingBoxEnumerator.Dispose();
				effect.End();
#endif
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
				textEnumerator.Dispose();
			}
		}

		private void drawObject(Objects.GameObject obj, BoundingFrustum viewFrustum)
		{
			Dictionary<String, Objects.Sprite>.ValueCollection.Enumerator spriteEnumerator = obj.sprites.Values.GetEnumerator();

			while (spriteEnumerator.MoveNext())
			{
				if (spriteEnumerator.Current.visible)
				{
					Vector3 objectPosition = new Vector3(obj.position + spriteEnumerator.Current.position, -obj.layer - spriteEnumerator.Current.layerOffset);
					Vector3 objectScale = new Vector3(spriteEnumerator.Current.image.size, 1);

					BoundingSphere sphere = new BoundingSphere(objectPosition, objectScale.X * objectScale.Y);
					if (viewFrustum.Intersects(sphere))
					{

						int xBlock = (int)(spriteEnumerator.Current.frame % spriteEnumerator.Current.image.blocks.X);
						int yBlock = (int)((spriteEnumerator.Current.frame - xBlock) / spriteEnumerator.Current.image.blocks.X);

						Matrix translationMatrix = Matrix.CreateScale(objectScale) * Matrix.CreateRotationZ(obj.rotation + spriteEnumerator.Current.rotation) *
							Matrix.CreateTranslation(objectPosition);
						VertexPositionTexture[] objVertices = new VertexPositionTexture[vertices.Length];
						int xFlip = 0;
						if (spriteEnumerator.Current.horizontalFlip && !obj.horizontalFlip || obj.horizontalFlip && !spriteEnumerator.Current.horizontalFlip)
						{
							xFlip = -1;
						}
						int yFlip = 0;
						for (int index = 0; index < vertices.Length; index++)
						{
							objVertices[index].Position.X = translationMatrix.M11 * vertices[index].Position.X
															+ translationMatrix.M21 * vertices[index].Position.Y
															+ translationMatrix.M41;
							objVertices[index].Position.Y = translationMatrix.M12 * vertices[index].Position.X
															+ translationMatrix.M22 * vertices[index].Position.Y
															+ translationMatrix.M42;
							objVertices[index].Position.Z = objectPosition.Z;

							int xMod = 1 - index % 2;
							int yMod = 0;
							if (index == 0 || index == 1)
							{
								yMod = 1;
							}
							objVertices[index].TextureCoordinate.X = (xBlock + xMod) / spriteEnumerator.Current.image.blocks.X + xFlip;
							objVertices[index].TextureCoordinate.Y = (yBlock + yMod) / spriteEnumerator.Current.image.blocks.Y + yFlip;
						}

						// TODO this probably isn't how we want to do this if we end up using more than one effect
						Effect effect = getOrCreateEffect(spriteEnumerator.Current);
						effect.CurrentTechnique = effect.Techniques["Textured"];
						effect.Parameters["xView"].SetValue(cameraView);
						effect.Parameters["xProjection"].SetValue(cameraProjection);
						effect.Parameters["xWorld"].SetValue(Matrix.Identity);
						effect.Parameters["xTexture"].SetValue(getOrCreateTexture(spriteEnumerator.Current));

						effect.Begin();
						IEnumerator<EffectPass> effectPassEnumerator = effect.CurrentTechnique.Passes.GetEnumerator();
						while (effectPassEnumerator.MoveNext())
						{
							effectPassEnumerator.Current.Begin();
							// The vertex declaration needs to be set before it gets here, or this will fail
							SnailsPace.getInstance().GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, objVertices, 0, 2);
							effectPassEnumerator.Current.End();
						}
						effectPassEnumerator.Dispose();
						effect.End();
					}
					else
					{
#if DEBUG
						if (SnailsPace.debugCulling)
						{
							SnailsPace.debug("Object culled.");
						}
#endif
					}
				}
			}
			spriteEnumerator.Dispose();
		}

		private void setUpVertices()
		{
			vertices = new VertexPositionTexture[4];
			vertices[3].Position = new Vector3(-0.5f, 0.5f, 0.0f);
			vertices[3].TextureCoordinate.X = 0;
			vertices[3].TextureCoordinate.Y = 0;

			vertices[2].Position = new Vector3(0.5f, 0.5f, 0f);
			vertices[2].TextureCoordinate.X = 1;
			vertices[2].TextureCoordinate.Y = 0;

			vertices[1].Position = new Vector3(-0.5f, -0.5f, 0.0f);
			vertices[1].TextureCoordinate.X = 0;
			vertices[1].TextureCoordinate.Y = 1;

			vertices[0].Position = new Vector3(0.5f, -0.5f, 0.0f);
			vertices[0].TextureCoordinate.X = 1;
			vertices[0].TextureCoordinate.Y = 1;
		}

	}
}
