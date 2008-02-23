
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
function FireAnt(startPos, behav)
	walk = FASprite(0, 3, 0.07)
	stand = FASprite(0, 0, 0.07)
	die = FASprite(8, 11, .17)
	
	fireant = Character()
	fireant.sprites:Add("Walk", walk)
	fireant.sprites:Add("Stand", stand)
	fireant.sprites:Add("Die", die)
	fireant.size = Vector2(FireAntImage.size.X, FireAntImage.size.Y - 64)
	fireant.startPosition = startPos
	fireant.position = startPos
	fireant.affectedByGravity = true
	fireant.direction = Vector2(1,0)
	fireant.maxVelocity = 400
	fireant.thinker = "FireAntThinker"
	fireant.health = 3
	fireant.weapon.cooldown = 800
	fireant.name = "FireAnt"
	fireant.behavior = behav
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
	if (self.behavior == "platPatrol") then
		AI.platformPatrol(self)
		if(AI.canSeeHelix(self, 300)) then
			AI.shootDirectlyAtHelix(self, gameTime)
		end
	elseif (self.behavior == "patrol") then
		AI.patrol(self, self.startPosition.X + 300, self.startPosition.X - 300)
		if(AI.canSeeHelix(self, 300)) then
			AI.shootDirectlyAtHelix(self, gameTime)
		end
	elseif (self.behavior == "attack") then
		AI.moveToHelix(self, nil, nil, nil, false)
		self:setSprite("Walk")
		if(AI.canSeeHelix(self, 300)) then
			AI.shootDirectlyAtHelix(self, gameTime)
		end
	elseif (self.behavior == "blockWatch") then
		for x=0,5 do
			if(queenBlocks[x].affectedByGravity) then
				queenBlocksTriggers[x].position = Vector2( queenBlocks[x].position.X, queenBlocks[x].position.Y - 4 )
				queenBlocksTriggers[x].bounds = GameObjectBounds(queenBlocks[x].size, queenBlocksTriggers[x].position, queenBlocks[x].rotation)
			end
		end
	end
	-- TODO: Extend AI for the Fire Ant
end