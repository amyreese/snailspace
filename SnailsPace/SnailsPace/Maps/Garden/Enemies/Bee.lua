
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

-- Creates a Sprite for a Bee
function BeeSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = BeeImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end
	

-- Creates a Bee object
function Bee()
	fly = BeeSprite(0, 3, .07)
	hover = BeeSprite(0, 3, .035)
	
	bee = Character()
	bee.sprites:Add("Fly", fly)
	bee.sprites:Add("Hover", hover) 
	bee.size = BeeImage.size
	bee.position = Vector2(0,0)
	bee.velocity = Vector2(0,0)
	bee.maxVelocity = 960
	bee.thinker = "BeeThinker"
	bee.state = {}
	bee:setSprite("Hover")
	map.characters:Add(bee)

	return bee
end

-- Bee behavior function
function BeeThinker( self, gameTime )
	if ( AI.canSeeHelix( self, 640 ) ) then 
		AI.moveToHelix( self, 384.0, 256.0 )
		self:setSprite("Fly")
	else
		AI.stop( self )
		self:setSprite("Hover")
	end
	
	-- TODO: Create AI for the Bee
end