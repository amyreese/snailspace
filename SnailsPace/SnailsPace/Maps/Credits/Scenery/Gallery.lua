
xOffset = 3200
yOffset = 160

galleryImage = Image()
galleryImage.filename = "Resources/Textures/GalleryTable"
galleryImage.blocks = Vector2(1,10)
galleryImage.size = Vector2(512,100)

function GalleryCharacter(position, character)
	charpos = Vector2(position.X, position.Y + 150)
	
	sprite = Sprite()
	sprite.image = galleryImage
	sprite.position = Vector2(0,0)
	sprite.effect = "Resources/Effects/effects"
	sprite.layerOffset = -0.1
	sprite.visible = true
	
	if character == "bee" then
		char = Bee(charpos)
		sprite.frame = 2
	elseif character == "blackant" then
		char = BlackAnt(charpos)
		sprite.frame = 1
	elseif character == "fireant" then
		char = FireAnt(charpos)
		sprite.frame = 3
	elseif character == "princess" then
		char = Queen(charpos)
		char.scale = Vector2(0.8, 0.8)
		sprite.frame = 5
	elseif character == "queen" then
		char = Queen(charpos)
		sprite.frame = 7
	elseif character == "shaker" then
		char = Shaker(charpos)
		sprite.frame = 6
	elseif character == "spider" then
		char = Spider(charpos)
		sprite.frame = 4
	elseif character == "title" then
		sprite.image = Image()
		sprite.image.filename = "Resources/Textures/GalleryTable"
		sprite.image.blocks = Vector2(3,10)
		sprite.image.size = Vector2(170,100)
		sprite.frame = 1
	else
		return nil
	end		
	
	sprite.animationStart = sprite.frame
	sprite.animationEnd = sprite.frame
	
	plat = GameObject()
	if character ~= "title" then
		plat.sprites:Add("wood", woodSprite)
		plat.collidable = true
	else
		plat.sprites:Add("sign", ssprite)
		plat.collidable = false
		plat.layer = 2
	end
	plat.size = woodImage.size
	plat.sprites:Add("title", sprite)
	plat.position = position
	
	map.objects:Add( plat )
end

GalleryCharacter(Vector2(xOffset-400, 10), "title")
GalleryCharacter(Vector2(xOffset+(0*600),yOffset), "blackant")
GalleryCharacter(Vector2(xOffset+(0.5*600),yOffset+250), "bee")
GalleryCharacter(Vector2(xOffset+(1*600),yOffset), "fireant")
GalleryCharacter(Vector2(xOffset+(1.5*600),yOffset+250), "spider")
GalleryCharacter(Vector2(xOffset+(2*600),yOffset), "princess")
GalleryCharacter(Vector2(xOffset+(2.5*600),yOffset+250), "shaker")
GalleryCharacter(Vector2(xOffset+(3*600),yOffset), "queen")