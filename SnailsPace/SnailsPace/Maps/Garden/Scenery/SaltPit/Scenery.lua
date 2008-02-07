xOffset = saltPitXOffset
yOffset = saltPitYOffset

-- Drop Left Wall
WorldBuilding.BuildSection( 1, 17, 0, 0, dirtSprite, 20, 20, nil, true )

-- Drop Right Wall
-- WorldBuilding.BuildSection( 1, 18, 2560, 0, dirtSprite, 20, 20, nil, true )
WorldBuilding.BuildRamp( 9, 2048, 0, dirtSprite, - 0.25 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 11, 1584, -2016, dirtSprite, 0.35 - MathHelper.PiOver2, 20 )

platformOffset = -104

-- Salt Can
saltcan = WorldBuilding.BuildObject( 1024 + platformOffset, -saltcanImage.size.Y / 2, saltcanSprite, "can", WorldBuilding.defaultLayer - 2, MathHelper.PiOver2 )
saltcan.collidable = true;

-- Salt platforms
--Platform 1
WorldBuilding.BuildSection( 5, 1, 960 + platformOffset, -310, dirtSpriteS, 10, 10, false, true )

-- Salt for platform 1
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 872 + platformOffset, yOffset - pourImage.size.Y + 32 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 872 + platformOffset, yOffset - 1.5 * pourImage.size.Y + 32 )
pour1.layer = WorldBuilding.defaultLayer + 2
map.objects:Add(pour1)

-- Platform 2
WorldBuilding.BuildSection( 5, 1, 504 + platformOffset, -1024, dirtSpriteS, 10, 10, nil, true )

-- Salt for platform 2
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1046 + platformOffset, yOffset - 1024 - pourImage.size.Y / 2 + 64 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = Character()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1046 + platformOffset, yOffset - 1024 - pourImage.size.Y * 3 / 2 + 128 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)

-- Platform 3
WorldBuilding.BuildSection( 6, 1, 566 + platformOffset, -2048, dirtSpriteS, 10, 10, nil, true )

-- Salt for platform 3
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1226 + platformOffset, yOffset - 2048 - pourImage.size.Y / 2 + 64 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = Character()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1226 + platformOffset, yOffset - 2048 - pourImage.size.Y * 3 / 2 + 128 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)

-- Platform 4
WorldBuilding.BuildSection( 3, 1, 1088 + platformOffset, -3060, dirtSpriteS, 10, 10, nil, true )

-- Left Salt for platform 4
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1008 + platformOffset, yOffset - 3060 - pourImage.size.Y / 2 + 64 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1008 + platformOffset, yOffset - 3060 - pourImage.size.Y * 2 / 2 + 128 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)

-- Right Salt for platform 4
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1392 + platformOffset, yOffset - 3060 - pourImage.size.Y / 2 + 64 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1392 + platformOffset, yOffset - 3060 - pourImage.size.Y * 3 / 2 + 128 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)

-- Platforms 5 & 6
WorldBuilding.BuildSection( 4, 1, 772 + platformOffset,  -3828, dirtSpriteS, 10, 10, nil, true )
WorldBuilding.BuildSection( 5, 1, 1280 + platformOffset, -4084, dirtSpriteS, 10, 10, nil, true )

-- Salt for platforms 5 & 6
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1200 + platformOffset, yOffset - 3828 - pourImage.size.Y / 2 + 64 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1200 + platformOffset, yOffset - 3828 - pourImage.size.Y * 3 / 2 + 128 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)
pour1 = GameObject()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1200 + platformOffset, yOffset - 3828 - pourImage.size.Y * 5 / 2 + 128 )
pour1.layer = WorldBuilding.defaultLayer + 1
map.objects:Add(pour1)

-- Salt Ramp ("Big Rock")
-- Ramp 1
WorldBuilding.BuildRamp( 5, 1024, -5120, gravelSprite, -0.93, 20 )
WorldBuilding.BuildObject( 1152, -5100, pourSprite, "pour", nil, MathHelper.PiOver2-0.93, -75 )
WorldBuilding.BuildObject( 1440, -5484, pourSprite, "pour", nil, MathHelper.PiOver2-0.93, -75 )
WorldBuilding.BuildObject( 1728, -5868, pourSprite, "pour", nil, MathHelper.PiOver2-0.93, -75 )

-- Ramp 2
WorldBuilding.BuildRamp( 5, 1664, -5950, gravelSprite, -0.67, 20 )
WorldBuilding.BuildObject( 1856, -5950, pourSprite, "pour", nil, MathHelper.PiOver2-0.67, -75 )
WorldBuilding.BuildObject( 2240, -6251, pourSprite, "pour", nil, MathHelper.PiOver2-0.67, -75 )
WorldBuilding.BuildObject( 2624, -6552, pourSprite, "pour", nil, MathHelper.PiOver2-0.67, -75 )

-- Ramp 3
WorldBuilding.BuildRamp( 5, 2460, -6600, gravelSprite, -0.37, 20 )
WorldBuilding.BuildObject( 2805, -6600, pourSprite, "pour", nil, MathHelper.PiOver2-0.37, -75 )
WorldBuilding.BuildObject( 3253, -6778, pourSprite, "pour", nil, MathHelper.PiOver2-0.37, -75 )
WorldBuilding.BuildObject( 3477, -6867, pourSprite, "pour", nil, MathHelper.PiOver2-0.37, -75 )

-- Left
WorldBuilding.BuildSection( 1, 10, 960, -4776, gravelSprite, 20, 20, nil, true )

-- Bottom
WorldBuilding.BuildSection( 19, 1, 960, -7016, gravelSprite, 20, 20, nil, true )

-- Filler
WorldBuilding.BuildSection( 1, 7, 1152, -5440, gravelSprite, 20, 20, nil, true )
WorldBuilding.BuildSection( 1, 6, 1368, -5716, gravelSprite, 20, 20, nil, true )
WorldBuilding.BuildSection( 1, 5, 1584, -5972, gravelSprite, 20, 20, nil, true )
WorldBuilding.BuildSection( 1, 4, 1800, -6260, gravelSprite, 20, 20, nil, true )
WorldBuilding.BuildSection( 1, 3, 2016, -6420, gravelSprite, 20, 20, nil, true )
WorldBuilding.BuildSection( 1, 2, 2232, -6580, gravelSprite, 20, 20, nil, true )
WorldBuilding.BuildSection( 3, 1, 2320, -6798, gravelSprite, 20, 20, nil, true )

-- Salt Ramp Top
WorldBuilding.BuildRamp( 11, 1824, -4992, dirtSprite, -0.65, 20 )
WorldBuilding.BuildSection( 7, 1, 3856, -6472, dirtSprite, 20, 20, nil, true )
WorldBuilding.BuildRamp( 4, 3856 + 236*7 - 128, -6536, dirtSprite, -0.65, 20 )

-- Right wall hook
WorldBuilding.BuildRamp( 1, 2496 - 64, -4084 - 128, dirtSprite, -0.60 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 2496 - 64 * 2.5, -4084 - 128 * 2, dirtSprite, -0.70 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 2496 - 64 * 4.5, -4084 - 128 * 3, dirtSprite, -0.80 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 2496 - 64 * 7, -4084 - 128 * 4, dirtSprite, -0.90 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 2496 - 64 * 9, -4084 - 128 * 5, dirtSprite, -0.70 - MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 2496 - 64 * 10.5, -4084 - 128 * 6 - 32, dirtSprite, -0.40 - MathHelper.PiOver2, 20 )

-- Left wall hook
WorldBuilding.BuildRamp( 1, 20, -3792, dirtSprite, 0.50 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 20 + 64, -3792 - 128, dirtSprite, 0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 20 + 64 * 2.5, -3792 - 128 * 2, dirtSprite, 0.70 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 20 + 64 * 4.5, -3792 - 128 * 3, dirtSprite, 0.80 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 20 + 64 * 6, -3792 - 128 * 4, dirtSprite, 0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 20 + 64 * 7, -3792 - 128 * 5, dirtSprite, 0.30 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildSection( 1, 5, 484, -3792 - 128 * 6 - 64, dirtSprite, 20, 20, nil, true )
WorldBuilding.BuildRamp( 1, 8 + 64 * 7, -3792 - 128 * 1 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.30 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 8 + 64 * 6, -3792 - 128 * 2 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 8 + 64 * 4.5, -3792 - 128 * 3 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.80 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 8 + 64 * 2.5, -3792 - 128 * 4 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.70 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 8 + 64, -3792 - 128 * 5 - 128 * 6 - 64 - 236 * 4, dirtSprite, -0.60 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 1, 8, -3792 - 128 * 5 - 128 * 7 - 64 - 236 * 4, dirtSprite, -0.50 + MathHelper.PiOver2, 20 )
WorldBuilding.BuildRamp( 6, 8, -3792 - 128 * 5 - 128 * 7 - 64 - 236 * 4, dirtSprite, -1.30, 20 )

-- "Low Road" Bottom
WorldBuilding.BuildSection( 24, 1, 484, -7528, dirtSprite, 20, 20, nil, true )

-- TODO: Add webs to salt ramp and "low road"
