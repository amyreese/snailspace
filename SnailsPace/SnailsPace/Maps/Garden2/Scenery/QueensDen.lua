--[[
TODOs
Add in Queen Ant
Add in patrolling ants
Finish Exit Path
]]--

xOffset = 6084
yOffset = -8296

-- **** Entrance Path ****
-- Floor
WorldBuilding.BuildSection({ width=5, xOffset=0, yOffset=0, sprite=dirtSprite, xOverlap=20 })

-- Ants
ant = BlackAnt( Vector2( xOffset + 364, yOffset + 192 ), "patrol" )

-- Ceiling
WorldBuilding.BuildSection({ width=3, xOffset=0, yOffset=512, sprite=dirtSprite, xOverlap=20 })

-- Right Wall
WorldBuilding.BuildSection({ height=4, xOffset=1180, yOffset=0, sprite=dirtSprite, yOverlap=20 })

-- **** Entrance Path Double-back ****
-- Floor
WorldBuilding.BuildRamp({ length=3, xOffset=412, yOffset=576, sprite=dirtSprite, overlap=20, rotation=MathHelper.Pi - 0.35 })

-- Ants
ant = BlackAnt( Vector2( xOffset + 364, yOffset + 804 ), "patrol" )

-- Ceiling
WorldBuilding.BuildRamp({ length=4, xOffset=1180, yOffset=832, sprite=dirtSprite, overlap=20, rotation=MathHelper.Pi - 0.35 })

-- Left Wall
WorldBuilding.BuildSection({ height=3, xOffset=0, yOffset=896, sprite=dirtSprite, yOverlap=20 })

-- **** Queen's Den ****
-- Left Wall
WorldBuilding.BuildRamp({ length=2, xOffset=0, yOffset=1540, sprite=dirtSprite, overlap=20, rotation=1.15 })
WorldBuilding.BuildRamp({ length=2, xOffset=172, yOffset=1884, sprite=dirtSprite, overlap=20, rotation=0.45 })

-- Floor
WorldBuilding.BuildSection({ width=7, xOffset=600, yOffset=1156, sprite=dirtSprite, xOverlap=20 })
WorldBuilding.BuildRamp({ length=3, xOffset=2220, yOffset=1156, sprite=dirtSprite, overlap=20, rotation=0.30 })

-- Ants
ant = BlackAnt( Vector2( xOffset + 920, yOffset + 1320 ), "patrol" )
ant = BlackAnt( Vector2( xOffset + 1240, yOffset + 1320 ), "patrol" )
ant = BlackAnt( Vector2( xOffset + 1560, yOffset + 1320 ), "patrol" )

-- Queen Ant
ant = Queen( Vector2( xOffset + 2260, yOffset + 1420 ) )

-- Ceiling
WorldBuilding.BuildSection({ width=5, xOffset=600, yOffset=2052, sprite=dirtSprite, xOverlap=20 })
WorldBuilding.BuildRamp({ length=3, xOffset=1652, yOffset=2052, sprite=dirtSprite, overlap=20, rotation=-0.65 })
WorldBuilding.BuildRamp({ length=4, xOffset=2220, yOffset=1680, sprite=dirtSprite, overlap=20, rotation=1.20 })

-- Right Wall
WorldBuilding.BuildRamp({length=6, xOffset=2732, yOffset=1412, sprite=dirtSprite, overlap=20, rotation=1.35 })

-- **** Exit Path ****
EndLevel.BuildLevelEnd( 2220 + 512, 2052 + 256 )