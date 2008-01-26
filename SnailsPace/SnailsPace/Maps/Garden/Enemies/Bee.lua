
--[[ 
	Bee.lua
	Define the Bee's properties and behaviors.
]]--

library('AI')

-- Generic BeeImage object to be reused by all Bees
BeeImage = Image()
BeeImage.filename = "Resources/Textures/BeeTable"
BeeImage.blocks = Vector2(4, 4)
BeeImage.size = Vector2(128, 128)

-- Creates a Bee object
function Bee()
	body = Sprite()
	body.image = BeeImage
	body.effect = "Resources/Effects/effects"
	body.visible = true
	body.animationStart = 0
	body.animationEnd = 0
	body.animationDelay = 1.0 / 15.0
	body.frame = 0
	body.timer = 0
	
	bee = Character()
	bee.sprites:Add("Body", body)
	bee.size = BeeImage.size
	bee.position = Vector2(0,0)
	bee.velocity = Vector2(0,0)
	bee.maxVelocity = 960
	bee.thinker = "BeeThinker"
	bee.state = {}
	map.characters:Add(bee)

	return bee
end

-- Bee behavior function
function BeeThinker( self, gameTime )
	if ( AI.canSeeHelix( self, 640 ) ) then 
		AI.moveToHelix( self, 384.0, 256.0 )
	else
		AI.stop( self )
	end
	
	-- TODO: Create AI for the Bee
end