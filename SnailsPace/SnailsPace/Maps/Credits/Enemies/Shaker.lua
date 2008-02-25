
--[[ 
	Shaker.lua
	Define the Shaker's properties and behaviors.
]]--

library('AI')

-- Generic ShakerImage object to be reused by all Fire Ants
ShakerImage = Image()
ShakerImage.filename = "Resources/Textures/ShakerTable"
ShakerImage.blocks = Vector2(4, 4)
ShakerImage.size = Vector2(256, 288)

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
	stand = ShakerSprite(1, 1, 0.07)
	fly = ShakerSprite(2, 2, 0.07)
	fly2 = ShakerSprite(2, 2, 0.07)
	die = ShakerSprite(4, 14, .17)
	
	shaker = Character("saltneric")
	shaker.sprites:Add("Walk", walk)
	shaker.sprites:Add("Stand", stand)
	shaker.sprites:Add("Die", die)
	shaker.sprites:Add("Fly", fly)
	shaker.sprites:Add("Fly2", fly2)
	shaker.size = Vector2(ShakerImage.size.X - 72, ShakerImage.size.Y - 48)
	shaker.startPosition = startPos
	shaker.position = startPos
	shaker.affectedByGravity = true
	shaker.direction = Vector2(0,0)
	shaker.maxVelocity = 400
	shaker.health = 200
	shaker.maxHealth = 200;
	shaker.weapon.cooldown = 200
	shaker.name = "Shaker"
	shaker.thinker = "ShakerThinker"
	shaker:setSprite("Walk")
	shaker.state = {
		shaker = shaker
	}
	map.characters:Add(shaker)
	
	return shaker
end

-- Shaker behavior function
function ShakerThinker( self, gameTime )
	if(self.health <= 55 and self.health > 45) then
		self:setSprite("Fly2")
	end
	
	if(self.health <= 105 and self.health > 95) then
			self:setSprite("Fly")
	end
	
	if (self.health <= 155 and self.health > 145) then
			self:setSprite("Stand")
	end
end