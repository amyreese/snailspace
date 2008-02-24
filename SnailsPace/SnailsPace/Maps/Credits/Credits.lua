library('Weapons')
library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2(0,0)
player = Player( startPosition, "generic" )

rotmod = 5
function newRotation()
	return ( math.random() - 0.5 ) / rotmod;
end

-- Enemy Character Definitions
include("Enemies/Name.lua")

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

LoadArea("Credits")

-- Set the bounds for this map
map.bounds:Add(Vector2(-512, -196))
map.bounds:Add(Vector2(-512, 5200))
map.bounds:Add(Vector2(10000, 5200))
map.bounds:Add(Vector2(10000, -196))
map.bounds:Add(Vector2(-512, -196))

EndLevel.BuildLevelEnd( 9856, 0 )