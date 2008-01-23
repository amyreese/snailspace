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

xOffset = 130
yOffset = -4.5
grassLength = 40
for x=0,5 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.sprites:Add("Grass", grassObjSprite)
 grassObj.position = Vector2( x * 6 + xOffset, yOffset )
 grassObj.layer = 0.5
 grassObj.size = grassImage.size
 map.objects:Add(grassObj)
end

xOffset = 166
yOffset = -6.5
grassLength = 40
for x=0,5 do
 grassObj = GameObject()
 grassObjSprite = grassSprite:clone()
 grassObjSprite.frame = 0
 grassObj.sprites:Add("Grass", grassObjSprite)
 grassObj.position = Vector2( x * 6 + xOffset,x * -1 + yOffset )
 grassObj.layer = 0.5
 grassObj.size = grassImage.size
 map.objects:Add(grassObj)
end