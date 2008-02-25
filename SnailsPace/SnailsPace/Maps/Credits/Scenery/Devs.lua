
xOffset = 900
yOffset = 300

sign = GameObject()

ssprite = savepointSprite:clone()
ssprite.frame = 2
ssprite.animationStart = 2
ssprite.animationEnd = 2
sign.sprites:Add("sign", ssprite)

sprite = Sprite()
sprite.image = Image()
sprite.image.filename = "Resources/Textures/GalleryTable"
sprite.image.blocks = Vector2(3,10)
sprite.image.size = Vector2(170,100)

sprite.position = Vector2(0,0)
sprite.effect = "Resources/Effects/effects"
sprite.layerOffset = -0.1
sprite.visible = true
sprite.frame = 0

sign.sprites:Add("title", sprite)
sign.collidable = false
sign.position = Vector2(xOffset-100,10)
map.objects:Add(sign)


Name( Vector2( xOffset + 0*(256+64), yOffset ), "joe" )
Name( Vector2( xOffset + 1*(256+64), yOffset ), "pat" )
Name( Vector2( xOffset + 2*(256+64), yOffset ), "josh" )
Name( Vector2( xOffset + 3*(256+64), yOffset ), "john" )
Name( Vector2( xOffset + 4*(256+64), yOffset ), "brian" )
Name( Vector2( xOffset + 5*(256+64), yOffset ), "simon" )