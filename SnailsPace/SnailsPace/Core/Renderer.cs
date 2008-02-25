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
		public static Vector3 cameraPosition;

		public static Objects.GameObject cameraTarget;
		public static Vector3 cameraTargetOffset;

		public Vector2[] cameraBounds;

		public Matrix cameraView;
		public Matrix cameraProjection;
		public static float debugZoom = 1.0f;
		public static float normalCameraDistance = 1000.0f;
		public const float minimumCameraMovement = 0.5f;
		public const float cameraSpeed = 4.0f;

		// Set distance from the camera of the near and far clipping planes.
		public static float nearClip = 0.1f;
		public static float farClip = 500.0f + 2 * normalCameraDistance;
		
		VertexPositionColorTexture[] vertices;
#if DEBUG
		List<VertexPositionColorTexture[]> boundingBoxVertices;
		Color boundingBoxColor = new Color(255, 0, 0, 128);
		Color boundingBoxCenterColor = new Color(0, 0, 0, 64);
		Color triggerBoxColor = new Color(255, 255, 255, 200);
		Color triggerBoxCenterColor = new Color(255, 255, 255, 50);
#endif

		private Dictionary<String, Texture2D> textures;
		private Dictionary<String, Effect> effects;

		/// <summary>
		/// Create the renderer, initialize the camera.
		/// </summary>
		public Renderer()
		{
			cameraPosition = new Vector3(-10, 20, normalCameraDistance);
			cameraTargetOffset = new Vector3(0, 0, normalCameraDistance);
			cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + new Vector3(0, 0, -1), Vector3.Up);
			setUpVertices();

			textures = new Dictionary<string, Texture2D>();
			effects = new Dictionary<string, Effect>();
		}

		/// <summary>
		/// Iterate over a collection and load the textures and effects used within.
		/// </summary>
		/// <param name="objects">A list of all GameObjects.</param>
		public void createTexturesAndEffects(List<Objects.GameObject> objects)
		{
			// Iterate through list of objects
			List<Objects.GameObject>.Enumerator objectEnumerator = objects.GetEnumerator();
			while (objectEnumerator.MoveNext())
			{
				// Iterate through sprites used by this object
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

		/// <summary>
		/// If a texture has already been loaded, return it. If a texture has not already been loaded,
		/// load it and add it to the textures collection.
		/// </summary>
		/// <param name="sprite">The Sprite object.</param>
		/// <returns>A Texture2D object that corresponds to the Sprite object.</returns>
		private Texture2D getOrCreateTexture(Objects.Sprite sprite)
		{
			return getOrCreateTexture(sprite.image.filename);
		}

		/// <summary>
		/// If a texture has already been loaded, return it. If a texture has not already been loaded,
		/// load it and add it to the textures collection.
		/// </summary>
		/// <param name="texture">The name of the texture.</param>
		/// <returns>A Texture2D object that corresponds to the texture name.</returns>
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

		/// <summary>
		/// If an effect has already been loaded, return it. If an effect has not already been loaded,
		/// load it and add it to the effects collection.
		/// </summary>
		/// <param name="sprite">The Sprite object.</param>
		/// <returns>An Effect object that corresponds to the Sprite object.</returns>
		private Effect getOrCreateEffect(Objects.Sprite sprite)
		{
			return getOrCreateEffect(sprite.effect);
		}

		/// <summary>
		/// If an effect has already been loaded, return it. If an effect has not already been loaded,
		/// load it and add it to the effects collection.
		/// </summary>
		/// <param name="texture">The name of the effect.</param>
		/// <returns>A Effect object that corresponds to the effect name.</returns>
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

		/// <summary>
		/// Calculate how far the camera must move given an amount of elapsed time.
		/// </summary>
		/// <param name="distance">A single component of distance (X, Y, or Z).</param>
		/// <param name="elapsedTime">The elapsed time.</param>
		/// <returns>How far the camera should move in this direction.</returns>
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

		/// <summary>
		/// Return the target position that the camera is moving to.
		/// </summary>
		/// <returns>The camera's target position.</returns>
		public Vector3 getCameraTargetPosition()
		{
			Vector3 targetPosition = cameraTarget == null ? Vector3.Zero : new Vector3(cameraTarget.position, 0);

			if (SnailsPace.inputManager.inputDown("Camera"))
			{
				int screenWidth = SnailsPace.videoConfig.getInt("width");
				int screenHeight = SnailsPace.videoConfig.getInt("height");
				Vector2 centerPositionOffset = new Vector2(screenWidth / 2, screenHeight / 2);
				float mouseX = (SnailsPace.inputManager.mousePosition.X - centerPositionOffset.X) * .9f;
				float mouseY = (centerPositionOffset.Y - SnailsPace.inputManager.mousePosition.Y)*.9f;
				if (mouseX < 0)
				{
					mouseX = Math.Max(mouseX, -screenWidth / 2);
				}
				else
				{
					mouseX = Math.Min(mouseX, screenWidth / 2);
				}
				if (mouseY < 0)
				{
					mouseY = Math.Max(mouseY, -screenHeight / 2);
				}
				else
				{
					mouseY = Math.Min(mouseY, screenHeight/2);
				}
				Vector2 mousePosition = new Vector2(mouseX, mouseY); 
				cameraTargetOffset = new Vector3(mousePosition, normalCameraDistance);
			} else {
				cameraTargetOffset = new Vector3(cameraTarget.velocity * new Vector2(.35f,.35f), normalCameraDistance);
			}
			return targetPosition + cameraTargetOffset;
		}

		/// <summary>
		/// Render the scene.
		/// </summary>
		/// <param name="objects">All GameObjects to be rendered.</param>
		/// <param name="strings">Strings of text to render.</param>
		/// <param name="gameTime">The current time.</param>
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
				// If the camera is not at it's target position, move the camera toward the target.
                float elapsedTime = (float)Math.Min(gameTime.ElapsedRealTime.TotalSeconds, 1);
                Vector3 cameraDifference = cameraTargetPosition - cameraPosition;
                Vector3 cameraPositionMovement = Vector3.Zero;
                cameraPositionMovement.X = calculateCameraMovement(cameraDifference.X, elapsedTime);
                cameraPositionMovement.Y = calculateCameraMovement(cameraDifference.Y, elapsedTime);
                cameraPositionMovement.Z = calculateCameraMovement(cameraDifference.Z, elapsedTime);
                cameraPosition = cameraPosition + cameraPositionMovement;

				// Keep the camera inside the level's specified bounds.
                float cameraStopXDistance = (float)(1000 * Math.Tan(45 / 2.0));
                float cameraStopYDistance = (float)(1000 * Math.Tan(MathHelper.PiOver4 / 2.0));
                for (int i = 1; i < cameraBounds.Length; i++)
                {
                    if (cameraBounds[i].X == cameraBounds[i - 1].X)
                    {
						// This is a vertical bounding line, check the camera position to the left and right.
                        if ((cameraBounds[i].X < 0) && (cameraPosition.X - cameraStopXDistance < cameraBounds[i].X))
                            cameraPosition.X = cameraBounds[i].X + cameraStopXDistance;
                        else if ((cameraBounds[i].X > 0) && (cameraPosition.X + cameraStopXDistance > cameraBounds[i].X))
                            cameraPosition.X = cameraBounds[i].X - cameraStopXDistance;
                    }
                    else if (cameraBounds[i].Y == cameraBounds[i - 1].Y)
                    {
						// This is a horizontal bounding line, check the camera position on top and bottom.
						if ((cameraBounds[i].Y > 0) && (cameraPosition.Y + cameraStopYDistance > cameraBounds[i].Y))
							cameraPosition.Y = cameraBounds[i].Y - cameraStopYDistance;
						else if ((cameraBounds[i].Y < 0) && (cameraPosition.Y - cameraStopYDistance < cameraBounds[i].Y))
							cameraPosition.Y = cameraBounds[i].Y + cameraStopYDistance;
                    }
                }
            }

            Viewport viewport = SnailsPace.getInstance().GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;
            cameraProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
            cameraView = Matrix.CreateLookAt(cameraPosition, cameraPosition + Vector3.Forward, Vector3.Up);
            BoundingFrustum viewFrustum = new BoundingFrustum(cameraView * cameraProjection);

#if DEBUG
			// If debugging bounding boxes, make sure the list exists.
			if (boundingBoxVertices == null)
			{
				boundingBoxVertices = new List<VertexPositionColorTexture[]>();
			}
			else
			{
				boundingBoxVertices.Clear();
			}
#endif
            if (objects != null)
            {
                List<Objects.GameObject>.Enumerator objectEnumerator = objects.GetEnumerator();

                SnailsPace.getInstance().GraphicsDevice.VertexDeclaration = new VertexDeclaration(SnailsPace.getInstance().GraphicsDevice, VertexPositionColorTexture.VertexElements);
                while (objectEnumerator.MoveNext())
                {
					// Iterate over each GameObject and draw it.
                    drawObject(objectEnumerator.Current, viewFrustum);
#if DEBUG
                    if (SnailsPace.debugBoundingBoxes && objectEnumerator.Current.collidable)
                    {
						// Draw a bounding box if we are debugging bounding boxes and this object has one.
                        Objects.GameObjectBounds boundingBox = objectEnumerator.Current.bounds;
						Vector2[] boxVertices = boundingBox.Points;
                        VertexPositionColorTexture[] visualBoxVertices = new VertexPositionColorTexture[boxVertices.Length + 2];
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
                if (SnailsPace.debugTriggers)
                {
					// Draw a trigger box if we are debugging triggers.
                    List<Objects.Trigger>.Enumerator triggers = Engine.map.triggers.GetEnumerator();

                    while (triggers.MoveNext())
                    {
						// For each trigger, draw a box and add it to the list with the bounding boxes.
                        Objects.GameObjectBounds boundingBox = triggers.Current.bounds;
						Vector2[] boxVertices = boundingBox.Points;
                        VertexPositionColorTexture[] visualBoxVertices = new VertexPositionColorTexture[boxVertices.Length + 2];
                        visualBoxVertices[0].Color = triggerBoxCenterColor;
                        visualBoxVertices[0].Position = new Vector3(triggers.Current.position, 1);
                        visualBoxVertices[visualBoxVertices.Length - 1].Position = new Vector3(boxVertices[0], 10);
                        visualBoxVertices[visualBoxVertices.Length - 1].Color = triggerBoxColor;
                        for (int boxVertexIndex = 0; boxVertexIndex < boxVertices.Length; boxVertexIndex++)
                        {
                            visualBoxVertices[boxVertexIndex + 1].Position = new Vector3(boxVertices[boxVertexIndex], 1);
                            visualBoxVertices[boxVertexIndex + 1].Color = triggerBoxColor;
                        }
                        boundingBoxVertices.Add(visualBoxVertices);
                    }
					triggers.Dispose();
                }

                // TODO: this probably isn't how we want to do this if we end up using more than one effect
                Effect effect = getOrCreateEffect("Resources/Effects/effects");
                effect.CurrentTechnique = effect.Techniques["Colored"];
                effect.Parameters["xView"].SetValue(cameraView);
                effect.Parameters["xProjection"].SetValue(cameraProjection);
                effect.Parameters["xWorld"].SetValue(Matrix.Identity);
                effect.Begin();

				// Draw all the bounding and trigger boxes.
                List<VertexPositionColorTexture[]>.Enumerator boundingBoxEnumerator = boundingBoxVertices.GetEnumerator();
                while (boundingBoxEnumerator.MoveNext())
                {
                    IEnumerator<EffectPass> effectPassEnumerator = effect.CurrentTechnique.Passes.GetEnumerator();
                    while (effectPassEnumerator.MoveNext())
                    {
                        effectPassEnumerator.Current.Begin();
                        // The vertex declaration needs to be set before it gets here or it will fail.
                        SnailsPace.getInstance().GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleFan, boundingBoxEnumerator.Current, 0, boundingBoxEnumerator.Current.Length - 2);
                        effectPassEnumerator.Current.End();
                    }
                    effectPassEnumerator.Dispose();
                }
                boundingBoxEnumerator.Dispose();
                effect.End();
#endif
            }

            if (strings == null)
            {
                strings = new List<Objects.Text>();
            }

            SpriteBatch batch = new SpriteBatch(SnailsPace.getInstance().GraphicsDevice);
            batch.Begin();

            // Draw HUD
            {
                batch.Draw(Engine.healthIcon, new Rectangle(0, 0, 32, 32), Color.White);
                batch.Draw(Engine.healthBar, new Rectangle(32, 8, (int)((Player.helix.health / (float)Player.helix.maxHealth) * 300), 16), Color.White);
                batch.Draw(Engine.fuelIcon, new Rectangle(0, 24, 32, 32), Color.White);
                batch.Draw(Engine.fuelBar, new Rectangle(32, 32, (int)((Player.helix.fuel / Player.helix.maxFuel) * 300), 16), Color.White);

				// If we're fighting a boss, draw them a health bar.
				if (Engine.boss != null)
				{
					batch.Draw(Engine.bossHealthShadow, new Rectangle(100, 526, 600, 60), Color.White);
					batch.Draw(Engine.bossHealthBar, new Rectangle(118, 545, (int)((Engine.boss.health / (float)Engine.boss.maxHealth) * 566), 20), Color.White);
				}
            }

            // Draw the Inventory
            {
                Texture2D weaponTable = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/WeaponTable");
                Texture2D separator = SnailsPace.getInstance().Content.Load<Texture2D>("Resources/Textures/PauseScreen");
                SpriteFont font = SnailsPace.getInstance().Content.Load<SpriteFont>("Resources/Fonts/Score");

                int y = 64;
                int bigSize = 32;
                int smallSize = 24;
                Objects.Weapon currentWeapon = Player.helix.weapon;
                for(int i = 0; i < Player.helix.inventory.Length; i++)
                {
					// Draw each item in the player's inventory.
                    batch.Draw(separator, new Rectangle(8, y++, 32, 1), new Rectangle(1, 0, 1, 1), Color.White);
                    
                    Objects.Weapon weapon = Player.helix.inventory[i];
                    if (weapon == null)
                    {
                        y += 8;
                        continue;
                    }

                    int tx = weapon.sprite.animationStart % 4, ty = weapon.sprite.animationStart / 4;
                    Rectangle spot, source;

                    if (currentWeapon == weapon)
                    {
						// If this is the weapon we're using, make it pop out by displaying it bigger and with white text.
                        Color textColor = Color.White;
                        spot = new Rectangle(0, y, bigSize * 2, bigSize);
                        strings.Add(new Objects.Text(weapon.name, font, new Vector2(bigSize * 2, y), new Vector2(0.5f, 0.5f), textColor));
                        strings.Add(new Objects.Text((weapon.ammunition != -1 ? weapon.ammunition.ToString() : "Inf"), font, new Vector2(bigSize * 2, y + bigSize / 2.25f), new Vector2(0.5f, 0.5f), textColor));
                        y += bigSize;
                    }
                    else
                    {
						// This isn't the weapon we're using, give it a smaller icon and gray text.
                        Color textColor = Color.LightGray;
                        spot = new Rectangle(0, y, smallSize * 2, smallSize);
                        strings.Add(new Objects.Text(weapon.name, font, new Vector2(smallSize * 2, y), new Vector2(0.4f, 0.4f), textColor));
                        strings.Add(new Objects.Text((weapon.ammunition != -1 ? weapon.ammunition.ToString() : "Inf"), font, new Vector2(smallSize * 2, y + smallSize / 2.25f), new Vector2(0.4f, 0.4f), textColor));
                        y += smallSize;
                    }
                    
                    source = new Rectangle(tx * 128, ty * 64, 128, 64);
                    batch.Draw(weaponTable, spot, source, Color.White);
                }

                batch.Draw(separator, new Rectangle(8, y++, 32, 1), new Rectangle(0, 0, 1, 1), Color.White);
            }

            // Draw text strings
            List<Objects.Text>.Enumerator textEnumerator = strings.GetEnumerator();
            while (textEnumerator.MoveNext())
            {
                batch.DrawString(textEnumerator.Current.font, textEnumerator.Current.content, textEnumerator.Current.position, textEnumerator.Current.color, textEnumerator.Current.rotation, Vector2.Zero, textEnumerator.Current.scale, SpriteEffects.None, 0);
            }
            textEnumerator.Dispose();

            batch.End();
			batch.Dispose();
        }

		/// <summary>
		/// Draw a GameObject with respect to the view frustum.
		/// </summary>
		/// <param name="obj">The GameObject to draw.</param>
		/// <param name="viewFrustum">The view frustum.</param>
		private void drawObject(Objects.GameObject obj, BoundingFrustum viewFrustum)
		{
			Dictionary<String, Objects.Sprite>.ValueCollection.Enumerator spriteEnumerator = obj.sprites.Values.GetEnumerator();

			// Iterate over all this object's sprites.
			while (spriteEnumerator.MoveNext())
			{
				// Check if this sprite is visible.
				if (spriteEnumerator.Current.visible)
				{
					Vector3 objectPosition = new Vector3(obj.position + spriteEnumerator.Current.position, -obj.layer - spriteEnumerator.Current.layerOffset);
					Vector3 objectScale = new Vector3(Vector2.Multiply(spriteEnumerator.Current.image.size, obj.scale), 1);

                    BoundingSphere sphere =  new BoundingSphere(objectPosition, objectScale.Length() );

					// Only draw the object if we can actually see it.
					if (viewFrustum.Intersects(sphere))
					{
						int xBlock = (int)(spriteEnumerator.Current.frame % spriteEnumerator.Current.image.blocks.X);
						int yBlock = (int)((spriteEnumerator.Current.frame - xBlock) / spriteEnumerator.Current.image.blocks.X);

						Matrix translationMatrix = Matrix.CreateScale(objectScale) * Matrix.CreateRotationZ(obj.rotation + spriteEnumerator.Current.rotation) *
							Matrix.CreateTranslation(objectPosition);
						VertexPositionColorTexture[] objVertices = new VertexPositionColorTexture[vertices.Length];
						int xFlip = 0;
						if (spriteEnumerator.Current.horizontalFlip && !obj.horizontalFlip || obj.horizontalFlip && !spriteEnumerator.Current.horizontalFlip)
						{
							xFlip = 1;
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

							int xMod = 1 - ( index + xFlip ) % 2;
							int yMod = 0;
							if (index == 0 || index == 1)
							{
								yMod = 1 + yFlip;
							}
							objVertices[index].TextureCoordinate.X = (xBlock + xMod) / spriteEnumerator.Current.image.blocks.X;
							objVertices[index].TextureCoordinate.Y = (yBlock + yMod) / spriteEnumerator.Current.image.blocks.Y;
						}

						// TODO: this probably isn't how we want to do this if we end up using more than one effect
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
							SnailsPace.getInstance().GraphicsDevice.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleStrip, objVertices, 0, 2);
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

		/// <summary>
		/// Initialize the collection of vertices used by the renderer.
		/// </summary>
		private void setUpVertices()
		{
			vertices = new VertexPositionColorTexture[4];
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
