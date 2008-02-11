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
WorldBuilding.BuildObject( { xOffset=352, yOffset=2960, sprite=beehiveSprite, xSizeMod=-50, layerOffset=-3, rotation=-0.35 } )

bee = Bee( Vector2( xOffset + 128, yOffset + 256 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 532 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 808 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 946 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 1222 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 1360 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 1498 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 1636 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 1774 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 1912 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 2050 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 2188 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 2326 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 2464 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 2602 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 128, yOffset + 2740 ), "flyDownLeft" )
bee.state.attacking = true

bee = Bee( Vector2( xOffset + -64, yOffset + 1498 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 1636 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 1774 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 1912 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2050 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2188 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2326 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2464 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2602 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2740 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -64, yOffset + 2878 ), "flyDownLeft" )
bee.state.attacking = true

bee = Bee( Vector2( xOffset + -260, yOffset + 2188 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -260, yOffset + 2326 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -260, yOffset + 2464 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -260, yOffset + 2602 ), "flyDownRight" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -260, yOffset + 2740 ), "flyDownLeft" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + -260, yOffset + 2878 ), "flyDownRight" )
bee.state.attacking = true
