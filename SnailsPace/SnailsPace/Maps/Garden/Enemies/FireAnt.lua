
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

-- Creates a Bee object
function FireAnt()
	body = Sprite()
	body.image = FireAntImage
	body.effect = "Resources/Effects/effects"
	body.visible = true
	body.animationStart = 0
	body.animationEnd = 3
	body.animationDelay = 1.0 / 15.0
	body.frame = 0
	body.timer = 0
	
	fireant = Character()
	fireant.sprites:Add("Body", body)
	fireant.size = FireAntImage.size
	fireant.position = Vector2(0,0)
	fireant.velocity = Vector2(0,0)
	fireant.maxVelocity = 640
	fireant.thinker = "FireAntThinker"
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
		
		self.state.tracking = true		
	else
		AI.stop( self )
		
		if ( self.state.tracking == true ) then
			print("getting angry")
			self.state.mad = true
		end
		
		self.state.tracking = false
	end
	
	-- TODO: Extend AI for the Fire Ant
end