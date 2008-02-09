
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
function Bee(startPos)
	fly = BeeSprite(0, 3, .07)
	hover = BeeSprite(0, 3, .035)
	
	bee = Character()
	bee.sprites:Add("Fly", fly)
	bee.sprites:Add("Hover", hover) 
	bee.size = BeeImage.size
	bee.startPosition = startPos
	bee.position = startPos
	bee.direction = Vector2(0,1)
	bee.maxVelocity = 1280
	bee.thinker = "BeeThinker"
	bee.state = {}
	bee.name = "Bee"
	bee.health = 3
	bee.affectedByGravity = true
	bee:setSprite("Hover")
	map.characters:Add(bee)

	return bee
end

-- Bee behavior function
function BeeThinker( self, gameTime )
	AI.vertPatrol(self, self.startPosition.Y + 50, self.startPosition.Y - 50)
	
	-- TODO: Create AI for the Bee
end