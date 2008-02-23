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

dirtImage2 = Image()
dirtImage2.filename = "Resources/Textures/dirt"
dirtImage2.blocks = Vector2(1.0, 1.0)
dirtImage2.size = Vector2(256.0, 128.0)

dirtSprite2 = Sprite()
dirtSprite2.image = dirtImage2
dirtSprite2.visible = true
dirtSprite2.effect = "Resources/Effects/effects"
dirtSprite2.animationStart = 0
dirtSprite2.animationEnd = 0
dirtSprite2.frame = 0
dirtSprite2.animationDelay = 0.0
dirtSprite2.timer = 0.0

grassImage = Image()
grassImage.filename = "Resources/Textures/Grass"
grassImage.blocks = Vector2(1.0, 1.0)
grassImage.size = Vector2(256.0, 256.0)

grassSprite = Sprite()
grassSprite.image = grassImage
grassSprite.visible = true
grassSprite.effect = "Resources/Effects/effects"
grassSprite.animationStart = 0
grassSprite.animationEnd = 0
grassSprite.frame = 0
grassSprite.animationDelay = 0.0
grassSprite.timer = 0.0

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

-- Open Air Grass
xOffset = 130
yOffset = -4.5
for x=0,5 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 grassObj.sprites:Add("grass", grassObjSprite)
 grassObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 grassObj.layer = 0.5
 map.objects:Add(grassObj)
end

-- Open Air Dirt
xOffset = 130
yOffset = -8.5
for x=0,5 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite2:clone()
 dirtObjSprite.frame = 0
 dirtObj.size = Vector2(dirtImage2.size.X - 32, dirtImage2.size.Y - 32)
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
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, ( yOffset ) *32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Right 1
xOffset = 177
yOffset = 11.0
dirtLength = 40
for x=0,12 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Right 1 Fill
xOffset = 183
yOffset = 11.0
dirtLength = 40
for x=0,12 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
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
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Left
xOffset = 140
yOffset = -14.0
dirtLength = 40
for x=0,22 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Left Fill 1
xOffset = 134
yOffset = -14.0
dirtLength = 40
for x=0,22 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Left Fill 2
xOffset = 128.0
yOffset = -14.0
dirtLength = 40
for x=0,22 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
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
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 1 Fill 1
xOffset = 164
yOffset = -30.0
dirtLength = 40
for x=0,13 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 1 Fill 2
xOffset = 170
yOffset = -30.0
dirtLength = 40
for x=0,13 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 1 Fill 3
xOffset = 176
yOffset = -30.0
dirtLength = 40
for x=0,13 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 1 Fill 4
xOffset = 182
yOffset = -30.0
dirtLength = 40
for x=0,13 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Middle 1 Fill 5
xOffset = 188
yOffset = -30.0
dirtLength = 40
for x=0,13 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Platform 2
xOffset = 147
yOffset = -95.0
gravelLength = 40
for x=0,1 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Drop Platform 2 Ceiling
xOffset = 165
yOffset = -75
dirtLength = 40
for x=0,8 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Drop Right 2
xOffset = 177
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Drop Right 2 Fill 1
xOffset = 183
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Drop Right 2 Fill 2
xOffset = 189
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Drop Middle 2
xOffset = 158
yOffset = -95.0
gravelLength = 40
for x=0,16 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 if ( x > 12 ) then 
		gravelObj.collidable = false
 end
 map.objects:Add(gravelObj)
end

--Cave Drop Middle 2 Fill 1
xOffset = 152
yOffset = -95.0
gravelLength = 40
for x=0,2 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Floor Ceiling
xOffset = 179
yOffset = -135.0
gravelLength = 40
for x=0,10 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Floor Ceiling Fill 1
xOffset = 179
yOffset = -129.0
gravelLength = 40
for x=0,8 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Floor Ceiling Fill 2
xOffset = 179
yOffset = -124.0
gravelLength = 40
for x=0,6 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Floor
xOffset = 156
yOffset = -158
gravelLength = 40
for x=0,24 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Left 1
xOffset = 223
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Left 1 Fill 1
xOffset = 218
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Left 1 Fill 2
xOffset = 213
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Left 1 Fill 3
xOffset = 208
yOffset = -80.0
gravelLength = 40
for x=0,15 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Right 1
xOffset = 258
yOffset = -80.0
for x=0,22 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end


--Cave Escape Right 1 Fill 1
xOffset = 263
yOffset = -80.0
gravelLength = 40
for x=0,22 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Right 1 Fill 2
xOffset = 268
yOffset = -80.0
gravelLength = 40
for x=0,22 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Right 1 Fill 3
xOffset = 273
yOffset = -80.0
gravelLength = 40
for x=0,22 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Cave Escape Left 2
xOffset = 243
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Left 2 Fill 1
xOffset = 238
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Left 2 Fill 2
xOffset = 233
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Left 2 Fill 3
xOffset = 228
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Right 2
xOffset = 278
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Right 2 Fill 1
xOffset = 283
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end


--Cave Escape Right 2 Fill 2
xOffset = 288
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end


--Cave Escape Right 2 Fill 3
xOffset = 293
yOffset = 0.0
dirtLength = 40
for x=0,21 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( -x + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end


--Cave Escape Bounds
xOffset = 242.0
yOffset = 75.0
dirtLength = 40
for x=0,20 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Bounds Fill 1
xOffset = 236.0
yOffset = 75.0
dirtLength = 40
for x=0,20 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Cave Escape Bounds Fill 2
xOffset = 230.0
yOffset = 75.0
dirtLength = 40
for x=0,20 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.5
 map.objects:Add(dirtObj)
end

--Boss platform fill
xOffset = 278
yOffset = 6.0
dirtLength = 40
for x=0,5 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.sprites:Add("grass", grassObjSprite)
 grassObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 grassObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 grassObj.layer = 0.5
 map.objects:Add(grassObj)
end

--Boss Platform
xOffset = 278
yOffset = 0.0
grassLength = 40
for x=0,5 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.sprites:Add("grass", grassObjSprite)
 grassObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 grassObj.position = Vector2( ( x * 6 + xOffset ) * 32, yOffset * 32 )
 grassObj.layer = 0.5
 map.objects:Add(grassObj)
end