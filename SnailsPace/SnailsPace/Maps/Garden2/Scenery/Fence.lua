xOffset = 1536
yOffset = 0

WorldBuilding.BuildSection( { width=10, xOffset=0, yOffset=-192, sprite=grassSprite, xOverlap=20, yOverlap=20, xSizeMod=-32, ySizeMod=-32 } )
WorldBuilding.BuildSection( { width=20, xOffset=-48, yOffset=-320, sprite=dirtSpriteS, xOverlap=10, yOverlap=10, xSizeMod=-32, ySizeMod=-32, layerOffset=-2 } )
WorldBuilding.BuildSection( { width=19, xOffset=16, yOffset=-438, sprite=dirtSpriteS, xOverlap=10, yOverlap=10, xSizeMod=-32, ySizeMod=-32, layerOffset=-2, rotation=MathHelper.Pi } )
WorldBuilding.BuildSection( { width=18, xOffset=80, yOffset=-556, sprite=dirtSpriteS, xOverlap=10, yOverlap=10, xSizeMod=-32, ySizeMod=-32, layerOffset=-2 } )
WorldBuilding.BuildSection( { width=12, xOffset=448, yOffset=-674, sprite=dirtSpriteS, xOverlap=10, yOverlap=10, xSizeMod=-32, ySizeMod=-32, layerOffset=-2 } )
WorldBuilding.BuildRamp( {length=3, xOffset=-24, yOffset=-400, sprite=dirtSpriteS, overlap=10, rotation=-1.05, layerOffset=2 } )
WorldBuilding.BuildRamp( {length=6, xOffset=144, yOffset=-640, sprite=dirtSpriteS, overlap=10, rotation=-0.15, layerOffset=2 } )
WorldBuilding.BuildRamp( {length=12, xOffset=840, yOffset=-728, sprite=dirtSpriteS, overlap=10, rotation=0.05, layerOffset=2 } )
WorldBuilding.BuildObject( { xOffset=512, yOffset=fencePostImage.size.Y / 2 - 384, sprite=fencePostSprite, xSizeMod=-256, layerOffset=-3 } )
