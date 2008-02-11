--[[
TODOs:
 Add webs to salt ramp and "low road"
]]--

spiderAttack = false

xOffset = 0
yOffset = -768

-- Drop Left Wall
WorldBuilding.BuildSection( {height=17, xOffset=0, yOffset=0, sprite=dirtSprite, xOverlap=20, yOverlap=20, buildDown=true } )

-- Drop Right Wall
WorldBuilding.BuildRampDEPR( 9, 2048, 0, dirtSprite, - 0.25 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 11, 1584, -2016, dirtSprite, 0.35 - MathHelper.PiOver2, 20 )

-- Right Wall Hook
WorldBuilding.BuildRampDEPR( 1, 2496 - 64, -4084 - 128, dirtSprite, -0.60 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 2.5, -4084 - 128 * 2, dirtSprite, -0.70 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 4.5, -4084 - 128 * 3, dirtSprite, -0.80 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 7, -4084 - 128 * 4, dirtSprite, -0.90 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 9, -4084 - 128 * 5, dirtSprite, -0.70 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRampDEPR( 1, 2496 - 64 * 10.5, -4084 - 128 * 6 - 32, dirtSprite, -0.40 - MathHelper.PiOver2, 20 )


-- **** Salt Platforms ****
-- ** Platform 1 **
-- Platform
WorldBuilding.BuildSection( {width=5, xOffset=856, yOffset=-310, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt Can
saltcan = WorldBuilding.BuildObject( {xOffset=920, yOffset=-saltcanImage.size.Y / 2, sprite=saltcanSprite, spriteName="can", layerOffset=-2, rotation=MathHelper.PiOver2 } )
saltcan.collidable = true;

-- Salt Can Save Point
SavePoints.BuildSavePoint( 1200, -176 )

-- Salt Pouring Off
WorldBuilding.BuildObject( {xOffset=768, yOffset=-pourImage.size.Y + 32, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=768, yOffset=-1.5 * pourImage.size.Y + 32, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
Traps.SaltPile( 768, 1.25 * -pourImage.size.Y + 32, pourImage.size.X, 1.5 * pourImage.size.Y )

-- Spider
spider = Spider(Vector2(xOffset + 1176, yOffset - 438))
spider.state.attacking = spiderAttack

-- ** Platform 2 **
-- Platform
WorldBuilding.BuildSection( {width=5, xOffset=400, yOffset=-1024, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt Pile
saltpile = WorldBuilding.BuildObject( {xOffset=784, yOffset=-1024 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 784, -1024 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

-- Salt Pouring Off
WorldBuilding.BuildObject( {xOffset=942, yOffset=-pourImage.size.Y / 2 - 960, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=942, yOffset=-1.5 * pourImage.size.Y - 960, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
Traps.SaltPile( 942, -pourImage.size.Y - 960, pourImage.size.X, 2 * pourImage.size.Y )

-- Spider
spider = Spider(Vector2(xOffset + 496, yOffset - 1152))
spider.state.attacking = spiderAttack

-- ** Platform 3 **
-- Platform
WorldBuilding.BuildSection( {width=6, xOffset=462, yOffset=-2048, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt Pile
saltpile = WorldBuilding.BuildObject( {xOffset=958, yOffset=-2048 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 958, -2048 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

-- Salt Pouring Off
WorldBuilding.BuildObject( {xOffset=1122, yOffset=-pourImage.size.Y / 2 - 1984, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1122, yOffset=-1.5 * pourImage.size.Y - 1984, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
Traps.SaltPile( 1122, -pourImage.size.Y - 1984, pourImage.size.X, 2 * pourImage.size.Y )

-- Spider
spider = Spider(Vector2(xOffset + 446, yOffset - 2176))
spider.state.attacking = spiderAttack

-- ** Platform 4 **
-- Platform
WorldBuilding.BuildSection( {width=3, xOffset=984, yOffset=-3060, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt Pile
saltpile = WorldBuilding.BuildObject( {xOffset=1130, yOffset=-3060 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 1130, -3060 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

saltpile = WorldBuilding.BuildObject( {xOffset=1074, yOffset=-3060 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 1074, -3060 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

-- Salt Pouring Off Left
WorldBuilding.BuildObject( {xOffset=904, yOffset=-pourImage.size.Y / 2 - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=904, yOffset=-1 * pourImage.size.Y - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
Traps.SaltPile( 904, -0.75 * pourImage.size.Y - 2996, pourImage.size.X, 1.5 * pourImage.size.Y )

-- Salt Pouring Off Right
WorldBuilding.BuildObject( {xOffset=1288, yOffset=-pourImage.size.Y / 2 - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1288, yOffset=-1.5 * pourImage.size.Y - 2996, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
Traps.SaltPile( 1288, -1 * pourImage.size.Y - 2996, pourImage.size.X, 2 * pourImage.size.Y )


-- Spider
spider = Spider(Vector2(xOffset + 1112, yOffset - 3188))
spider.state.attacking = spiderAttack

-- ** Platform 5 **
-- Platform
WorldBuilding.BuildSection( {width=4, xOffset=668, yOffset=-3828, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt Pile
saltpile = WorldBuilding.BuildObject( {xOffset=930, yOffset=-3828 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 930, -3828 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

-- Salt Pouring Off
WorldBuilding.BuildObject( {xOffset=1096, yOffset=-pourImage.size.Y / 2 - 3764, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1096, yOffset=-1.5 * pourImage.size.Y - 3764, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
WorldBuilding.BuildObject( {xOffset=1096, yOffset=-2 * pourImage.size.Y - 3764, sprite=pourSprite, spriteName="pour", layerOffset=2, collidable=false } )
Traps.SaltPile( 1096, -1.25 * pourImage.size.Y - 3764, pourImage.size.X, 2.5 * pourImage.size.Y )

-- Spider
spider = Spider(Vector2(xOffset + 704, yOffset - 3956))
spider.state.attacking = spiderAttack

-- ** Platform 6 **
-- Platform
WorldBuilding.BuildSection( {width=5, xOffset=1176, yOffset=-4084, sprite=dirtSpriteS, xOverlap=10, yOverlap=10 } )

-- Salt Pile
saltpile = WorldBuilding.BuildObject( {xOffset=1272, yOffset=-4084 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
saltpile.horizontalFlip = true
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 1272, -4084 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

-- See Platform 5 for Salt Pouring Off

-- Spider
spider = Spider(Vector2(xOffset + 1496, yOffset - 4212))
spider.state.attacking = spiderAttack

-- **** Salt Ramp ("Big Rock") ****

-- Left Wall
WorldBuilding.BuildSection( {height=10, xOffset=960, yOffset=-4776, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )

-- Save Point
SavePoints.BuildSavePoint( 960, -4584 )

-- ** Upper Ramp **
-- Ramp
WorldBuilding.BuildRamp( {length=5, xOffset=1024, yOffset=-5120, sprite=gravelSprite, rotation=-0.93, overlap=20 } )

-- Salt
WorldBuilding.BuildObject( {xOffset=1152, yOffset=-5100, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.93, collidable=false} )
WorldBuilding.BuildObject( {xOffset=1440, yOffset=-5484, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.93, collidable=false} )
WorldBuilding.BuildObject( {xOffset=1728, yOffset=-5868, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.93, collidable=false} )
Traps.SaltPile( 1440, -5484, pourImage.size.X, 2.5 * pourImage.size.Y, MathHelper.PiOver2-0.93 )

-- ** Middle Ramp **
-- Ramp
WorldBuilding.BuildRamp( {length=5, xOffset=1664, yOffset=-5950, sprite=gravelSprite, rotation=-0.67, overlap=20 } )

-- Salt
WorldBuilding.BuildObject( {xOffset=1856, yOffset=-5950, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.67, collidable=false} )
WorldBuilding.BuildObject( {xOffset=2240, yOffset=-6251, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.67, collidable=false} )
WorldBuilding.BuildObject( {xOffset=2624, yOffset=-6552, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.67, collidable=false} )
Traps.SaltPile( 2240, -6251, pourImage.size.X, 2.5 * pourImage.size.Y, MathHelper.PiOver2-0.67 )

-- ** Lower Ramp **
-- Ramp
WorldBuilding.BuildRampDEPR( 5, 2460, -6600, gravelSprite, -0.37, 20 )

-- Salt
WorldBuilding.BuildObject( {xOffset=2805, yOffset=-6600, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.37, collidable=false} )
WorldBuilding.BuildObject( {xOffset=3253, yOffset=-6778, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.37, collidable=false} )
WorldBuilding.BuildObject( {xOffset=3477, yOffset=-6867, sprite=pourSprite, spriteName="pour", rotation=MathHelper.PiOver2-0.37, collidable=false} )
Traps.SaltPile( 3253, -6778, pourImage.size.X, 2.5 * pourImage.size.Y, MathHelper.PiOver2-0.37 )

-- ** Bottom Wall **
-- Wall
WorldBuilding.BuildSection( {width=19, xOffset=960, yOffset=-7016, sprite=gravelSprite, xOverlap=20, yOverlap=20 } )

-- Salt Pile on Bottom
saltpile = WorldBuilding.BuildObject( {xOffset=3653, yOffset=-6960 + saltpileImage.size.Y * 4 / 5, sprite=saltpileSprite, layerOffset=-2, collidable=true } )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -120, -55 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 140, -55 ) )
boundBuilder:AddPoint( Vector2( 0, 61 ) )
saltpile.bounds = boundBuilder:BuildBounds()
saltpile.bounds:Move( saltpile.position )

saltpileTrap = Traps.SaltPile( 3653, -6960 + saltpileImage.size.Y * 4 / 5, saltpile.size.X, saltpile.size.Y )
boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
saltpileTrap.bounds = boundBuilder:BuildBounds()
saltpileTrap.bounds:Move( saltpileTrap.position )

-- ** Rock Filler **
WorldBuilding.BuildSection( {height=7, xOffset=1152, yOffset=-5440, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=6, xOffset=1368, yOffset=-5716, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=5, xOffset=1584, yOffset=-5972, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=4, xOffset=1800, yOffset=-6260, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=3, xOffset=2016, yOffset=-6420, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {height=2, xOffset=2232, yOffset=-6580, sprite=gravelSprite, xOverlap=20, yOverlap=10, buildDown=true } )
WorldBuilding.BuildSection( {width=3, xOffset=2320, yOffset=-6798, sprite=gravelSprite, xOverlap=20, yOverlap=20 } )

-- ** Wall Over Salt Ramp **
-- Wall
WorldBuilding.BuildRampDEPR( 11, 1824, -4992, dirtSprite, -0.65, 20 )

-- Spiders
yTweak = math.sin( -0.65 ) * ( 156 )
xTweak = math.cos( -0.65 ) * ( 156 )
for x = -1, 14 do
	rndNum = ( math.random() - 0.5 ) / 2
	spider = Spider(Vector2(xOffset + 1824 + xTweak * x + xTweak * rndNum, yOffset - 5264 + yTweak * x + yTweak * rndNum))
	spider.state.attacking = spiderAttack
end

-- Health & Fuel
yTweak = math.sin( -0.65 ) * ( 768 )
xTweak = math.cos( -0.65 ) * ( 768 )
for x = 0, 2 do
	rndNum = ( math.random() - 0.5 ) / 2
	Powerups.BuildFuelPowerup( 1824 + xTweak * x + xTweak * rndNum, - 5392 + yTweak * x + yTweak * rndNum )
end
for x = 0, 1 do
	rndNum = 0.5 + ( math.random() - 0.5 ) / 2
	Powerups.BuildHealthPowerup( 1824 + xTweak * x + xTweak * rndNum, - 5392 + yTweak * x + yTweak * rndNum )
end

-- ** Wall over area after Salt Ramp
-- Wall
WorldBuilding.BuildSection( {width=7, xOffset=3856, yOffset=-6472, sprite=dirtSprite, xOverlap=20, yOverlap=20 } )
WorldBuilding.BuildRampDEPR( 4, 3856 + 236*7 - 128, -6536, dirtSprite, -0.65, 20 )

-- Spiders
for x = 0, 5 do
	spider = Spider(Vector2(xOffset + 3800 + 256 * (x + ( math.random() - 0.5 ) / 2), yOffset - 6637))
	spider.state.attacking = spiderAttack
end

-- ** Wall left of Salt Ramp **
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

-- ** "Low Road" **
-- Floor
WorldBuilding.BuildSection( {width=24, xOffset=484, yOffset=-7528, sprite=dirtSprite, xOverlap=20, yOverlap=20 } )

-- Spiders
for x = 0, 16 do
	spider = Spider(Vector2(xOffset + 960 + 256 * (x + ( math.random() - 0.5 ) / 2), yOffset -7208))
	spider.state.attacking = spiderAttack
end


-- ** Exit Save Point **
SavePoints.BuildSavePoint( 5892, -7336 )
