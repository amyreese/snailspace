xOffset = 0
yOffset = 0

WorldBuilding.BuildSection( { width=40, xOffset=-512, yOffset=-160, sprite=grassSprite, xOverlap=20, xSizeMod=-32, ySizeMod=-32 } )
WorldBuilding.BuildObject( { xOffset=0.1 * treetrunkImage.size.X + 2000, yOffset=0.40 * treetrunkImage.size.Y, sprite=treetrunkSprite, collidable=false, layerOffset=10 } )

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
sign.position = Vector2(200,0)
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

rock.position = Vector2( 760, 0 )
rock.size = rsprite.image.size
rock.affectedByGravity = false
rock.collidable = true	

map.objects:Add(rock)


nameCnt = 4
names = {}
possibleNames = { "joe", "josh", "pat", "brian", "john", "simon"  }
nameCounts = { 0,0,0,0,0,nameCnt - 2 }
for i=1,#possibleNames * nameCnt do
	rndnum = math.random()
	for n=1,#possibleNames do
		if rndnum < n / #possibleNames then
			if nameCounts[n] < nameCnt then
				table.insert( names, possibleNames[n] )
				nameCounts[n] = nameCounts[n] + 1
			else
				i = i - 1
			end
			rndnum = 0
		end
	end
end

for i,v in ipairs( names ) do
	name = Name( Vector2( xOffset + i * 420, yOffset + 256 + 96 * i ), v )
end

--[[
yMod = -64
for i=0,creditsImage.blocks.X-1 do
	rot = math.random() / 8 + 0.2
	csprite = creditsSprite:clone()
	csprite.frame = i
	csprite.animationStart = i
	csprite.animationEnd = i
	WorldBuilding.BuildObject( {xOffset=1024 + i*(creditsImage.size.X - 8), yOffset=yMod, sprite=csprite, ySizeMod=-creditsImage.size.X/3, rotation=rot } )
	yMod = yMod + math.sin(rot) * 448
	endLevelX = 1024 + i*(creditsImage.size.X - 8) + 256
	endLevelY = yMod + 256
end

WorldBuilding.BuildObject( { xOffset=endLevelX, yOffset=endLevelY, sprite=exitPortalSprite, layer=0, collidable=false } )
EndLevel.BuildLevelEnd( endLevelX, endLevelY )

WorldBuilding.BuildObject( { xOffset=endLevelX, yOffset=128, sprite=exitPortalSprite, layer=0, collidable=false } )
EndLevel.BuildLevelEnd( endLevelX, 128 )
]]--