library('Weapons')

-- Player creation and starting position
player = Player( Vector2(0,0) )
include("Triggers/Message.lua")

-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/FireAnt.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")

-- Set up the background
include("Scenery/Background.lua")
include("Scenery/TunnelBackground.lua")

-- Set up map platforms
include("Scenery/Pit.lua")
include("Scenery/Platforms.lua")
include("Scenery/Plane1.lua")
include("Scenery/Plane2.lua")


-- Create an enemy
fireAntY = -4856
fireAntX = 100
fireant1 = FireAnt(Vector2(5056, fireAntY))
fireant2 = FireAnt(Vector2(5456 + fireAntX, fireAntY))
fireant3 = FireAnt(Vector2(5856 + fireAntX, fireAntY))
fireant4 = FireAnt(Vector2(6256 + fireAntX, fireAntY))
fireant5 = FireAnt(Vector2(6656 + fireAntX, fireAntY))
fireant6 = FireAnt(Vector2(7056 + fireAntX, fireAntY))
fireant7 = FireAnt(Vector2(7456 + fireAntX, fireAntY))
fireant8 = FireAnt(Vector2(7856 + fireAntX, fireAntY))

--Platform Black Ants
blackant1 = BlackAnt(Vector2(1080, 0), "platPatrol")
blackant2 = BlackAnt(Vector2(1560, -130), "platPatrol")
blackant3 = BlackAnt(Vector2(2310, -580), "platPatrol")
blackant4 = BlackAnt(Vector2(3160, -480), "platPatrol")
blackant5 = BlackAnt(Vector2(1720, 260), "platPatrol")
blackant6 = BlackAnt(Vector2(2520, 100), "platPatrol")
blackant7 = BlackAnt(Vector2(3160, 575), "platPatrol")
blackant8 = BlackAnt(Vector2(3610, 50), "platPatrol")

--Platform Spiders
spider9 = Spider(Vector2(1080, -250))
spider10 = Spider(Vector2(1560, -430))
spider11 = Spider(Vector2(2310, -930))
spider12 = Spider(Vector2(3160, -680))
spider13 = Spider(Vector2(1720, 60))
spider14 = Spider(Vector2(2520, -100))
spider15 = Spider(Vector2(3160, 375))
spider16 = Spider(Vector2(3610, -250))

bee = Bee(Vector2(0,400))


--Pit Ramp Spiders
spider1 = Spider(Vector2(2060, -1950))
spider2 = Spider(Vector2(2260, -2200))
spider3 = Spider(Vector2(2460, -2400))
spider4 = Spider(Vector2(2660, -2600))
spider5 = Spider(Vector2(2860, -2800))
spider6 = Spider(Vector2(3060, -3000))
spider7 = Spider(Vector2(3260, -3200))
spider8 = Spider(Vector2(3460, -3400))

-- Set the bounds for this map
map.bounds:Add(Vector2(-1400, -80))
map.bounds:Add(Vector2(-1400, 3000))
map.bounds:Add(Vector2(9960, 3000))
map.bounds:Add(Vector2(9960, 300))