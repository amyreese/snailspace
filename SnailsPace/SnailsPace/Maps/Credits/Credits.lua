library('Weapons')
library('WorldBuilding')

-- Player creation and starting position
startPosition = Vector2(0,0)
player = Player( startPosition, "generic" )
player.helix:AddWeapon(Weapon.load("stinger"))
player.helix:AddWeapon(Weapon.load("minigun"))
player.helix:AddWeapon(Weapon.load("flamethrower"))
player.helix:AddWeapon(Weapon.load("grenadelauncher"))

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

rock = GameObject()
rsprite = Sprite()
rsprite.image = Image()
rsprite.image.filename = "Resources/Textures/Rock"
rsprite.image.blocks = Vector2(1,1)
rsprite.image.size = Vector2(512,512)
rsprite.effect = "Resources/Effects/effects"
rsprite.visible = true
rock.sprites:Add("Rock", rsprite)

rbounds = GameObjectBoundsBuilder()
rbounds:AddPoint(Vector2(-190,-40))
rbounds:AddPoint(Vector2(-170,50))
rbounds:AddPoint(Vector2(-100,120))
rbounds:AddPoint(Vector2(-50,160))
rbounds:AddPoint(Vector2(45,170))
rbounds:AddPoint(Vector2(170,100))
rbounds:AddPoint(Vector2(200,40))
rbounds:AddPoint(Vector2(210,-20))
rbounds:AddPoint(Vector2(160,-60))
rbounds:AddPoint(Vector2(0,-80))
rbounds:AddPoint(Vector2(-170,-60))
rock.bounds = rbounds:BuildBounds()

rock.position = Vector2( 760, 0 )
rock.size = rsprite.image.size
rock.affectedByGravity = false
rock.collidable = true	

map.objects:Add(rock)

-- Set the bounds for this map
map.bounds:Add(Vector2(-512, -196))
map.bounds:Add(Vector2(-512, 5200))
map.bounds:Add(Vector2(10000, 5200))
map.bounds:Add(Vector2(10000, -196))
map.bounds:Add(Vector2(-512, -196))

EndLevel.BuildLevelEnd( 9856, 0 )