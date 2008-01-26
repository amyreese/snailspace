
--[[ 
	FireAnt.lua
	Define the Fire Ant's properties and behaviors.
]]--

-- Generic FireAntImage object to be reused by all Fire Ants
FireAntImage = Image()
FireAntImage.filename = "Resources/Textures/FireAntTable"
FireAntImage.blocks = Vector2(4, 4)
FireAntImage.size = Vector2(128, 128)

-- Creates a Bee object
function FireAnt()
	fireant = {}
	
	body = Sprite()
	body.image = FireAntImage
	body.effect = "Resources/Effects/effects"
	body.visible = true
	body.animationStart = 0
	body.animationEnd = 3
	body.animationDelay = 1.0 / 15.0
	body.frame = 0
	body.timer = 0
	
	char = Character()
	char.sprites:Add("Body", body)
	char.size = FireAntImage.size
	char.position = Vector2(0,0)
	char.velocity = Vector2(0,0)
	char.maxVelocity = 10
	char.thinker = "FireAntThinker"
	map.characters:Add(char)
	
	fireant.body = body
	fireant.character = char
	
	return fireant
end

-- Fire Ant behavior function
function FireAntThinker( self, gameTime )
	aVelocity = Vector2(helix.position.X - self.position.X, helix.position.Y - self.position.Y)
	self.velocity = aVelocity
	
	-- TODO: Extend AI for the Fire Ant
end