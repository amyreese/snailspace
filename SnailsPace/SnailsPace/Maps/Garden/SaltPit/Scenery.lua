gravelImage = Image()
gravelImage.filename = "Resources/Textures/gravel"
gravelImage.blocks = Vector2(1.0, 1.0)
gravelImage.size = Vector2(256.0, 256.0)

gravelSprite = Sprite()
gravelSprite.image = gravelImage
gravelSprite.visible = true
gravelSprite.effect = "Resources/Effects/effects"
gravelSprite.animationStart = 0
gravelSprite.animationEnd = 0
gravelSprite.frame = 0
gravelSprite.animationDelay = 0.0
gravelSprite.timer = 0.0

saltImage = Image()
saltImage.filename = "Resources/Textures/salt"
saltImage.blocks = Vector2(1.0, 1.0)
saltImage.size = Vector2(256.0, 64.0)

saltSprite = Sprite()
saltSprite.image = saltImage
saltSprite.visible = true
saltSprite.effect = "Resources/Effects/effects"
saltSprite.animationStart = 0
saltSprite.animationEnd = 0
saltSprite.frame = 0
saltSprite.animationDelay = 0.0
saltSprite.timer = 0.0

dirtImage = Image()
dirtImage.filename = "Resources/Textures/dirt"
dirtImage.blocks = Vector2(1.0, 1.0)
dirtImage.size = Vector2(256.0, 256.0)

dirtSprite = Sprite()
dirtSprite.image = dirtImage
dirtSprite.visible = true
dirtSprite.effect = "Resources/Effects/effects"
dirtSprite.animationStart = 0
dirtSprite.animationEnd = 0
dirtSprite.frame = 0
dirtSprite.animationDelay = 0.0
dirtSprite.timer = 0.0

dirtImageS = Image()
dirtImageS.filename = "Resources/Textures/dirt"
dirtImageS.blocks = Vector2(1.0, 1.0)
dirtImageS.size = Vector2(128.0, 128.0)

dirtSpriteS = Sprite()
dirtSpriteS.image = dirtImageS
dirtSpriteS.visible = true
dirtSpriteS.effect = "Resources/Effects/effects"
dirtSpriteS.animationStart = 0
dirtSpriteS.animationEnd = 0
dirtSpriteS.frame = 0
dirtSpriteS.animationDelay = 0.0
dirtSpriteS.timer = 0.0

saltcanImage = Image()
saltcanImage.filename = "Resources/Textures/saltcan"
saltcanImage.blocks = Vector2(1.0, 1.0)
saltcanImage.size = Vector2(150.0, 345.0)

saltcanSprite = Sprite()
saltcanSprite.image = saltcanImage
saltcanSprite.visible = true
saltcanSprite.effect = "Resources/Effects/effects"
saltcanSprite.animationStart = 0
saltcanSprite.animationEnd = 0
saltcanSprite.frame = 0
saltcanSprite.animationDelay = 0.0
saltcanSprite.timer = 0.0

pourImage = Image()
pourImage.filename = "Resources/Textures/pouringsalt"
pourImage.blocks = Vector2(4.0, 1.0)
pourImage.size = Vector2(128.0, 512.0)

pourSprite = Sprite()
pourSprite.image = pourImage
pourSprite.visible = true
pourSprite.effect = "Resources/Effects/effects"
pourSprite.animationStart = 0
pourSprite.animationEnd = 3
pourSprite.frame = 0
pourSprite.animationDelay = 0.1
pourSprite.timer = 0.0


xOffset = 0
yOffset = -768
defaultLayer = 5

function BuildPlatform(platformWidth, platformHeight, platformXOffset, platformYOffset, platformSprite, platformXOverlap, platformYOverlap, layer)
	for x = 0, platformWidth - 1 do
		for y = 0, platformHeight - 1 do
			object = GameObject()
			sprite = platformSprite:clone()
			
			object.sprites:Add("background", sprite)
			object.size = sprite.image.size
			object.position = Vector2( x * ( sprite.image.size.X - platformXOverlap ) + platformXOffset + xOffset, -y * ( sprite.image.size.Y - platformYOverlap ) + platformYOffset + yOffset )
			object.layer = layer
			object.collidable = true
		
			map.objects:Add(object)
		end
	end
end

-- Left edge
BuildPlatform( 1, 20, 0, 0, dirtSprite, 20, 20, defaultLayer )

-- Right edge
BuildPlatform( 1, 20, 4096, 0, dirtSprite, 20, 20, defaultLayer )

-- Salt Can
saltcan = GameObject()
saltcanSprite.frame = 0
saltcan.sprites:Add("Can", saltcanSprite)
saltcan.size = Vector2(saltcanImage.size.X, saltcanImage.size.Y)
saltcan.rotation = 1.57
saltcan.position = Vector2( xOffset + 2048, yOffset - saltcanImage.size.Y / 2 )
saltcan.layer = defaultLayer - 2
saltcan.collidable = true;
map.objects:Add(saltcan)

-- Salt platforms
--Platform 1
BuildPlatform( 5, 1, 1984, -310, dirtSpriteS, 10, 10, defaultLayer )

-- Salt for platform 1
pour1 = Character()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1896, yOffset - pourImage.size.Y + 32 )
pour1.layer = defaultLayer + 1
map.objects:Add(pour1)
pour1 = Character()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 1896, yOffset - 1.5 * pourImage.size.Y + 32 )
pour1.layer = defaultLayer + 2
map.objects:Add(pour1)

-- Platform 2
BuildPlatform( 5, 1, 1528, -1024, dirtSpriteS, 10, 10, defaultLayer )

-- Salt for platform 2
pour1 = Character()
pour1Sprite = pourSprite:clone()
pour1Sprite.frame = 0
pour1.sprites:Add("Pour", pour1Sprite)
pour1.size = Vector2(pourImage.size.X - 75, pourImage.size.Y)
pour1.rotation = 0
pour1.position = Vector2( xOffset + 2168, yOffset - 1024 - pourImage.size.Y + 32 )
pour1.layer = defaultLayer + 1
map.objects:Add(pour1)

BuildPlatform( 7, 1, 1472, -2048, dirtSpriteS, 10, 10, defaultLayer )
BuildPlatform( 3, 1, 2176, -3060, dirtSpriteS, 10, 10, defaultLayer )
BuildPlatform( 4, 1, 1728, -3828, dirtSpriteS, 10, 10, defaultLayer )
BuildPlatform( 5, 1, 2432, -4084, dirtSpriteS, 10, 10, defaultLayer )

--Salt Ramp
rampXOffset = 2048
rampYOffset = -5120
for x=0,5 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X, gravelImage.size.Y)
 gravelObj.rotation = 0.64
 gravelObj.position = Vector2( x * 128 + xOffset + rampXOffset, x * -164 + yOffset + rampYOffset )
 gravelObj.layer = defaultLayer
 map.objects:Add(gravelObj)
end
rampXOffset = 2688
rampYOffset = -5940
for x=0,5 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X, gravelImage.size.Y)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( x * 128 + xOffset + rampXOffset, x * -112 + yOffset + rampYOffset )
 gravelObj.layer = defaultLayer
 map.objects:Add(gravelObj)
end
rampXOffset = 3328
rampYOffset = -6500
for x=0,5 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X, gravelImage.size.Y)
 gravelObj.rotation = 1.0
 gravelObj.position = Vector2( x * 136 + xOffset + rampXOffset, x * -95 + yOffset + rampYOffset )
 gravelObj.layer = defaultLayer
 map.objects:Add(gravelObj)
end

BuildPlatform( 1, 10, 1984, -4776, gravelSprite, 20, 20, defaultLayer )
BuildPlatform( 14, 1, 1984, -7016, gravelSprite, 20, 20, defaultLayer )