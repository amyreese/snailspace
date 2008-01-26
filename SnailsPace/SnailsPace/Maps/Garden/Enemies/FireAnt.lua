
--[[ 
	FireAnt.lua
	Define the Fire Ant's properties and behaviors.
]]--

library('AI')

-- Generic FireAntImage object to be reused by all Fire Ants
FireAntImage = Image()
FireAntImage.filename = "Resources/Textures/FireAntTable"
FireAntImage.blocks = Vector2(4, 4)
FireAntImage.size = Vector2(128, 128)

-- Creates a Sprite for a FireAnt
function FASprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = FireAntImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end

-- Creates a FireAnt object
function FireAnt()
	walk = FASprite(0, 3, 0.07)
	stand = FASprite(0, 0, 0.07)
	
	fireant = Character()
	fireant.sprites:Add("Walk", walk)
	fireant.sprites:Add("Stand", stand)
	fireant.size = FireAntImage.size
	fireant.position = Vector2(0,0)
	fireant.velocity = Vector2(0,0)
	fireant.maxVelocity = 640
	fireant.thinker = "FireAntThinker"
	fireant:setSprite("Stand")
	fireant.state = {
		tracking = false,
		mad = false,
	}
	map.characters:Add(fireant)
	
	return fireant
end

-- Fire Ant behavior function
function FireAntThinker( self, gameTime )
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
	
	-- TODO: Extend AI for the Fire Ant
end