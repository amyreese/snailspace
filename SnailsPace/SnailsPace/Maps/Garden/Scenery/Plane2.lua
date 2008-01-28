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

-- Open Air Dirt
xOffset = 130
yOffset = -4.5
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.size = dirtImage.size
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Entrance Ceiling
xOffset = 160
yOffset = 18
dirtLength = 40
for x=0,4 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, ( yOffset ) *32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Right
xOffset = 177
yOffset = 11.0
dirtLength = 40
for x=0,12 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Platform 1
xOffset = 166
yOffset = -30
dirtLength = 40
for x=0,1 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Left
xOffset = 140
yOffset = -10.0
dirtLength = 40
for x=0,23 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 1
xOffset = 158
yOffset = -30.0
dirtLength = 40
for x=0,13 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Platform 2
xOffset = 147
yOffset = -95.0
dirtLength = 40
for x=0,1 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Platform 2 Ceiling
xOffset = 165
yOffset = -75
dirtLength = 40
for x=0,2 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Right 2
xOffset = 177
yOffset = -80.0
dirtLength = 40
for x=0,15 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 2
xOffset = 158
yOffset = -95.0
dirtLength = 40
for x=0,16 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Floor Ceiling
xOffset = 179
yOffset = -140.0
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Floor
xOffset = 160
yOffset = -158
dirtLength = 40
for x=0,6 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Boss platform fill
xOffset = 245
yOffset = -13
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Boss Platform
xOffset = 245
yOffset = -6
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = dirtImage.size
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end