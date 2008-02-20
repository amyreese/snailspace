xOffset = 0
yOffset = 3136

wallHeight = 4

rotmod = 5
-- Floor
WorldBuilding.BuildObject( { xOffset=2.2 * -woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=1.8 * -woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=-woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=1.8 * woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=2.6 * woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=3.4 * woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=4.2 * woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=4.6 * woodSprite.image.size.X, yOffset=0, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )

-- Side walls
for i=0,wallHeight do
	-- Left
	WorldBuilding.BuildObject( { xOffset=-2.3 * woodSprite.image.size.X, yOffset=( 0.4 + 0.8 * i ) * woodSprite.image.size.X, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=MathHelper.PiOver2 + newRotation() } )

	-- Right
	WorldBuilding.BuildObject( { xOffset=4.7 * woodSprite.image.size.X, yOffset=( 0.4 + 0.8 * i ) * woodSprite.image.size.X, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=MathHelper.PiOver2 + newRotation() } )
end

-- Ceiling
for i=0,8 do
	WorldBuilding.BuildObject( { xOffset= ( -2.0 + 0.8 * i ) * woodSprite.image.size.X, yOffset=( 0.8 + 0.8 * wallHeight ) * woodSprite.image.size.X, sprite=woodSprite, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
end

-- Window
WorldBuilding.BuildObject( { xOffset=2.8 * woodSprite.image.size.X, yOffset=1.3 * woodSprite.image.size.X, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=2.6 * woodSprite.image.size.X, yOffset=2.7 * woodSprite.image.size.X, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
WorldBuilding.BuildObject( { xOffset=2.1 * woodSprite.image.size.X, yOffset=2.2 * woodSprite.image.size.X, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=MathHelper.PiOver2 + newRotation() } )
WorldBuilding.BuildObject( { xOffset=3.3 * woodSprite.image.size.X, yOffset=1.8 * woodSprite.image.size.X, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=MathHelper.PiOver2 + newRotation() } )
Spider(Vector2(xOffset + 1290, yOffset + 564))
Spider(Vector2(xOffset + 1400, yOffset + 1264))

Hive( Vector2( xOffset - 1.8 * woodSprite.image.size.X, yOffset + ( 0.2 + 0.8 * wallHeight ) * woodSprite.image.size.X ) )
