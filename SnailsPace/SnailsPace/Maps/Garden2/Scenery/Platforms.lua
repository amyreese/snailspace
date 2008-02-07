imge = Image()
imge.filename = "Resources/Textures/wood"
imge.blocks = Vector2(1.0, 1.0)
imge.size = Vector2(500.0, 100.0)

sprit = Sprite()
sprit.image = imge
sprit.visible = true
sprit.effect = "Resources/Effects/effects"

platOffX = 40
platOffY = 0

plat1 = GameObject()
plat1Sprite = sprit:clone()
plat1Sprite.frame = 0
plat1.sprites:Add("Grass", plat1Sprite)
plat1.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat1.position = Vector2( ( 0+platOffX ) * 32, 5 * 32 )
plat1.layer = 0.5
map.objects:Add(plat1)

plat2 = GameObject()
plat2Sprite = sprit:clone()
plat2Sprite.frame = 0
plat2.sprites:Add("Grass", plat2Sprite)
plat2.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat2.position = Vector2( (15+platOffX) * 32, -4.5 * 32 )
plat2.layer = 0.5
map.objects:Add(plat2)

plat3 = GameObject()
plat3Sprite = sprit:clone()
plat3Sprite.frame = 0
plat3.sprites:Add("Grass", plat3Sprite)
plat3.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat3.position = Vector2( (40+platOffX)*32, -15 * 32 )
plat3.layer = 0.5
map.objects:Add(plat3)

plat4 = GameObject()
plat4Sprite = sprit:clone()
plat4Sprite.frame = 0
plat4.sprites:Add("Grass", plat4Sprite)
plat4.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat4.position = Vector2( (65+platOffX)*32, -8 * 32 )
plat4.layer = 0.5
map.objects:Add(plat4)

plat5 = GameObject()
plat5Sprite = sprit:clone()
plat5Sprite.frame = 0
plat5.sprites:Add("Grass", plat5Sprite)
plat5.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat5.position = Vector2( (20+platOffX)*32, 15*32 )
plat5.layer = 0.5
map.objects:Add(plat5)

plat6 = GameObject()
plat6Sprite = sprit:clone()
plat6Sprite.frame = 0
plat6.sprites:Add("Grass", plat6Sprite)
plat6.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat6.position = Vector2( (45+platOffX)*32, 10*32 )
plat6.layer = 0.5
map.objects:Add(plat6)

plat7 = GameObject()
plat7Sprite = sprit:clone()
plat7Sprite.frame = 0
plat7.sprites:Add("Grass", plat7Sprite)
plat7.size = Vector2(imge.size.X - 16, imge.size.Y - 16)
plat7.position = Vector2( (65+platOffX)*32, 25*32 )
plat7.layer = 0.5
map.objects:Add(plat7)