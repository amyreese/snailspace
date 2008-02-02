
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

-- Set up map platforms
include("Scenery/Pit.lua")
include("Scenery/Platforms.lua")
include("Scenery/Plane1.lua")
include("Scenery/Plane2.lua")


-- Create an enemy
fireant = FireAnt(Vector2(1120, 260))

--blackant = BlackAnt()
--blackant.position = Vector2(256, 0)

--bee = Bee()
--bee.position = Vector2(256, 256)

--spider = Spider()
--spider.position = Vector2(384, 0)

-- Set the bounds for this map
map.bounds:Add(Vector2(-1400, -80))
map.bounds:Add(Vector2(-1400, 3000))
map.bounds:Add(Vector2(9960, 3000))
map.bounds:Add(Vector2(9960, 300))