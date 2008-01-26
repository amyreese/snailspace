

red = Image()
red.filename = "Resources/Textures/FireAntTable"
red.blocks = Vector2(4.0, 4.0)
red.size = Vector2(128.0, 128.0)

spri = Sprite()
spri.image = red
spri.visible = true
spri.effect = "Resources/Effects/effects"
spri.animationStart = 0
spri.animationEnd = 3
spri.frame = 0
spri.animationDelay = 1.0 / 15.0
spri.timer = 0.0

fireAnt = Character()
fireAnt.sprites:Add("FireAnt", spri)
fireAnt.velocity = Vector2(1.0, 0.0)
fireAnt.layer = 0.1
fireAnt.maxVelocity = 10.0
fireAnt.size = image.size;
function fireAnt_think( self, gameTime )
	aVelocity = Vector2(helix.position.X - self.position.X, helix.position.Y - self.position.Y)
	self.velocity = aVelocity
	print("angry!")
end
fireAnt.thinker = "fireAnt_think"
map.characters:Add(fireAnt)

