
--[[ 
	Bee.lua
	Define the Bee's properties and behaviors.
]]--

library('AI')

-- Generic BeeImage object to be reused by all Bees
BeeImage = Image()
BeeImage.filename = "Resources/Textures/BeeTable"
BeeImage.blocks = Vector2(4, 4)
BeeImage.size = Vector2(64, 64)

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
	die = BeeSprite(8, 11, .25)
	
	bee = Character("stinger")
	bee.sprites:Add("Fly", fly)
	bee.sprites:Add("Hover", hover)
	bee.sprites:Add("Die", die)
	bee.size = BeeImage.size
	bee.startPosition = startPos
	bee.position = startPos
	bee.maxVelocity = 768
	bee.thinker = "BeeThinker"
	bee.state = {
		attacking = true
	}
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
	elseif (self.behavior == "swarmHelixA") then
		AI.diagonalPatrol(self, Vector2( Player.helix.position.X - 75, Player.helix.position.Y + 196), Vector2( Player.helix.position.X + 75, Player.helix.position.Y + 128 ))
	elseif (self.behavior == "swarmHelixB") then
		AI.diagonalPatrol(self, Vector2( Player.helix.position.X + 75, Player.helix.position.Y + 196), Vector2( Player.helix.position.X - 75, Player.helix.position.Y + 128 ))
	elseif (self.behavior == "flyLeftRight") then
		AI.diagonalPatrol(self, Vector2( self.startPosition.X + 100, self.startPosition.Y), Vector2( self.startPosition.X, self.startPosition.Y))
	end	
	if self.state.attacking then
		if AI.canSeeHelix( self, 600 ) then
			AI.shootDirectlyAtHelix( self, gameTime )
		end
	end
end