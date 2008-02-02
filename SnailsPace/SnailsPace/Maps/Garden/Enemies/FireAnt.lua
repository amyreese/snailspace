
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
function FireAnt(startPos)
	walk = FASprite(0, 3, 0.07)
	stand = FASprite(0, 0, 0.07)
	
	fireant = Character()
	fireant.sprites:Add("Walk", walk)
	fireant.sprites:Add("Stand", stand)
	fireant.size = Vector2(FireAntImage.size.X, FireAntImage.size.Y - 64)
	fireant.startPosition = startPos
	fireant.position = startPos
	fireant.affectedByGravity = true
	fireant.velocity = Vector2(1,0)
	fireant.maxVelocity = 400
	fireant.thinker = "FireAntThinker"
	fireant.health = 3
	fireant.name = "FireAnt"
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
	AI.platformPatrol(self)
	
	-- TODO: Extend AI for the Fire Ant
end