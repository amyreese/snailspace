
xOffset = 3200
yOffset = 150

galleryImage = Image()
galleryImage.filename = "Resources/Textures/GalleryTable"
galleryImage.blocks = Vector2(1,10)
galleryImage.size = Vector2(512,100)

function GalleryCharacter(position, character)
	charpos = Vector2(position.X, position.Y + 100)
	
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
	elseif character == "helix" then
		char = RogueHelix(charpos)
		sprite.frame = 8
	elseif character == "princess" then
		char = Queen(charpos)
		char.scale = Vector2(0.8, 0.8)
		sprite.frame = 5
	elseif character == "queen" then
		char = Queen(charpos)
		sprite.frame = 7
	elseif character == "shaker" then
		charpos = Vector2(position.X, position.Y + 200)
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

yOffset = 200
GalleryCharacter(Vector2(xOffset-400, 10), "title")
GalleryCharacter(Vector2(xOffset+(0*600),yOffset), "blackant")
GalleryCharacter(Vector2(xOffset+(1*600),yOffset+100), "bee")
GalleryCharacter(Vector2(xOffset+(2*600),yOffset), "fireant")
GalleryCharacter(Vector2(xOffset+(3*600),yOffset+100), "spider")
GalleryCharacter(Vector2(xOffset+(4*600),yOffset), "princess")
GalleryCharacter(Vector2(xOffset+(5*600),yOffset+100), "queen")
GalleryCharacter(Vector2(xOffset+(6*600),yOffset), "shaker")
GalleryCharacter(Vector2(xOffset+(7*600)+50,yOffset+50), "helix")

--[[
GalleryCharacter(Vector2(xOffset+(0*650),yOffset), "blackant")
GalleryCharacter(Vector2(xOffset+(0.5*650),yOffset+230), "bee")
GalleryCharacter(Vector2(xOffset+(1*650),yOffset), "fireant")
GalleryCharacter(Vector2(xOffset+(1.5*650),yOffset+230), "spider")
GalleryCharacter(Vector2(xOffset+(2*650),yOffset), "princess")
GalleryCharacter(Vector2(xOffset+(2.5*650),yOffset+230), "queen")
GalleryCharacter(Vector2(xOffset+(3.2*650),yOffset), "shaker")
GalleryCharacter(Vector2(xOffset+(4.2*650),yOffset+100), "helix")
]]--