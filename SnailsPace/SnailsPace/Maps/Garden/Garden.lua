
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
sprite.animationEnd = 15
sprite.frame = 0
sprite.animationDelay = 1.0 / 15.0
sprite.timer = 0.0

go = GameObject()
go.sprites:Add("Snail", sprite)
go.velocity = Vector2(3.0, 2.0)

map.objects:Add(go)
