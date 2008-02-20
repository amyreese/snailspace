
fallingPlats = {}
--Boss Area Falling Platforms
for x=0,1 do
 dirtObj = GameObject()
 dirtObj.name = "fallingPlatform"
 dirtObjSprite = dirtSpriteS:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImageS.size.X - 32, dirtImageS.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2(9000, (x+1) * 500)
 dirtObj.layer = 0.6
 map.objects:Add(dirtObj)
 fallingPlats[x] = dirtObj
end
