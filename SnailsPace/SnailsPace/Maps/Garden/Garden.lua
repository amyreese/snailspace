library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2(0,0)
-- startPosition = Vector2(800, -2500) -- Salt Pit Middle Left
-- startPosition = Vector2(1400, -2500) -- Salt Pit Middle Right
-- startPosition = Vector2(3700, -7620) -- Salt Ramp Bottom
player = Player( startPosition )
include("Triggers/Message.lua")

-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/FireAnt.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")

-- Scenery Sprite Definitions
include("Scenery/Sprites.lua")

-- Set up the background
include("Scenery/Background.lua")
include("Scenery/TunnelBackground.lua")

-- Set up map platforms
include("Scenery/SaltPit/SaltPit.lua")
include("Scenery/Platforms.lua")
include("Scenery/Plane1.lua")
include("Scenery/Plane2.lua")


-- Create an enemy
--fireant = FireAnt(Vector2(300, 0))

blackant1 = BlackAnt(Vector2(1280, 300), "patrol")
blackant2 = BlackAnt(Vector2(1760, 70), "platPatrol")
blackant3 = BlackAnt(Vector2(2560, -310), "platPatrol")
blackant4 = BlackAnt(Vector2(3360, -80), "platPatrol")
blackant5 = BlackAnt(Vector2(1920, 660), "platPatrol")
blackant6 = BlackAnt(Vector2(2720, 500), "platPatrol")
blackant7 = BlackAnt(Vector2(3360, 975), "platPatrol")

--bee = Bee()



-- Set the bounds for this map
map.bounds:Add(Vector2(-1400, -80))
map.bounds:Add(Vector2(-1400, 3000))
map.bounds:Add(Vector2(9960, 3000))
map.bounds:Add(Vector2(9960, 300))