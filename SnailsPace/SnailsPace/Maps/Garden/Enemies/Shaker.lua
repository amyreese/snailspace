
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
	stand = ShakerSprite(0, 0, 0.07)
	fly = ShakerSprite(0, 0, 0.07)
	die = ShakerSprite(0, 0, .17)
	
	shaker = Character("flamethrower")
	shaker.sprites:Add("Walk", walk)
	shaker.sprites:Add("Stand", stand)
	shaker.sprites:Add("Die", die)
	shaker.sprites:Add("Fly", fly)
	shaker.size = Vector2(ShakerImage.size.X - 48, ShakerImage.size.Y - 48)
	shaker.startPosition = startPos
	shaker.position = startPos
	shaker.affectedByGravity = true
	shaker.direction = Vector2(0,0)
	shaker.maxVelocity = 400
	shaker.thinker = "ShakerThinker"
	shaker.health = 100
	shaker.weapon.cooldown = 800
	shaker.name = "Shaker"
	shaker:setSprite("Stand")
	shaker.state = {}
	map.characters:Add(shaker)
	
	return shaker
end

-- Fire Ant behavior function
function ShakerThinker( self, gameTime )
	if(self.health == 1) then
		EndLevel.BuildBossEnd( self.position.X - xOffset, self.position.Y - yOffset )
	end
	
	self:ShootAt(helix.position, gameTime)
	
	if(keystone.affectedByGravity) then
		if(self.health < 25) then
		
		elseif(self.health < 50) then
			--[[--Go Right
			if(not self.state.movingLeft) then
				if(self.position <= 9100) then
					self.direction = Vector2(1, 0)
				else
					self.state.movingLeft = true
				end
			elseif(self.state.goingUp) then
				if(self.position.Y < 800) then
					--Go UpLeft
					AI.diagonalPatrol(self, Vector2( self.startPosition.X - 700, self.startPosition.Y + 700), Vector2( self.startPosition.X, self.startPosition.Y))
				else
					self.state.goingUp = false
			else
				if(self.position.Y > 800) then
				
				--Go DownLeft
				AI.diagonalPatrol(self, Vector2( self.startPosition.X - 700, self.startPosition.Y + 700), Vector2( self.startPosition.X, self.startPosition.Y))
			end]]--
		elseif(self.health < 75) then
			AI.jumpPatrol(self)
		elseif(self.health <= 100) then
			AI.slowJumpPatrol(self)
		end
	end
	
		
end
