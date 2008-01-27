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

xOffset = 130
yOffset = -4.5
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 135
yOffset = -11.5
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 140
yOffset = -18.5
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 163
yOffset = -6.0
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.rotation = 0.8
 dirtObj.position = Vector2( ( x * 2 + xOffset ) * 32, ( x * -2.0 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 163
yOffset = 13
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 167
yOffset = 11.0
dirtLength = 40
for x=0,8 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.rotation = 0.8
 dirtObj.position = Vector2( ( x * 2 + xOffset ) * 32, ( x * -2.0 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 170
yOffset = -20
dirtLength = 40
for x=0,20 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 185
yOffset = -6
dirtLength = 40
for x=0,6 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 180
yOffset = 1
dirtLength = 40
for x=0,7 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 175
yOffset = 8
dirtLength = 40
for x=0,8 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, ( yOffset ) *32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 245
yOffset = -13
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end

xOffset = 245
yOffset = -6
dirtLength = 40
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 dirtObj.size = dirtImage.size
 map.objects:Add(dirtObj)
end