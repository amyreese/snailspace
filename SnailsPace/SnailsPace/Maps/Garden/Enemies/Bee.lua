
--[[ 
	Bee.lua
	Define the Bee's properties and behaviors.
]]--

-- Generic BeeImage object to be reused by all Bees
BeeImage = Image()
BeeImage.filename = "Resources/Textures/BeeTable"
BeeImage.blocks = Vector2(4, 4)
BeeImage.size = Vector2(128, 128)

-- Creates a Bee object
function Bee()
	bee = {}
	
	body = Sprite()
	body.image = BeeImage
	body.effect = "Resources/Effects/effects"
	body.visible = true
	body.animationStart = 0
	body.animationEnd = 0
	body.animationDelay = 1.0 / 15.0
	body.frame = 0
	body.timer = 0
	
	char = Character()
	char.sprites:Add("Body", body)
	char.size = BeeImage.size
	char.position = Vector2(0,0)
	char.velocity = Vector2(0,0)
	char.maxVelocity = 15
	char.thinker = "BeeThinker"
	map.characters:Add(char)

	bee.body = body
	bee.character = char
	
	return bee
end

-- Bee behavior function
function BeeThinker( self, gameTime )
	-- TODO: Create AI for the Bee
end