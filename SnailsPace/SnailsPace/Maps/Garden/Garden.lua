
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

img = Image()
img.filename = "Resources/Textures/BlackAnt_1"
img.blocks = Vector2(1.0, 1.0)
img.size = Vector2(500.0, 500.0)

sprt = Sprite()
sprt.image = img
sprt.visible = true
sprt.effect = "Resources/Effects/effects"
sprt.animationStart = 0
sprt.animationEnd = 0
sprt.frame = 0

gmob = GameObject()
gmob.sprites:Add("Ant", sprt)
gmob.velocity = Vector2(3.0, 2.0)

map.objects:Add(gmob)