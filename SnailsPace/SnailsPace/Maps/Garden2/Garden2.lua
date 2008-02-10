library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2(0,0)
--startPosition = Vector2(800, -2500) -- Salt Pit Middle Left
--startPosition = Vector2(1400, -2500) -- Salt Pit Middle Right
--startPosition = Vector2(3900, -7620) -- Salt Ramp Bottom
player = Player( startPosition )

-- Enemy Character Definitions
include("../Garden/Enemies/Bee.lua")
include("../Garden/Enemies/FireAnt.lua")
include("../Garden/Enemies/BlackAnt.lua")
include("../Garden/Enemies/Spider.lua")

-- Trigger Definitions
include("Triggers/SavePoints.lua")
include("Triggers/Powerups.lua")
include("Triggers/Traps.lua")

-- Scenery Sprite Definitions
include("Scenery/Sprites.lua")

-- Set up the background
include("Scenery/Background.lua")
include("Scenery/TunnelBackground.lua")

-- Set up map regions
function LoadArea( name )
	include("Scenery/" .. name .. "/" .. name .. ".lua")
end
LoadArea("StartArea")
LoadArea("Fence")
LoadArea("SaltPit")



-- Set the bounds for this map
map.bounds:Add(Vector2(-1400, -80))
map.bounds:Add(Vector2(-1400, 3000))
map.bounds:Add(Vector2(9960, 3000))
map.bounds:Add(Vector2(9960, 300))