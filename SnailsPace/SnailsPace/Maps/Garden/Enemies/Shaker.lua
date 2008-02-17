
--[[ 
	Shaker.lua
	Define the Shaker's properties and behaviors.
]]--

library('AI')

-- Generic ShakerImage object to be reused by all Fire Ants
ShakerImage = Image()
ShakerImage.filename = "Resources/Textures/ShakerTable"
ShakerImage.blocks = Vector2(4, 2)
ShakerImage.size = Vector2(256, 392)

-- Creates a Sprite for a Shaker
function ShakerSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = ShakerImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end

-- Creates a Shaker object
function Shaker(startPos)
	walk = ShakerSprite(0, 0, 0.07)
	stand = ShakerSprite(0, 0, 0.07)
	die = ShakerSprite(0, 0, .17)
	
	shaker = Character("flamethrower")
	shaker.sprites:Add("Walk", walk)
	shaker.sprites:Add("Stand", stand)
	shaker.sprites:Add("Die", die)
	shaker.size = Vector2(ShakerImage.size.X, ShakerImage.size.Y - 64)
	shaker.startPosition = startPos
	shaker.position = startPos
	shaker.affectedByGravity = true
	shaker.direction = Vector2(1,0)
	shaker.maxVelocity = 400
	shaker.thinker = "ShakerThinker"
	shaker.health = 10
	shaker.weapon.cooldown = 800
	shaker.name = "Shaker"
	shaker:setSprite("Stand")
	shaker.state = {
		tracking = false,
		mad = false,
	}
	map.characters:Add(shaker)
	
	return shaker
end

-- Fire Ant behavior function
function ShakerThinker( self, gameTime )
	AI.jump(self)
	-- TODO: Extend AI for the Fire Ant
end
