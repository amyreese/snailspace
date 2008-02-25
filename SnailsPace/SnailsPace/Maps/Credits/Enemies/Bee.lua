
--[[ 
	Bee.lua
	Define the Bee's properties and behaviors.
]]--

library('AI')

-- Generic BeeImage object to be reused by all Bees
BeeImage = Image()
BeeImage.filename = "Resources/Textures/BeeTable"
BeeImage.blocks = Vector2(4, 4)
BeeImage.size = Vector2(64, 64)

-- Creates a Sprite for a Bee
function BeeSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = BeeImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end
	

-- Creates a Bee object
function Bee(startPos, behav)
	behav = behav or "flyUp"
	fly = BeeSprite(0, 3, .07)
	hover = BeeSprite(0, 3, .035)
	die = BeeSprite(8, 11, .25)
	
	bee = Character("stinger")
	bee.sprites:Add("Fly", fly)
	bee.sprites:Add("Hover", hover)
	bee.sprites:Add("Die", die)
	bee.size = BeeImage.size
	bee.startPosition = startPos
	bee.position = startPos
	bee.maxVelocity = 768
	bee.state = {}
	bee.name = "Bee"
	bee.health = 3
	bee.affectedByGravity = true
	bee:setSprite("Hover")
	bee.behavior = behav
	map.characters:Add(bee)

	return bee
end
