plat1 = GameObject()
plat1Sprite = grassSprite:clone()
plat1Sprite.frame = 0
plat1.sprites:Add("Grass", plat1Sprite)
plat1.position = Vector2( 95, 8 )
plat1.layer = 0.5
plat1.bounds = GameObjectBounds(Rectangle(plat1.position.X, plat1.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat1)

plat2 = GameObject()
plat2Sprite = grassSprite:clone()
plat2Sprite.frame = 0
plat2.sprites:Add("Grass", plat2Sprite)
plat2.position = Vector2( 110, -4.5 )
plat2.layer = 0.5
plat2.bounds = GameObjectBounds(Rectangle(plat2.position.X, plat2.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat2)

plat3 = GameObject()
plat3Sprite = grassSprite:clone()
plat3Sprite.frame = 0
plat3.sprites:Add("Grass", plat3Sprite)
plat3.position = Vector2( 120, -15 )
plat3.layer = 0.5
plat3.bounds = GameObjectBounds(Rectangle(plat3.position.X, plat3.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat3)

plat4 = GameObject()
plat4Sprite = grassSprite:clone()
plat4Sprite.frame = 0
plat4.sprites:Add("Grass", plat4Sprite)
plat4.position = Vector2( 135, -8 )
plat4.layer = 0.5
plat4.bounds = GameObjectBounds(Rectangle(plat4.position.X, plat4.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat4)

plat5 = GameObject()
plat5Sprite = grassSprite:clone()
plat5Sprite.frame = 0
plat5.sprites:Add("Grass", plat5Sprite)
plat5.position = Vector2( 115, 20 )
plat5.layer = 0.5
plat5.bounds = GameObjectBounds(Rectangle(plat5.position.X, plat5.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat5)

plat6 = GameObject()
plat6Sprite = grassSprite:clone()
plat6Sprite.frame = 0
plat6.sprites:Add("Grass", plat6Sprite)
plat6.position = Vector2( 120, 15 )
plat6.layer = 0.5
plat6.bounds = GameObjectBounds(Rectangle(plat6.position.X, plat6.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat6)

plat7 = GameObject()
plat7Sprite = grassSprite:clone()
plat7Sprite.frame = 0
plat7.sprites:Add("Grass", plat7Sprite)
plat7.position = Vector2( 130, 30 )
plat7.layer = 0.5
plat7.bounds = GameObjectBounds(Rectangle(plat7.position.X, plat7.position.Y, grassImage.size.X, grassImage.size.Y))
map.objects:Add(plat7)