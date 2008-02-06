
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
include("SaltPit/SaltPit.lua")
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