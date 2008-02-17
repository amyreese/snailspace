
--[[ 
	Queen.lua
	Define the Queen's properties and behaviors.
]]--

library('AI')

-- Generic QueenImage object to be reused by all Fire Ants
QueenImage = Image()
QueenImage.filename = "Resources/Textures/QueenTable"
QueenImage.blocks = Vector2(2, 4)
QueenImage.size = Vector2(512, 256)

-- Queen Sack Image
SackImage = Image()
SackImage.filename = "Resources/Textures/SackTable"
SackImage.blocks = Vector2(2, 4)
SackImage.size = Vector2(512, 256)

-- Creates a Sprite for a Queen
function QueenSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = QueenImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	sprt.horizontalFlip = true
	
	return sprt
end

--Creates a Sack Sprite for a Queen
function SackSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = SackImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	sprt.layerOffset = -10
	sprt.horizontalFlip = true
	sprt.position = Vector2( 200, 0 )
	
	return sprt
end

-- Creates a Queen object
function Queen(startPos)
	walk = QueenSprite(0, 1, 0.07)
	stand = QueenSprite(0, 0, 0.07)
	die = QueenSprite(2, 2, .17)
	sack = SackSprite(0, 3, 0.07)

	queen = Character("flamethrower")
	queen.sprites:Add("Walk", walk)
	queen.sprites:Add("Stand", stand)
	queen.sprites:Add("Die", die)
	queen.sprites:Add("Sack", sack)

	queen.size = Vector2(QueenImage.size.X, QueenImage.size.Y - 64)
	queen.startPosition = startPos
	queen.position = startPos
	queen.affectedByGravity = true
	queen.direction = Vector2(0,0)
	queen.maxVelocity = 400
	queen.thinker = "QueenThinker"
	queen.health = 50
	queen.weapon.cooldown = 2000
	queen.name = "Queen"
	queen:setSprites("Stand","Sack")
	queen.state = {
		tracking = false,
		mad = false,
		lastSpawned = 0,
	}
	map.characters:Add(queen)
	
	return queen
end

-- Fire Ant behavior function
function QueenThinker( self, gameTime )
	if AI.canSeeHelix( self, 800 ) then
		AI.shootDirectlyAtHelix(self, gameTime)
	end
	if AI.canSeeHelix( self, 1600 ) then
		if gameTime.TotalRealTime.TotalSeconds - self.state.lastSpawned > 2 then
			self.state.lastSpawned = gameTime.TotalRealTime.TotalSeconds
			FireAnt( Vector2( self.position.X - 128, self.position.Y - 64 ) )
		end
	end
end