
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
	
	shaker = Character("generic")
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
	shaker.thinker = "ShakerThinker"
	shaker.health = 200
	shaker.weapon.cooldown = 200
	shaker.name = "Shaker"
	shaker:setSprite("Walk")
	shaker.state = {
		shaker = shaker
	}
	function shaker.state:die(gameTime)
		EndLevel.BuildBossEnd( self.shaker.position.X - xOffset, self.shaker.position.Y - yOffset )
	end
	map.characters:Add(shaker)
	
	return shaker
end

-- Shaker behavior function
function ShakerThinker( self, gameTime )
	
	AI.shootDirectlyAtHelix(self, gameTime)
	--Set movement and target at different health phases
	if(keystone.affectedByGravity) then
		if(self.health <= 50) then
			AI.diagonalPatrol(self, Vector2( self.startPosition.X-700, self.startPosition.Y + 450), Vector2( self.startPosition.X, self.startPosition.Y+450))
		elseif(self.health <= 100) then
			AI.diagonalPatrol(self, Vector2( self.startPosition.X, self.startPosition.Y + 600), Vector2( self.startPosition.X, self.startPosition.Y+100))
		elseif(self.health <= 150) then
			AI.jumpPatrol(self)
		elseif(self.health <= 200) then
			AI.slowJumpPatrol(self)
		end
	end
	
	--Set weapon and rotation at different health phases
	if(self.health <= 52 and self.health > 49) then
		if(self.weapon.name ~= "Fanshot") then
			self.weapon = Weapon.load("fanshot")
			self.weapon.cooldown = 800
			self.rotation = 3.14
			self.bounds = GameObjectBounds(Vector2.Multiply(Vector2(ShakerImage.size.X - 48, ShakerImage.size.Y - 48), self.scale), self.position, self.rotation);
			self:setSprite("Fly2")
		end
	end
	
	if(self.health <= 102 and self.health > 99) then
		if(self.weapon.name ~= "Grenade Launcher") then
			self.weapon = Weapon.load("grenadelauncher")
			self.weapon.cooldown = 1600
			self.affectedByGravity = false
			self.rotation = 1.57
			self.bounds = GameObjectBounds(Vector2.Multiply(Vector2(ShakerImage.size.X - 48, ShakerImage.size.Y - 48), self.scale), self.position, self.rotation);
			self:setSprite("Fly")
		end
	end
	
	if (self.health <= 152 and self.health > 149) then
		if(self.weapon.name ~= "Saltthrower") then
			self.weapon = Weapon.load("saltthrower")
			self.weapon.cooldown = 800
			self:setSprite("Stand")
		end
	end
	
	
	
		
end
