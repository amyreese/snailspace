
--[[ 
	Hive.lua
	Define the Hive's properties and behaviors.
]]--

library('AI')

-- Generic HiveImage object to be reused by all Fire Ants
HiveImage = Image()
HiveImage.filename = "Resources/Textures/beehive"
HiveImage.blocks = Vector2(1, 1)
HiveImage.size = Vector2(512, 512)

-- Creates a Sprite for a Hive
function HiveSprite(animSt, animEnd, animDelay)
	sprt = Sprite()
	sprt.image = HiveImage
	sprt.effect = "Resources/Effects/effects"
	sprt.visible = true
	sprt.animationStart = animSt
	sprt.animationEnd = animEnd
	sprt.animationDelay = animDelay
	sprt.frame = 0
	sprt.timer = 0
	
	return sprt
end

-- Creates a Hive object
function Hive(startPos)
	stand = HiveSprite(0, 0, 0.07)
	die = HiveSprite(0, 0, .17)

	Hive = Character("generic")
	Hive.sprites:Add("Stand", stand)
	Hive.sprites:Add("Die", die)
	Hive.size = HiveImage.size
	Hive.startPosition = startPos
	Hive.position = startPos
	Hive.bounds:Move(Vector2(-128, 128))
	Hive.affectedByGravity = false
	Hive.direction = Vector2(0,0)
	Hive.maxVelocity = 0
	Hive.health = 150
	Hive.maxHealth = 150
	Hive.weapon.ammunition = 0
	Hive.name = "Hive"
	Hive:setSprite("Stand")
	Hive.thinker = "HiveThinker"
	Hive.state = {
		lastSpawned = 0
	}
	function Hive.state:die(gameTime)
		Engine.boss = nil;
		Powerups.BuildFuelPowerup(-750, 1500)
	end
	map.characters:Add(Hive)
	return Hive
end

-- Bee Hive behavior function
function HiveThinker( self, gameTime )
	if AI.canSeeHelix(self, 1200) then
		if Engine.boss == nil then
			Engine.boss = self
		end
	else
		Engine.boss = nil
	end

	if AI.canSeeHelix( self, 1400 ) then
		if gameTime.TotalRealTime.TotalSeconds - self.state.lastSpawned > 1 then
			self.state.lastSpawned = gameTime.TotalRealTime.TotalSeconds
			behav = "swarmHelix"
			if math.random() > 0.5 then
				behav = behav .. "A"
			else
				behav = behav .. "B"
			end
			Bee( Vector2( self.position.X + 64, self.position.Y - 64 ), behav )
		end
	end
end