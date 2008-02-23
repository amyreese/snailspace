queenBlocks = {}
queenBlocksTriggers = {}

--Cave Block
xOffset = 245
yOffset = -134.0
for x=0,6 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSpriteS:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImageS.size.X - 32, dirtImageS.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2( ( (x*4) + xOffset ) * 32, ( -2.5 + yOffset ) * 32 )
 dirtObj.layer = 0.6
 map.objects:Add(dirtObj)
 queenBlocks[x] = dirtObj
end

for x=0,5 do
	local trig = Traps.SaltPile( 10000, -4200, queenBlocks[x].size.X, queenBlocks[x].size.Y, 0, 40)
	queenBlocksTriggers[x] = trig
end


blockwatcher = FireAnt(Vector2(8608, -4368), "blockWatch")
blockwatcher.sprites["Stand"].visible = false