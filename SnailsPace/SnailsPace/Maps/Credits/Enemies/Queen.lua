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
function Queen(startPos)
	walk = QueenSprite(0, 1, 0.07)
	stand = QueenSprite(0, 0, 0.07)
	die = QueenSprite(2, 7, .17)
	
	
	queen = Character("flamethrower")
	queen.sprites:Add("Walk", walk)
	queen.sprites:Add("Stand", stand)
	queen.sprites:Add("Die", die)
	queen.size = Vector2(QueenImage.size.X, QueenImage.size.Y - 64)
	queen.startPosition = startPos
	queen.position = startPos
	queen.affectedByGravity = true
	queen.maxVelocity = 200
	queen.health = 50
	queen.weapon.cooldown = 800
	queen.name = "Queen"
	queen:setSprite("Stand")
	queen.state = {}
	function queen.state:die(gameTime)
	end
	map.characters:Add(queen)
	
	return queen
end
