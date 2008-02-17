
--[[ 
	Queen.lua
	Define the Queen's properties and behaviors.
]]--

library('AI')

-- Generic QueenImage object to be reused by all Queens
QueenImage = Image()
QueenImage.filename = "Resources/Textures/QueenTable"
QueenImage.blocks = Vector2(2, 4)
QueenImage.size = Vector2(256, 128)

-- Queen Sack Image
SackImage = Image()
SackImage.filename = "Resources/Textures/SackTable"
SackImage.blocks = Vector2(2, 4)
SackImage.size = Vector2(256, 128)

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
	
	return sprt
end

-- Creates a Queen object
function Queen(startPos, haveEggs)
	walk = QueenSprite(0, 1, 0.07)
	stand = QueenSprite(0, 0, 0.07)
	die = QueenSprite(2, 2, .17)
	if(haveEggs) then
		sack = SackSprite(0, 4, 0.07)
		sack.position = Vector2(-200,0);
	end
	
	queen = Character("flamethrower")
	queen.sprites:Add("Walk", walk)
	queen.sprites:Add("Stand", stand)
	queen.sprites:Add("Die", die)
	if(haveEggs) then
		queen.sprites:Add("Sack", sack)
	end
	queen.size = Vector2(QueenImage.size.X, QueenImage.size.Y - 64)
	queen.startPosition = startPos
	queen.position = startPos
	queen.affectedByGravity = true
	queen.direction = Vector2(1,0)
	queen.maxVelocity = 400
	queen.thinker = "QueenThinker"
	queen.health = 10
	queen.weapon.cooldown = 800
	queen.name = "Queen"
	queen:setSprite("Stand")
	queen.state = {
		tracking = false,
		mad = false,
	}
	map.characters:Add(queen)
	
	return queen
end

-- Fire Ant behavior function
function QueenThinker( self, gameTime )
	self:setSprite("Walk")
	
	if(self.horizontalFlip and self.sprites["Sack"]) then
		self.sprites["Sack"].position = Vector2(200, 0)
		self.sprites["Sack"].visible = true
	else
		self.sprites["Sack"].position = Vector2(-200, 0)
		self.sprites["Sack"].visible = true
	end
	AI.moveToHelix(self, nil, nil, nil, false)
	AI.shootDirectlyAtHelix(self, gameTime)
	-- TODO: Extend AI for the Fire Ant
end