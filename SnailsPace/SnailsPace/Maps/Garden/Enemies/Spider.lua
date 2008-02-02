
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
	spider.size = SpiderImage.size
	spider.startPosition = startPos
	spider.position = startPos
	spider.velocity = Vector2(0,0)
	spider.maxVelocity = 640
	spider.thinker = "SpiderThinker"
	spider.health = 2
	spider.name = "Spider"
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
	vision = 640
	if ( self.state.mad == true and self.state.tracking ) then
		vision = 1600
	end
	
	if ( AI.canSeeHelix( self, vision ) ) then 
		AI.moveToHelix( self, 128.0, 64.0 )
		self:setSprite("Walk")
		self.state.tracking = true		
	else
		AI.stop( self )
		
		if ( self.state.tracking == true ) then
			print("getting angry")
			self.state.mad = true
		end
		
		self.state.tracking = false
		self:setSprite("Stand")
	end
	
	-- TODO: Extend AI for the Fire Ant
end