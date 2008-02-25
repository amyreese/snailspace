xOffset = 0
yOffset = 0

WorldBuilding.BuildSection( { width=40, xOffset=-512, yOffset=-160, sprite=grassSprite, xOverlap=20, xSizeMod=-32, ySizeMod=-32 } )
WorldBuilding.BuildObject( { xOffset=0.1 * treetrunkImage.size.X + 3500, yOffset=0.40 * treetrunkImage.size.Y, sprite=treetrunkSprite, collidable=false, layerOffset=10 } )

sign = GameObject()

ssprite = savepointSprite:clone()
ssprite.frame = 2
ssprite.animationStart = 2
ssprite.animationEnd = 2
sign.sprites:Add("sign", ssprite)

asprite = savepointSprite:clone()
asprite.frame = 3
asprite.animationStart = 3
asprite.animationEnd = 3
sign.sprites:Add("arrow", asprite)

sign.collidable = false
sign.position = Vector2(-400,230)
map.objects:Add(sign)

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

rock.position = Vector2( -400, 0 )
rock.size = rsprite.image.size
rock.affectedByGravity = false
rock.collidable = true	

map.objects:Add(rock)

for i=0,creditsImage.blocks.X * creditsImage.blocks.Y - 1 do
	csprite = creditsSprite:clone()
	csprite.frame = i
	csprite.animationStart = i
	csprite.animationEnd = i
	WorldBuilding.BuildObject( {xOffset=100 + i*(creditsImage.size.X), yOffset=600, sprite=csprite, ySizeMod=-creditsImage.size.X/3, rotation=0, collidable=false } )
	endLevelX = 1024 + i*(creditsImage.size.X - 8)
end

sign = GameObject()

ssprite = savepointSprite:clone()
ssprite.frame = 2
ssprite.animationStart = 2
ssprite.animationEnd = 2
sign.sprites:Add("sign", ssprite)

sprite = Sprite()
sprite.image = Image()
sprite.image.filename = "Resources/Textures/GalleryTable"
sprite.image.blocks = Vector2(3,10)
sprite.image.size = Vector2(170,100)

sprite.position = Vector2(0,0)
sprite.effect = "Resources/Effects/effects"
sprite.layerOffset = -0.1
sprite.visible = true
sprite.frame = 2
sprite.animationStart = 2
sprite.animationEnd = 2

sign.sprites:Add("title", sprite)
sign.collidable = false
sign.position = Vector2(endLevelX-200,10)
sign.layer = 2
map.objects:Add(sign)

WorldBuilding.BuildObject( { xOffset=endLevelX + 200, yOffset=128, sprite=exitPortalSprite, layer=0, collidable=false } )
EndLevel.BuildLevelEnd( endLevelX + 200, 128 )
