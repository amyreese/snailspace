
-- Generic BeeImage object to be reused by all Bees
RogueHelixImage = Image()
RogueHelixImage.filename = "Resources/Textures/HelixTable"
RogueHelixImage.blocks = Vector2(4, 5)
RogueHelixImage.size = Vector2(128, 128)

-- Creates a Sprite for RogueHelix
function RogueHelixSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = RogueHelixImage
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
function RogueHelix(startPos)
	sit = RogueHelixSprite(0, 0, .035)
	
	roguehelix = Character("stinger")
	bounds = GameObjectBoundsBuilder()
	bounds:AddPoint(Vector2(0.0, 28.0))
	bounds:AddPoint(Vector2(20.0, 18.0))
	bounds:AddPoint(Vector2(28.0, 0.0))
	bounds:AddPoint(Vector2(28.0, -34.0))
	bounds:AddPoint(Vector2(-28.0, -34.0))
	bounds:AddPoint(Vector2(-28.0, 0.0))
	bounds:AddPoint(Vector2(-20.0, 18.0))
	roguehelix.bounds = bounds:BuildBounds()
	roguehelix.rotation = 0
	roguehelix.sprites:Add("Sit", sit)
	roguehelix.sprites:Add("Diet", sit)
	roguehelix.size = RogueHelixImage.size
	roguehelix.startPosition = startPos
	roguehelix.position = startPos
	roguehelix.maxVelocity = 768
	roguehelix.state = {}
	roguehelix.name = "RogueHelix"
	roguehelix.health = 999999999
	roguehelix.affectedByGravity = true
	roguehelix:setSprite("Sit")
	map.characters:Add(roguehelix)

	return roguehelix
end
