
--[[ 
	Name.lua
	Define the Name's properties and behaviors.
]]--

library('AI')

-- Joe's Name
JoeImage = Image()
JoeImage.filename = "Resources/Textures/names/JoeTable"
JoeImage.blocks = Vector2(4, 2)
JoeImage.size = Vector2(128, 128)

-- Josh's Name
JoshImage = Image()
JoshImage.filename = "Resources/Textures/names/JoshTable"
JoshImage.blocks = Vector2(4, 2)
JoshImage.size = Vector2(128, 128)

-- Pat's Name
PatImage = Image()
PatImage.filename = "Resources/Textures/names/PatTable"
PatImage.blocks = Vector2(4, 2)
PatImage.size = Vector2(128, 128)

-- John's Name
JohnImage = Image()
JohnImage.filename = "Resources/Textures/names/JohnTable"
JohnImage.blocks = Vector2(4, 2)
JohnImage.size = Vector2(128, 128)

-- Brian's Name
BrianImage = Image()
BrianImage.filename = "Resources/Textures/names/BrianTable"
BrianImage.blocks = Vector2(4, 2)
BrianImage.size = Vector2(128, 128)

-- Simon's Name
SimonImage = Image()
SimonImage.filename = "Resources/Textures/names/JohnTable"
SimonImage.blocks = Vector2(4, 2)
SimonImage.size = Vector2(128, 128)

-- Creates a Sprite for a Bee
function NameSprite(name, animSt, animEnd, animDelay)
	sprt = Sprite()
	if name == "joe" then
		sprt.image = JoeImage
	elseif name == "pat" then
		sprt.image = PatImage
	elseif name == "josh" then
		sprt.image = JoshImage
	elseif name == "john" then
		sprt.image = JohnImage
	elseif name == "brian" then
		sprt.image = BrianImage
	elseif name == "simon" then
		sprt.image = SimonImage
	end
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end
	

-- Creates a Name object
function Name(startPos, name)
	fly = NameSprite(name, 0, 3, .17)
	die = NameSprite(name, 4, 7, .35)

	name = Character("stinger")
	name.sprites:Add("Fly", fly)
	name.sprites:Add("Die", die)
	name.size = fly.image.size
	name.startPosition = startPos
	name.position = startPos
	name.maxVelocity = 768
	name.thinker = "NameThinker"
	name.state = {}
	name.name = "Name"
	name.health = 3
	name.affectedByGravity = true
	name:setSprite("Fly")
	map.characters:Add(name)

	return bee
end

-- Bee behavior function
function NameThinker( self, gameTime )
	if math.random() > 0.5 then
		AI.diagonalPatrol(self, Vector2( Player.helix.position.X - 256, Player.helix.position.Y + 400), Vector2( Player.helix.position.X + 256, Player.helix.position.Y + 408 ))
	else
		AI.diagonalPatrol(self, Vector2( Player.helix.position.X - 256, Player.helix.position.Y + 408), Vector2( Player.helix.position.X + 256, Player.helix.position.Y + 400 ))
	end
	if self.state.attacking then
		if AI.canSeeHelix( self, 600 ) then
			AI.shootDirectlyAtHelix( self, gameTime )
		end
	end
end