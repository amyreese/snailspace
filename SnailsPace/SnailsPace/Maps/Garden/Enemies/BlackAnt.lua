
--[[ 
	BlackAnt.lua
	Define the Black Ant's properties and behaviors.
]]--

library('AI')

-- Generic BlackAntImage object to be reused by all Black Ants
BlackAntImage = Image()
BlackAntImage.filename = "Resources/Textures/BlackAntTable"
BlackAntImage.blocks = Vector2(4, 4)
BlackAntImage.size = Vector2(128, 128)

-- Creates a Sprite for a BlackAnt
function BASprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = BlackAntImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end

-- Creates a BlackAnt object
function BlackAnt()
	walk = BASprite(0, 3, 0.07)
	stand = BASprite(0, 0, 0.07)
	
	blackant = Character()
	blackant.sprites:Add("Walk", walk)
	blackant.sprites:Add("Stand", stand)
	blackant.size = BlackAntImage.size
	blackant.position = Vector2(0,0)
	blackant.velocity = Vector2(0,0)
	blackant.maxVelocity = 640
	blackant.thinker = "BlackAntThinker"
	blackant.health = 1
	blackant.name = "Black Ant"
	blackant:setSprite("Stand")
	blackant.state = {
		tracking = false,
		mad = false,
	}
	map.characters:Add(blackant)
	
	return blackant
end

-- Black Ant behavior function
function BlackAntThinker( self, gameTime )
	vision = 640
	if ( self.state.mad == true and self.state.tracking ) then
		vision = 1600
	end
	
	if ( AI.canSeeHelix( self, vision ) ) then 
		AI.moveToHelix( self, 128.0, 64.0 )
		self:setSprite("Walk")
		self.state.tracking = true		
	else
		AI.stop( self )
		
		if ( self.state.tracking == true ) then
			print("getting angry")
			self.state.mad = true
		end
		
		self.state.tracking = false
		self:setSprite("Stand")
	end
	
	-- TODO: Extend AI for the Black Ant
end