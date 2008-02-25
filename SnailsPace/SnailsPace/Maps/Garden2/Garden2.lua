library('Weapons')
library('WorldBuilding')

Engine.sound:set("MusicTrack", 1)

-- Scenery Sprite Definitions
include("Scenery/Sprites.lua")

-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/FireAnt.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")
include("Enemies/Queen.lua")

-- Trigger Definitions
include("Triggers/SavePoints.lua")
include("Triggers/Powerups.lua")
include("Triggers/Traps.lua")
include("Triggers/EndLevel.lua")

-- Player creation and starting position
startPosition = Vector2(1536 + 352,fencePostImage.size.Y - 512)
--startPosition = Vector2(0,0)
--startPosition = Vector2(800, -2500) -- Salt Pit Middle Left
--startPosition = Vector2(1400, -2500) -- Salt Pit Middle Right
--startPosition = Vector2(3900, -7620) -- Salt Ramp Bottom
--startPosition = Vector2(6148, -8104) -- Queen's Den Entrance
player = Player( startPosition, "flamethrower", "Credits" )

-- Set up the background
include("Scenery/Background.lua")
include("Scenery/TunnelBackground.lua")

-- Set up map regions
function LoadArea( name )
	include("Scenery/" .. name .. ".lua")
end

LoadArea("StartArea")
LoadArea("Fence")
LoadArea("SaltPit")
LoadArea("QueensDen")

-- Set the bounds for this map
map.bounds:Add(Vector2(-1400, -80))
map.bounds:Add(Vector2(-1400, fencePostImage.size.Y - 384))
map.bounds:Add(Vector2(2000, fencePostImage.size.Y - 384))
