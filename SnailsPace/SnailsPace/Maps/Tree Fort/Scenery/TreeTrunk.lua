xOffset = 0
yOffset = 0

WorldBuilding.BuildSection( { height=4, width=18, xOffset=-1536, yOffset=-160, sprite=grassSprite, xOverlap=20, yOverlap=32, xSizeMod=-32, ySizeMod=-32, buildDown=true } )

rotmod = 2
height = 6
for i=1,height do
	WorldBuilding.BuildObject( { xOffset=0, yOffset=448 * i, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
end

spider = Spider(Vector2(xOffset + 64, yOffset + 448 * height - 104))

