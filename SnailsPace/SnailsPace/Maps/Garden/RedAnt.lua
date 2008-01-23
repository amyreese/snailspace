

red = Image()
red.filename = "Resources/Textures/FireAntTable"
red.blocks = Vector2(4.0, 4.0)
red.size = Vector2(128.0, 128.0)

sprite = Sprite()
sprite.image = red
sprite.visible = true
sprite.effect = "Resources/Effects/effects"
sprite.animationStart = 0
sprite.animationEnd = 3
sprite.frame = 0
sprite.animationDelay = 1.0 / 15.0
sprite.timer = 0.0

fireAnt = Character()
fireAnt.sprites:Add("FireAnt", sprite)
fireAnt.velocity = Vector2(3.0, 2.0)
fireAnt.layer = 1
fireAnt.size = image.size;
function fireAnt_think( self, gameTime )
	print("thinking")
end
go.think_func = "fireAnt_think"
map.characters:Add(fireAnt)
