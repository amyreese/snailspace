xOffset = saltPitXOffset
yOffset = saltPitYOffset

-- Drop Left Wall
WorldBuilding.BuildSection( {height=17, xOffset=0, yOffset=0, sprite=dirtSprite, xOverlap=20, yOverlap=20, buildDown=true } )

-- Drop Right Wall
WorldBuilding.BuildRampDEPR( 9, 2048, 0, dirtSprite, - 0.25 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 11, 1584, -2016, dirtSprite, 0.35 - MathHelper.PiOver2, 20 )

platformOffset = -104

-- Salt Can
saltcan = WorldBuilding.BuildObject( {xOffset=920, yOffset=-saltcanImage.size.Y / 2, sprite=saltcanSprite, spriteName="can", layerOffset=-2, rotation=MathHelper.PiOver2 } )
saltcan.collidable = true;

-- Salt platforms
--Platform 1
WorldBuilding.BuildSection( {width=5, xOffset=856, yOffset=-310, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt for platform 1
WorldBuilding.BuildObject( {xOffset=768, yOffset=-pourImage.size.Y + 32, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=768, yOffset=-1.5 * pourImage.size.Y + 32, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
Traps.SaltPile( 768, 1.25 * -pourImage.size.Y + 32, pourImage.size.X, 1.5 * pourImage.size.Y )

saltpile = WorldBuilding.BuildObject( {xOffset=768, yOffset=-1024 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -140, -45 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -45 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

-- Platform 2
WorldBuilding.BuildSection( {width=5, xOffset=400, yOffset=-1024, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt for platform 2
WorldBuilding.BuildObject( {xOffset=942, yOffset=-pourImage.size.Y / 2 - 960, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=942, yOffset=-1.5 * pourImage.size.Y - 960, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
Traps.SaltPile( 942, -pourImage.size.Y - 960, pourImage.size.X, 2 * pourImage.size.Y )

-- Platform 3
WorldBuilding.BuildSection( {width=6, xOffset=462, yOffset=-2048, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt for platform 3
WorldBuilding.BuildObject( {xOffset=1122, yOffset=-pourImage.size.Y / 2 - 1984, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1122, yOffset=-1.5 * pourImage.size.Y - 1984, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
Traps.SaltPile( 1122, -pourImage.size.Y - 1984, pourImage.size.X, 2 * pourImage.size.Y )

-- Platform 4
WorldBuilding.BuildSection( {width=3, xOffset=984, yOffset=-3060, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Left Salt for platform 4
WorldBuilding.BuildObject( {xOffset=904, yOffset=-pourImage.size.Y / 2 - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=904, yOffset=-1 * pourImage.size.Y - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
Traps.SaltPile( 904, -0.75 * pourImage.size.Y - 2996, pourImage.size.X, 1.5 * pourImage.size.Y )

-- Right Salt for platform 4
WorldBuilding.BuildObject( {xOffset=1288, yOffset=-pourImage.size.Y / 2 - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1288, yOffset=-1.5 * pourImage.size.Y - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
Traps.SaltPile( 1288, -1 * pourImage.size.Y - 2996, pourImage.size.X, 2 * pourImage.size.Y )

-- Platforms 5 & 6
WorldBuilding.BuildSection( {width=4, xOffset=668, yOffset=-3828, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )
WorldBuilding.BuildSection( {width=5, xOffset=1176, yOffset=-4084, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt for platforms 5 & 6
WorldBuilding.BuildObject( {xOffset=1096, yOffset=-pourImage.size.Y / 2 - 3764, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1096, yOffset=-1.5 * pourImage.size.Y - 3764, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1096, yOffset=-2 * pourImage.size.Y - 3764, sprite=pourSprite, spriteName="pour", layerOffset=2, xSizeMod=-75, collidable=false } )
Traps.SaltPile( 1096, -1.25 * pourImage.size.Y - 3764, pourImage.size.X, 2.5 * pourImage.size.Y )

-- Salt Ramp ("Big Rock")
-- Ramp 1
WorldBuilding.BuildRampDEPR( 5, 1024, -5120, gravelSprite, -0.93, 20 )
WorldBuilding.BuildObject( {xOffset=1152, yOffset=-5100, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.93, xSizeMod=-75, collidable=false} )
WorldBuilding.BuildObject( {xOffset=1440, yOffset=-5484, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.93, xSizeMod=-75, collidable=false} )
WorldBuilding.BuildObject( {xOffset=1728, yOffset=-5868, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.93, xSizeMod=-75, collidable=false} )
Traps.SaltPile( 1440, -5484, pourImage.size.X, 2.5 * pourImage.size.Y, MathHelper.PiOver2-0.93 )

-- Ramp 2
WorldBuilding.BuildRampDEPR( 5, 1664, -5950, gravelSprite, -0.67, 20 )
WorldBuilding.BuildObject( {xOffset=1856, yOffset=-5950, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.67, xSizeMod=-75, collidable=false} )
WorldBuilding.BuildObject( {xOffset=2240, yOffset=-6251, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.67, xSizeMod=-75, collidable=false} )
WorldBuilding.BuildObject( {xOffset=2624, yOffset=-6552, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.67, xSizeMod=-75, collidable=false} )
Traps.SaltPile( 2240, -6251, pourImage.size.X, 2.5 * pourImage.size.Y, MathHelper.PiOver2-0.67 )

-- Ramp 3
WorldBuilding.BuildRampDEPR( 5, 2460, -6600, gravelSprite, -0.37, 20 )
WorldBuilding.BuildObject( {xOffset=2805, yOffset=-6600, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.37, xSizeMod=-75, collidable=false} )
WorldBuilding.BuildObject( {xOffset=3253, yOffset=-6778, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.37, xSizeMod=-75, collidable=false} )
WorldBuilding.BuildObject( {xOffset=3477, yOffset=-6867, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.37, xSizeMod=-75, collidable=false} )
Traps.SaltPile( 3253, -6778, pourImage.size.X, 2.5 * pourImage.size.Y, MathHelper.PiOver2-0.37 )

-- Left
WorldBuilding.BuildSection( {height=10, xOffset=960, yOffset=-4776, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )

-- Bottom
WorldBuilding.BuildSection( {width=19, xOffset=960, yOffset=-7016, sprite=gravelSprite, xOverlap=20, yOverlap=20 } )

-- Filler
WorldBuilding.BuildSection( {height=7, xOffset=1152, yOffset=-5440, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=6, xOffset=1368, yOffset=-5716, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=5, xOffset=1584, yOffset=-5972, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=4, xOffset=1800, yOffset=-6260, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=3, xOffset=2016, yOffset=-6420, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=2, xOffset=2232, yOffset=-6580, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {width=3, xOffset=2320, yOffset=-6798, sprite=gravelSprite, xOverlap=20, yOverlap=20 } )

-- Salt Ramp Top
WorldBuilding.BuildRampDEPR( 11, 1824, -4992, dirtSprite, -0.65, 20 )
WorldBuilding.BuildSection( {width=7, xOffset=3856, yOffset=-6472, sprite=dirtSprite, xOverlap=20, yOverlap=20 } )
WorldBuilding.BuildRampDEPR( 4, 3856 + 236*7 - 128, -6536, dirtSprite, -0.65, 20 )

-- Right wall hook
WorldBuilding.BuildRampDEPR( 1, 2496 - 64, -4084 - 128, dirtSprite, -0.60 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 2.5, -4084 - 128 * 2, dirtSprite, -0.70 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 4.5, -4084 - 128 * 3, dirtSprite, -0.80 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 7, -4084 - 128 * 4, dirtSprite, -0.90 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 9, -4084 - 128 * 5, dirtSprite, -0.70 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 10.5, -4084 - 128 * 6 - 32, dirtSprite, -0.40 - MathHelper.PiOver2, 20 )

-- Left wall hook
WorldBuilding.BuildRampDEPR( 1, 20, -3792, dirtSprite, 0.50 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 20 + 64, -3792 - 128, dirtSprite, 0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 20 + 64 * 2.5, -3792 - 128 * 2, dirtSprite, 0.70 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 20 + 64 * 4.5, -3792 - 128 * 3, dirtSprite, 0.80 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 20 + 64 * 6, -3792 - 128 * 4, dirtSprite, 0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 20 + 64 * 7, -3792 - 128 * 5, dirtSprite, 0.30 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildSection( {height=5, xOffset=484, yOffset=-4624, sprite=dirtSprite, xOverlap=20, yOverlap=20, buildDown=true } )
WorldBuilding.BuildRampDEPR( 1, 8 + 64 * 7, -3792 - 128 * 1 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.30 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 8 + 64 * 6, -3792 - 128 * 2 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 8 + 64 * 4.5, -3792 - 128 * 3 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.80 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 8 + 64 * 2.5, -3792 - 128 * 4 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.70 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 8 + 64, -3792 - 128 * 5 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 8, -3792 - 128 * 5 - 128 * 7 - 64 - 236 * 4, dirtSprite, -0.50 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 6, 8, -3792 - 128 * 5 - 128 * 7 - 64 - 236 * 4, dirtSprite, -1.30, 20 )

-- "Low Road" Bottom
WorldBuilding.BuildSection( {width=24, xOffset=484, yOffset=-7528, sprite=dirtSprite, xOverlap=20, yOverlap=20 } )

-- TODO: Add webs to salt ramp and "low road"
