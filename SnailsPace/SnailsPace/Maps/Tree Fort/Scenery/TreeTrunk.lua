xOffset = 0
yOffset = 0

WorldBuilding.BuildObject( { xOffset=0.1 * treetrunkImage.size.X, yOffset=0.42 * treetrunkImage.size.Y, sprite=treetrunkSprite, collidable=false, layerOffset=5 } )

WorldBuilding.BuildSection( { height=4, width=18, xOffset=-1536, yOffset=-160, sprite=grassSprite, xOverlap=20, yOverlap=32, xSizeMod=-32, ySizeMod=-32, buildDown=true } )
WorldBuilding.BuildObject( { xOffset=-1436, yOffset=-384 + fencePostImage.size.Y / 2, sprite=fencePostSprite, xSizeMod=-16, ySizeMod=0, rotation=0 } )


rotmod = 2
height = 6
for i=1,height do
	WorldBuilding.BuildObject( { xOffset=0, yOffset=448 * i, sprite=woodSprite, xSizeMod=-16, ySizeMod=-16, rotation=newRotation() } )
end

spider = Spider(Vector2(xOffset + 64, yOffset + 448 * height - 104))

