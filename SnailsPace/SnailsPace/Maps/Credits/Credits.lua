library('Weapons')
library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2(-300,0)
player = Player( startPosition, "generic" )

weapon = Weapon.load("stinger")
weapon.ammunition = -1
weapon.cooldown = 200
player.helix:AddWeapon(weapon)

weapon = Weapon.load("minigun")
weapon.ammunition = -1
player.helix:AddWeapon(weapon)

weapon = Weapon.load("flamethrower")
weapon.ammunition = -1
player.helix:AddWeapon(weapon)

weapon = Weapon.load("grenadelauncher")
weapon.ammunition = -1
player.helix:AddWeapon(weapon)

player.helix:NextWeapon()

rotmod = 5
function newRotation()
	return ( math.random() - 0.5 ) / rotmod;
end

-- Enemy Character Definitions
include("Enemies/Name.lua")

include("Enemies/Bee.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/FireAnt.lua")
include("Enemies/Queen.lua")
include("Enemies/Shaker.lua")
include("Enemies/Spider.lua")

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
LoadArea("Devs")
LoadArea("Gallery")

-- Set the bounds for this map
map.bounds:Add(Vector2(-512, -196))
map.bounds:Add(Vector2(-512, 2800))
map.bounds:Add(Vector2(9400, 2800))
map.bounds:Add(Vector2(9400, -196))
map.bounds:Add(Vector2(-512, -196))

--EndLevel.BuildLevelEnd( 5800, 0 )