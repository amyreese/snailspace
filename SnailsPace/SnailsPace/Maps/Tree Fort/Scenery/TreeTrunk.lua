xOffset = 0
yOffset = 0

WorldBuilding.BuildObject( { xOffset=0.1 * treetrunkImage.size.X, yOffset=0.42 * treetrunkImage.size.Y, sprite=treetrunkSprite, collidable=false, layerOffset=5 } )

WorldBuilding.BuildSection( { height=4, width=18, xOffset=-1536, yOffset=-160, sprite=grassSprite, xOverlap=20, yOverlap=32, xSizeMod=-32, ySizeMod=-32, buildDown=true } )
WorldBuilding.BuildObject( { xOffset=-1436, yOffset=-384 + fencePostImage.size.Y / 2, sprite=fencePostSprite, xSizeMod=-16, ySizeMod=0, rotation=0 } )

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

rock.position = Vector2( xOffset + 1500, yOffset )
rock.size = rsprite.image.size
rock.affectedByGravity = false
rock.collidable = true	

map.objects:Add(rock)

Powerups.BuildWeaponPowerup( xOffset + 2000, 50, "minigun", 100 )

rotmod = 2
height = 6
for i=1,height do
	WorldBuilding.BuildObject( { xOffset=0, yOffset=448 * i, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
end

SavePoints.BuildSavePoint( xOffset, yOffset + 448 * 4 + 100 )

blackant = BlackAnt(Vector2(xOffset + 64, yOffset + 448 * 2 + 100), "platPatrol")
spider = Spider(Vector2(xOffset, yOffset + 448 * 4 - 80))
spider = Spider(Vector2(xOffset + 64, yOffset + 448 * 6 - 104))

spider = Spider(Vector2(xOffset - 300, yOffset + 448 * 7 - 80))
spider = Spider(Vector2(xOffset + 300, yOffset + 448 * 7 - 80))
blackant = BlackAnt(Vector2(xOffset - 500, yOffset + 448 * 7 + 100), "platPatrol")
blackant = BlackAnt(Vector2(xOffset + 500, yOffset + 448 * 7 + 100), "platPatrol")

bee = Bee( Vector2( xOffset - 1200, yOffset + 800 ), "swarmHelixA" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 1200, yOffset + 800 ), "swarmHelixA" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset - 1200, yOffset + 900 ), "swarmHelixB" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 1200, yOffset + 900 ), "swarmHelixB" )
bee.state.attacking = true

bee = Bee( Vector2( xOffset - 800, yOffset + 2200 ), "swarmHelixA" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 800, yOffset + 2200 ), "swarmHelixA" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset - 800, yOffset + 2400 ), "swarmHelixB" )
bee.state.attacking = true
bee = Bee( Vector2( xOffset + 800, yOffset + 2400 ), "swarmHelixB" )
bee.state.attacking = true
	

