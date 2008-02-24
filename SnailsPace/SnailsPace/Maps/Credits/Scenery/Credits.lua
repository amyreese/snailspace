xOffset = 0
yOffset = 0

WorldBuilding.BuildSection( { width=100, xOffset=-512, yOffset=-160, sprite=grassSprite, xOverlap=20, xSizeMod=-32, ySizeMod=-32 } )

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