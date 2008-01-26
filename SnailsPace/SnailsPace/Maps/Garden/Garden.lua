

print "Map initialization"

image = Image()
image.filename = "Resources/Textures/HelixTable"
image.blocks = Vector2(4.0, 4.0)
image.size = Vector2(128.0, 128.0)

sprite = Sprite()
sprite.image = image
sprite.visible = true
sprite.effect = "Resources/Effects/effects"
sprite.animationStart = 0
sprite.animationEnd = 11
sprite.frame = 0
sprite.animationDelay = 1.0 / 15.0
sprite.timer = 0.0

go = Character()
go.sprites:Add("Snail", sprite)
go.velocity = Vector2(3.0, 2.0)
go.layer = 1
go.size = image.size;
function snail_think( self, gameTime )
	print("thinking")
end
go.thinker = "snail_think"
map.characters:Add(go)

bkgImage = Image()
bkgImage.filename = "Resources/Textures/Garden"
bkgImage.blocks = Vector2(16.0, 12.0)
bkgImage.size = Vector2(512.0, 512.0)

bkgSprite = Sprite()
bkgSprite.image = bkgImage
bkgSprite.visible = true
bkgSprite.effect = "Resources/Effects/effects"
bkgSprite.animationStart = 0
bkgSprite.animationEnd = 0
bkgSprite.frame = 0
bkgSprite.animationDelay = 0.0
bkgSprite.timer = 0.0

xOffset = -40
yOffset = -20
for x=0,bkgImage.blocks.X - 1 do
 for y=0,bkgImage.blocks.Y - 1 do
  bkgObj = GameObject()
  bkgObjSprite = bkgSprite:clone()
  bkgObjSprite.animationStart = x + bkgImage.blocks.X * ( bkgImage.blocks.Y - 1 - y )
  bkgObjSprite.animationEnd = bkgObjSprite.animationStart
  bkgObjSprite.frame = bkgObjSprite.animationStart
  bkgObj.sprites:Add("Background", bkgObjSprite)
  bkgObj.position = Vector2( x * 16 + xOffset, y * 16 + yOffset )
  bkgObj.layer = 50
  bkgObj.collidable = false
  map.objects:Add(bkgObj)
 end
end

map.objects:Add(go)

img = Image()
img.filename = "Resources/Textures/BlackAntTable"
img.blocks = Vector2(4.0, 4.0)
img.size = Vector2(128.0, 128.0)

sprt = Sprite()
sprt.image = img
sprt.visible = true
sprt.effect = "Resources/Effects/effects"
sprt.animationStart = 0
sprt.animationEnd = 3
sprt.frame = 0
sprt.animationDelay = 1.0 / 15.0
sprt.timer = 0.0
sprt.horizontalFlip = true

gmob = GameObject()
gmob.sprites:Add("Ant", sprt)
gmob.velocity = Vector2(3.0, 2.0)
gmob.position = Vector2(10.0, 0.0)
gmob.layer = 0.1
gmob.size = img.size

map.objects:Add(gmob)

rdimg = Image()
rdimg.filename = "Resources/Textures/FireAntTable"
rdimg.blocks = Vector2(4.0, 4.0)
rdimg.size = Vector2(128.0, 128.0)

rdspt = Sprite()
rdspt.image = img
rdspt.visible = true
rdspt.effect = "Resources/Effects/effects"
rdspt.animationStart = 0
rdspt.animationEnd = 3
rdspt.frame = 0
rdspt.animationDelay = 1.0 / 15.0
rdspt.timer = 0.0
rdspt.horizontalFlip = true

rdobj = GameObject()
rdobj.sprites:Add("Ant", sprt)
rdobj.velocity = Vector2(3.0, 2.0)
rdobj.position = Vector2(30.0, 0.0)
rdobj.layer = 0.1
rdobj.size = rdimg.size

map.objects:Add(rdobj);

dofile("Maps/Garden/Platforms.lua")
dofile("Maps/Garden/Plane1.lua")
dofile("Maps/Garden/Plane2.lua")
dofile("Maps/Garden/RedAnt.lua")