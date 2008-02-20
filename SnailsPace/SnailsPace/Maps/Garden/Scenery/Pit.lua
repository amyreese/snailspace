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

pileImage = Image()
pileImage.filename = "Resources/Textures/saltpile"
pileImage.blocks = Vector2(1.0, 1.0)
pileImage.size = Vector2(280.0, 122.0)

pileSprite = Sprite()
pileSprite.image = pileImage
pileSprite.visible = true
pileSprite.effect = "Resources/Effects/effects"
pileSprite.animationStart = 0
pileSprite.animationEnd = 0
pileSprite.frame = 0
pileSprite.animationDelay = 0.0
pileSprite.timer = 0.0

dirtImage = Image()
dirtImage.filename = "Resources/Textures/dirt"
dirtImage.blocks = Vector2(1.0, 1.0)
dirtImage.size = Vector2(256.0, 128.0)

dirtSprite = Sprite()
dirtSprite.image = dirtImage
dirtSprite.visible = true
dirtSprite.effect = "Resources/Effects/effects"
dirtSprite.animationStart = 0
dirtSprite.animationEnd = 0
dirtSprite.frame = 0
dirtSprite.animationDelay = 0.0
dirtSprite.timer = 0.0

pourImage = Image()
pourImage.filename = "Resources/Textures/pouringsalt"
pourImage.blocks = Vector2(4.0, 1.0)
pourImage.size = Vector2(128.0, 900.0)

pourSprite = Sprite()
pourSprite.image = pourImage
pourSprite.visible = true
pourSprite.effect = "Resources/Effects/effects"
pourSprite.animationStart = 0
pourSprite.animationEnd = 3
pourSprite.frame = 0
pourSprite.animationDelay = 0.1
pourSprite.timer = 0.0





--Left Pit Wall
xOffset = 18
yOffset = -15.0
for x=0,9 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Left Pit Wall Fill 1
xOffset = 11
yOffset = -15.0
for x=0,9 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Left Pit Wall Fill 2
xOffset = 4
yOffset = -15.0
for x=0,9 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Floor
xOffset = 45
yOffset = -35.0
for x=0,40 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( x * 2 + xOffset ) * 32, ( -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Floor Secret Passage
xOffset = 28
yOffset = -35.0

for x=0,1 do
 dirtObj = GameObject()
 dirtObj.name = "fallingPlatform"
 if (x == 1) then
	dirtObj.sprites:Add("Pour", pourSprite)
	dirtObj.sprites["Pour"].position = Vector2(150, -300)
 end
 dirtObjSprite = dirtSprite:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImage.size.X - 32, dirtImage.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( x * 8.5 + xOffset ) * 32, ( -3.5 + yOffset ) * 32 )
 dirtObj.layer = 0.6
 map.objects:Add(dirtObj)
end

pourTrap2 = Traps.SaltPile( 1300, ( -6 + yOffset ) * 32, 64, 512, 0, 2 )





--Pit Floor SaltPile
pile1 = GameObject()
 pile1Sprite = pileSprite:clone()
 pile1Sprite.frame = 0
 pile1.sprites:Add("Pile", pile1Sprite)
 pile1.size = Vector2(pileImage.size.X - 32, gravelImage.size.Y - 192)
 pile1.rotation = 0.0
 pile1.position = Vector2(63*32, -33*32)
 pile1.layer = 0.6
 map.objects:Add(pile1)
 
 -- Set up the saltPile Trigger
--[[pileTrig = Trigger()
pileTrig.position = Vector2(69*32,-33*32)
pileTrig.bounds = GameObjectBounds( Vector2( 0,0 ), Vector2( 0,280 ), Vector2(140, 122))
pileTrig.state = {}
map.triggers:Add(pileTrig)

 saltPile Trigger function
function pileTrig.state:trigger( character, gameTime )
	character.takeDamage(1)
end]]--

--Pit Floor Salt
xOffset = 45
yOffset = -31.0
for x=0,37 do
 saltObj = Character()
 saltObjSprite = saltSprite:clone()
 saltObjSprite.frame = 0
 saltObj.sprites:Add("salt", saltObjSprite)
 saltObj.size = Vector2(saltImage.size.X - 32, saltImage.size.Y - 32)
 saltObj.rotation = 0.0
 saltObj.position = Vector2( ( x * 2 + xOffset ) * 32, ( -3.5 + yOffset ) * 32 )
 saltObj.layer = 0.6
 map.objects:Add(saltObj)
end


--Right Pit Wall
xOffset = 125
yOffset = -10.0
for x=0,6 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.0
 gravelObj.position = Vector2( ( 2 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end


--Pit Secret Ramp
xOffset = 22
yOffset = -45.0
for x=0,35 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Salt
xOffset = 27
yOffset = -45.0
for x=0,30 do
 saltObj = Character()
 saltObjSprite = saltSprite:clone()
 saltObjSprite.frame = 0
 saltObj.sprites:Add("salt", saltObjSprite)
 saltObj.size = Vector2(saltImage.size.X - 32, saltImage.size.Y - 32)
 saltObj.rotation = 2.35
 saltObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 saltObj.layer = 0.6
 map.objects:Add(saltObj)
end

--Pit Secret Ramp Fill 1
xOffset = 17
yOffset = -50.0
for x=0,34 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Fill 2
xOffset = 10
yOffset = -52.0
for x=0,33 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Ceiling
xOffset = 50
yOffset = -40.0
for x=0,27 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Ceiling Fill 1
xOffset = 62
yOffset = -41.0
for x=0,24 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Ceiling Fill 2
xOffset = 73
yOffset = -41.0
for x=0,21 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end

--Pit Secret Ramp Ceiling Fill 3
xOffset = 83
yOffset = -41.0
for x=0,18 do
 gravelObj = GameObject()
 gravelObjSprite = gravelSprite:clone()
 gravelObjSprite.frame = 0
 gravelObj.sprites:Add("gravel", gravelObjSprite)
 gravelObj.size = Vector2(gravelImage.size.X - 32, gravelImage.size.Y - 32)
 gravelObj.rotation = 0.8
 gravelObj.position = Vector2( ( x * 4 + xOffset ) * 32, ( x * -3.5 + yOffset ) * 32 )
 gravelObj.layer = 0.6
 map.objects:Add(gravelObj)
end