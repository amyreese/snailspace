
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
function Spider(startPos)
	walk = SpiderSprite(0, 3, 0.07)
	stand = SpiderSprite(0, 0, 0.07)
	
	spider = Character()
	spider.sprites:Add("Walk", walk)
	spider.sprites:Add("Stand", stand)
	spider.size = Vector2(SpiderImage.size.X - 64, SpiderImage.size.Y - 64)
	spider.startPosition = startPos
	spider.position = startPos
	spider.direction = Vector2(0,0)
	spider.maxVelocity = 640
	spider.thinker = "SpiderThinker"
	spider.health = 6
	spider.name = "Spider"
	spider.weapon.cooldown = 500
	spider:setSprite("Stand")
	spider.state = {
		tracking = false,
		mad = false,
	}
	map.characters:Add(spider)
	
	return spider
end

-- Spider behavior function
function SpiderThinker( self, gameTime )
	if ( helix.position.Y < self.position.Y and AI.canSeeHelix(self, 300)) then 
		self:setSprite("Walk")
		self.direction = Vector2(0,-1)
	else
		self.direction = Vector2(0,1)
	end
	if self.state.attacking then
		AI.shootSpiderPattern( self, gameTime );
	end
end