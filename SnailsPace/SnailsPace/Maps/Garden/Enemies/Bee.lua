
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
function Bee(startPos, behav)
	behav = behav or "flyUp"
	fly = BeeSprite(0, 3, .07)
	hover = BeeSprite(0, 3, .035)
	
	bee = Character("stinger")
	bee.sprites:Add("Fly", fly)
	bee.sprites:Add("Hover", hover) 
	bee.size = BeeImage.size
	bee.startPosition = startPos
	bee.position = startPos
	bee.maxVelocity = 768
	bee.thinker = "BeeThinker"
	bee.state = {}
	bee.name = "Bee"
	bee.health = 3
	bee.affectedByGravity = true
	bee:setSprite("Hover")
	bee.behavior = behav
	map.characters:Add(bee)

	return bee
end

-- Bee behavior function
function BeeThinker( self, gameTime )
	if (self.behavior == "flyUp") then
		AI.vertPatrol(self, self.startPosition.Y + 50, self.startPosition.Y - 50)
	elseif (self.behavior == "flyUpRight") then
		AI.diagonalPatrol(self, Vector2( self.startPosition.X + 75, self.startPosition.Y + 100), Vector2( self.startPosition.X, self.startPosition.Y - 50 ))
	elseif (self.behavior == "flyUpLeft") then
		AI.diagonalPatrol(self, Vector2( self.startPosition.X - 75, self.startPosition.Y + 100), Vector2( self.startPosition.X, self.startPosition.Y - 50 ))
	elseif (self.behavior == "flyDownRight") then
		AI.diagonalPatrol(self, Vector2( self.startPosition.X + 75, self.startPosition.Y + 50), Vector2( self.startPosition.X, self.startPosition.Y - 100 ))
	elseif (self.behavior == "flyDownLeft") then
		AI.diagonalPatrol(self, Vector2( self.startPosition.X - 75, self.startPosition.Y + 50), Vector2( self.startPosition.X, self.startPosition.Y - 100 ))
	end	
	if self.state.attacking then
		if AI.canSeeHelix( self, 600 ) then
			AI.shootDirectlyAtHelix( self, gameTime )
		end
	end
	-- TODO: Create AI for the Bee
end