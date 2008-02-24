xOffset = 0
yOffset = 0

WorldBuilding.BuildSection( { width=100, xOffset=-512, yOffset=-160, sprite=grassSprite, xOverlap=20, xSizeMod=-32, ySizeMod=-32 } )

possibleNames = { "joe", "josh", "pat", "brian", "john", "simon"  }
nameCounts = { 0,0,0,0,0,0 }
names = {}
nameCnt = 3
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
	name = Name( Vector2( xOffset + i * 512, yOffset + 256 ), v )
end

