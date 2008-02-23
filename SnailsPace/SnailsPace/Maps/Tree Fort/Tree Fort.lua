library('Weapons')
library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2(0,0)
--startPosition = Vector2(0, 3136) -- Tree Fort Door
player = Player( startPosition, "generic", "Garden2" )

rotmod = 5
function newRotation()
	return ( math.random() - 0.5 ) / rotmod;
end

-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")
include("Enemies/Hive.lua")

-- Trigger Definitions
include("Triggers/SavePoints.lua")
include("Triggers/Powerups.lua")
include("Triggers/Traps.lua")
include("Triggers/EndLevel.lua")

-- Scenery Sprite Definitions
include("Scenery/Sprites.lua")

-- Set up the background
include("Scenery/Background.lua")

-- Set up map regions
function LoadArea( name )
	include("Scenery/" .. name .. ".lua")
end

LoadArea("TreeTrunk")
LoadArea("Fort")

-- Set the bounds for this map
map.bounds:Add(Vector2(-1536, -80))
map.bounds:Add(Vector2(-1536, 5200))
map.bounds:Add(Vector2(2560, 5200))
map.bounds:Add(Vector2(2560, -80))
