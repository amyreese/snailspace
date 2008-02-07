--Grass Plane
xOffset = -40
yOffset = -6
grassLength = 40
for x=0,10 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.sprites:Add("Grass", grassObjSprite)
 grassObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 grassObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 grassObj.layer = 0.5
 map.objects:Add(grassObj)
end

--Dirt Fill 1
xOffset = -40
yOffset = -10
dirtLength = 40
for x=0,10 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSpriteS:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("Dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Dirt Fill 2
xOffset = -40
yOffset = -13
dirtLength = 40
for x=0,10 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("Dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Dirt Fill 3
xOffset = -40
yOffset = -16
dirtLength = 40
for x=0,10 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("Dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Dirt Fill 4
xOffset = -40
yOffset = -19
dirtLength = 40
for x=0,10 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("Dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end