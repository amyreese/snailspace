
--Boss Area Falling Platforms
 keystone = GameObject()
 dirtObjSprite = dirtSpriteS:clone()
 dirtObjSprite.frame = 0
 keystone.sprites:Add("dirt", dirtObjSprite)
 keystone.size = Vector2(dirtImageS.size.X - 32, dirtImageS.size.Y - 32)
 keystone.rotation = 0.0
 keystone.position = Vector2(8750, 500)
 keystone.layer = 0.6
 map.objects:Add(keystone)
 
 Traps.BossBounds( 8750, 300, 500, 500, 0, keystone)

for x=0,8 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSpriteS:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImageS.size.X - 32, dirtImageS.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2(8750, ((x+1) * 97) + 500)
 dirtObj.layer = 0.6
 dirtObj.affectedByGravity = true
 map.objects:Add(dirtObj)
end

for x=0,15 do
 dirtObj = GameObject()
 dirtObjSprite = dirtSpriteS:clone()
 dirtObjSprite.frame = 0
 dirtObj.sprites:Add("dirt", dirtObjSprite)
 dirtObj.size = Vector2(dirtImageS.size.X - 32, dirtImageS.size.Y - 32)
 dirtObj.rotation = 0.0
 dirtObj.position = Vector2(8750 + (x*74), ((x+10) * 97) + 500)
 dirtObj.layer = 0.6
 dirtObj.affectedByGravity = true
 map.objects:Add(dirtObj)
end
