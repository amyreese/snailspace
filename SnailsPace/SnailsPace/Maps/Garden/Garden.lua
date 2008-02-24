library('Weapons')
library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2( -1000, 0 )
--startPosition = Vector2(7956, -4600)
--startPosition = Vector2(800, -800)
--startPosition = Vector2(5456, -4500)
--startPosition = Vector2(5500, 800)
--startPosition = Vector2(8500, 0)
player = Player( startPosition, "generic", "Tree Fort" ) 


-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/FireAnt.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")
include("Enemies/Queen.lua")
include("Enemies/Shaker.lua")
include("Enemies/Hive.lua")

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

-- Set up map platforms
include("Scenery/QueenBlock.lua")
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

--****Plane 1****
--Plane1 Bees
bee1 = Bee(Vector2(100,150), "flyUp")
bee3 = Bee(Vector2(200,150), "flyUp")
bee5 = Bee(Vector2(300,150), "flyUp")
bee7 = Bee(Vector2(400,150), "flyUp")
--bee1.state.attacking = true
--bee3.state.attacking = true
--bee5.state.attacking = true
--bee7.state.attacking = true


--Plane1 Black Ants
blackant1 = BlackAnt(Vector2(0, 0), "attack")
blackant2 = BlackAnt(Vector2(128, 0), "attack")
blackant3 = BlackAnt(Vector2(256, 0), "attack")
blackant4 = BlackAnt(Vector2(384, 0), "attack")

--****Platforms****
--Platform Black Ants
blackant1 = BlackAnt(Vector2(1080, 0), "platPatrol")
blackant2 = BlackAnt(Vector2(1560, -160), "platPatrol")
blackant3 = BlackAnt(Vector2(2310, -510), "platPatrol")
blackant4 = BlackAnt(Vector2(3160, -480), "platPatrol")
blackant5 = BlackAnt(Vector2(1720, 260), "platPatrol")
blackant6 = BlackAnt(Vector2(2520, 100), "platPatrol")
blackant7 = BlackAnt(Vector2(3160, 500), "platPatrol")
blackant8 = BlackAnt(Vector2(3460, 50), "platPatrol")

--Platform Spiders
spider9 = Spider(Vector2(1080, -250))
spider10 = Spider(Vector2(1560, -430))
spider11 = Spider(Vector2(2310, -930))
spider12 = Spider(Vector2(3160, -680))
spider13 = Spider(Vector2(1720, 60))
spider14 = Spider(Vector2(2520, -100))
spider15 = Spider(Vector2(3160, 150))
spider16 = Spider(Vector2(3610, -250))

--****Plane 2****
--Plane 2 Bees
bee21 = Bee(Vector2(4200,150), "flyUp")
bee22 = Bee(Vector2(4300,150), "flyUp")
bee23 = Bee(Vector2(4400,150), "flyUp")
bee2 = Bee(Vector2(4500,150), "flyUp")
bee4 = Bee(Vector2(4600,150), "flyUp")
bee6 = Bee(Vector2(4700,150), "flyUp")
bee8 = Bee(Vector2(4800,150), "flyUp")
bee21.state.attacking = true
bee22.state.attacking = true
bee23.state.attacking = true
bee2.state.attacking = true
bee4.state.attacking = true
bee6.state.attacking = true
bee8.state.attacking = true

--****Cave Drop****
-- Cave Drop Fire Ants
fireAntY = -4856
fireant1 = FireAnt(Vector2(5056, 50), "platPatrol")
fireant4 = FireAnt(Vector2(5156, -700), "platPatrol")
fireant5 = FireAnt(Vector2(5356, -700), "platPatrol")
fireant6 = FireAnt(Vector2(4856, -2750), "platPatrol")
fireant7 = FireAnt(Vector2(5056, -2750), "platPatrol")
fireant2 = FireAnt(Vector2(5456, fireAntY), "attack")
fireant3 = FireAnt(Vector2(5856, fireAntY), "attack")

--Cave Drop Bees
bee9 = Bee(Vector2(5456,-200), "flyLeftRight")
bee10 = Bee(Vector2(4856,-1100), "flyLeftRight")
bee11 = Bee(Vector2(4856,-1300), "flyLeftRight")
bee12 = Bee(Vector2(4856,-2000), "flyLeftRight")
bee13 = Bee(Vector2(4856,-2200), "flyLeftRight")
bee14 = Bee(Vector2(4856,-2400), "flyLeftRight")
bee15 = Bee(Vector2(5456,-2950), "flyLeftRight")
bee16 = Bee(Vector2(5456,-3150), "flyLeftRight")
bee17 = Bee(Vector2(5456,-3350), "flyLeftRight")
bee18 = Bee(Vector2(5356,-4050), "flyLeftRight")
bee19 = Bee(Vector2(5356,-4250), "flyLeftRight")
bee20 = Bee(Vector2(5356,-4450), "flyLeftRight")
bee9.state.attacking = true
bee10.state.attacking = true
bee11.state.attacking = true
bee12.state.attacking = true
bee13.state.attacking = true
bee14.state.attacking = true
bee15.state.attacking = true
bee16.state.attacking = true
bee17.state.attacking = true
bee18.state.attacking = true
bee19.state.attacking = true
bee20.state.attacking = true

--****Secret Areas****
--Pit Secret Ramp Spiders
spider1 = Spider(Vector2(2060, -1950), true)
spider2 = Spider(Vector2(2260, -2200), true)
spider3 = Spider(Vector2(2460, -2400), true)
spider4 = Spider(Vector2(2660, -2600), true)
spider5 = Spider(Vector2(2860, -2800), true)
spider6 = Spider(Vector2(3060, -3000), true)
spider7 = Spider(Vector2(3260, -3200), true)
spider8 = Spider(Vector2(3460, -3400), true)

--High Secret Hive
hive1 = Hive(Vector2(6100, 35))

--Secret Boosts
boost5 = Powerups.BuildBoostPowerup(6950, 1000)

--Escape Boosts
boost1 = Powerups.BuildBoostPowerup(7956, -4300)
boost2 = Powerups.BuildBoostPowerup(7656, -3500)
boost3 = Powerups.BuildBoostPowerup(7356, -2500)
boost4 = Powerups.BuildBoostPowerup(7656, -1500)
boost5 = Powerups.BuildBoostPowerup(7956, -500)

--Save Points
SavePoints.BuildSavePoint( 4000, 35, savepointSprite )
SavePoints.BuildSavePoint( 5456, -4865, savepointSprite )
SavePoints.BuildSavePoint( 8600, 375, savepointSprite )
SavePoints.BuildSavePoint( 7500, 2575, savepointSprite )

--Health
health1 = Powerups.BuildHealthPowerup( 650, -1400 )
health2 = Powerups.BuildHealthPowerup( 4956, -2750 )

-- Fuel
Powerups.BuildFuelPowerup( 2660, -2900 )

--Weapons
stinger1 = Powerups.BuildWeaponPowerup( 850, -1400, "stinger", 200, 0.25)
flamethrower1 = Powerups.BuildWeaponPowerup( 6000, 400, "flamethrower", 40, 1)
minigun1 = Powerups.BuildWeaponPowerup( 9000, 2500, "minigun", 200, 3)
minigun2 = Powerups.BuildWeaponPowerup( 2100, -750, "minigun", 100, 3)
minigun3 = Powerups.BuildWeaponPowerup( 5156, 200, "minigun", 200, 3)
grenadelauncher1 = Powerups.BuildWeaponPowerup( 6200, -2100, "grenadelauncher", 20, 5)

--Princess Boss
thePrincess = Queen(Vector2(7856, -4856))

--The Salt Shaker boss
saltShaker = Shaker(Vector2(9800, 425))





