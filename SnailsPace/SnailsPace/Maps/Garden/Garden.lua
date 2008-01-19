
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

go = GameObject()
go.sprites:Add("Snail", sprite)
go.velocity = Vector2(3.0, 2.0)
go.layer = 1
map.objects:Add(go)

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
  map.objects:Add(bkgObj)
 end
end

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

xOffset = -40
yOffset = -4.5
grassLength = 40
for x=0,20 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.sprites:Add("Grass", grassObjSprite)
 grassObj.position = Vector2( x * 6 + xOffset, yOffset )
 grassObj.layer = 0.5
 map.objects:Add(grassObj)
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

map.objects:Add(gmob)
