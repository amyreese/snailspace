
--[[ 
	Spider.lua
	Define the Spider's properties and behaviors.
]]--

library('AI')

-- Generic SpiderImage object to be reused by all Fire Ants
SpiderImage = Image()
SpiderImage.filename = "Resources/Textures/SpiderTable"
SpiderImage.blocks = Vector2(4, 4)
SpiderImage.size = Vector2(128, 128)

-- Creates a Sprite for a Spider
function SpiderSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = SpiderImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end

-- Creates a Spider object
function Spider(startPos, attack)
	walk = SpiderSprite(2, 3, 0.12)
	stand = SpiderSprite(0, 0, 0.07)
	die = SpiderSprite(8, 11, .08)
	
	spider = Character("fanshot")
	spider.sprites:Add("Walk", walk)
	spider.sprites:Add("Stand", stand)
	spider.sprites:Add("Die", die)
	spider.size = Vector2(SpiderImage.size.X - 64, SpiderImage.size.Y - 64)
	spider.startPosition = startPos
	spider.position = startPos
	spider.direction = Vector2(0,0)
	spider.maxVelocity = 640
	spider.thinker = "SpiderThinker"
	spider.health = 6
	spider.name = "Spider"
	spider.weapon.cooldown = 500
	spider.weapon.ammunition = -1
	spider:setSprite("Stand")
	spider.state = {
		tracking = false,
		mad = false,
		attacking = attack,
	}
	map.characters:Add(spider)
	
	return spider
end

-- Spider behavior function
function SpiderThinker( self, gameTime )
	self:setSprite("Stand")
	self.direction = Vector2(0,1)
	if AI.canSeeHelix(self, 800) then
		if AI.canSeeHelix(self, 400) then
			self.direction = Vector2(0,0)
			if Player.helix.position.Y + 100 < self.position.Y then 
				self:setSprite("Walk")
				self.direction = Vector2(0,-1)
			elseif Player.helix.position.Y > self.position.Y then 
				self:setSprite("Walk")
				self.direction = Vector2(0,1)
			end
		end
		if Player.helix.position.Y - 150 < self.position.Y then
			AI.shootSpiderPattern( self, gameTime )
		end
	end
end