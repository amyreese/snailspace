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

--Left Pit Wall
xOffset = 20
yOffset = -13.0
for x=0,9 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Pit Floor
xOffset = 45
yOffset = -35.0
for x=0,45 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x * 2 + xOffset ) * 32, ( -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Right Pit Wall
xOffset = 125
yOffset = -10.0
for x=0,6 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end


--Pit Secret Ramp
xOffset = 25
yOffset = -45.0
for x=0,32 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.5
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Ceiling
xOffset = 60
yOffset = -40.0
for x=0,25 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(grassImage.size.X - 32, grassImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.4
 map.objects:Add(gravelObj)
end