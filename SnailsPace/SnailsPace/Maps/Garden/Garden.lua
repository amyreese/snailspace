library('Weapons')
library('WorldBuilding')

-- Player creation and starting position
--player = Player( Vector2(-1000,0), "Tree Fort" )
--player = Player(Vector2(7956, -4500), "Tree Fort")
--player = Player(Vector2(800, -800), "Tree Fort")
--player = Player(Vector2(5456, -4500), "Tree Fort")
player = Player(Vector2(8500, 0))

-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/FireAnt.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")
include("Enemies/Queen.lua")
include("Enemies/Shaker.lua")

-- Set up the background
include("Scenery/Background.lua")
include("Scenery/TunnelBackground.lua")

-- Scenery Sprite Definitions
include("Scenery/Sprites.lua")

-- Trigger Definitions
include("Triggers/SavePoints.lua")
include("Triggers/Powerups.lua")
include("Triggers/Traps.lua")
include("Triggers/EndLevel.lua")
include("Triggers/Message.lua")

-- Set up map platforms
include("Scenery/Pit.lua")
include("Scenery/Platforms.lua")
include("Scenery/Plane1.lua")
include("Scenery/Plane2.lua")
include("Scenery/BossArea.lua")

-- Set the bounds for this map
map.bounds:Add(Vector2(-1400, -80))
map.bounds:Add(Vector2(-1400, 3000))
map.bounds:Add(Vector2(9960, 3000))
map.bounds:Add(Vector2(9960, -5100))
map.bounds:Add(Vector2(4000, -5100))

-- Create an enemy
fireAntY = -4856
fireAntX = 100
--fireant1 = FireAnt(Vector2(5056, fireAntY))
--fireant2 = FireAnt(Vector2(5456 + fireAntX, fireAntY))
--fireant3 = FireAnt(Vector2(5856 + fireAntX, fireAntY))
--fireant4 = FireAnt(Vector2(6256 + fireAntX, fireAntY))
--fireant5 = FireAnt(Vector2(6656 + fireAntX, fireAntY))
--fireant6 = FireAnt(Vector2(7056 + fireAntX, fireAntY))
--fireant7 = FireAnt(Vector2(7456 + fireAntX, fireAntY))
--fireant8 = FireAnt(Vector2(7856 + fireAntX, fireAntY))

--Platform Black Ants
blackant1 = BlackAnt(Vector2(1080, 0), "platPatrol")
blackant2 = BlackAnt(Vector2(1560, -160), "platPatrol")
blackant3 = BlackAnt(Vector2(2310, -510), "platPatrol")
blackant4 = BlackAnt(Vector2(3160, -480), "platPatrol")
blackant5 = BlackAnt(Vector2(1720, 260), "platPatrol")
blackant6 = BlackAnt(Vector2(2520, 100), "platPatrol")
blackant7 = BlackAnt(Vector2(3160, 575), "platPatrol")
blackant8 = BlackAnt(Vector2(3460, 50), "platPatrol")

--Platform Spiders
spider9 = Spider(Vector2(1080, -250))
spider10 = Spider(Vector2(1560, -430))
spider11 = Spider(Vector2(2310, -930))
spider12 = Spider(Vector2(3160, -680))
spider13 = Spider(Vector2(1720, 60))
spider14 = Spider(Vector2(2520, -100))
spider15 = Spider(Vector2(3160, 375))
spider16 = Spider(Vector2(3610, -250))

--Plane1 Bees
bee1 = Bee(Vector2(0,50))
bee2 = Bee(Vector2(50,50))
bee3 = Bee(Vector2(100,50))
bee4 = Bee(Vector2(150,50))
bee5 = Bee(Vector2(200,50))
bee6 = Bee(Vector2(250,50))
bee7 = Bee(Vector2(300,50))
bee8 = Bee(Vector2(350,50))
bee1.state.attacking = true
bee2.state.attacking = true
bee3.state.attacking = true
bee4.state.attacking = true
bee5.state.attacking = true
bee6.state.attacking = true
bee7.state.attacking = true
bee8.state.attacking = true

--Pit Ramp Spiders
spider1 = Spider(Vector2(2060, -1950), true)
spider2 = Spider(Vector2(2260, -2200), true)
spider3 = Spider(Vector2(2460, -2400), true)
spider4 = Spider(Vector2(2660, -2600), true)
spider5 = Spider(Vector2(2860, -2800), true)
spider6 = Spider(Vector2(3060, -3000), true)
spider7 = Spider(Vector2(3260, -3200), true)
spider8 = Spider(Vector2(3460, -3400), true)

--Escape Boosts
boost1 = Powerups.BuildBoostPowerup(7956, -4500)
boost2 = Powerups.BuildBoostPowerup(7656, -3500)
boost3 = Powerups.BuildBoostPowerup(7356, -2500)
boost4 = Powerups.BuildBoostPowerup(7656, -1500)
boost5 = Powerups.BuildBoostPowerup(7956, -500)

--Secret Boosts
boost5 = Powerups.BuildBoostPowerup(6950, 1000)

--Save Points
SavePoints.BuildSavePoint( 4000, 35, savepointSprite )
SavePoints.BuildSavePoint( 5456, -4865, savepointSprite )
SavePoints.BuildSavePoint( 8600, 375, savepointSprite )
SavePoints.BuildSavePoint( 7500, 2575, savepointSprite )

--Health
health1 = Powerups.BuildHealthPowerup( 650, -1400 )

--Weapons
stinger1 = Powerups.BuildWeaponPowerup( 850, -1400, "stinger", 10)
minigun1 = Powerups.BuildWeaponPowerup( 6000, -2200, "minigun", 500)

--Princess Boss
thePrincess = Queen(Vector2(7856, -4856))

saltShaker = Shaker(Vector2(9800, 500))





