
--[[
	Background.lua
	Defines the map's background scenery.
]]--

-- Generic background image
BackgroundImage = Image()
BackgroundImage.filename = "Resources/Textures/Garden"
BackgroundImage.blocks = Vector2(16.0, 12.0)
BackgroundImage.size = Vector2(384.0, 384.0)

-- Generic background sprite
BackgroundSprite = Sprite()
BackgroundSprite.image = BackgroundImage
BackgroundSprite.visible = true
BackgroundSprite.effect = "Resources/Effects/effects"
BackgroundSprite.animationStart = 0
BackgroundSprite.animationEnd = 0
BackgroundSprite.frame = 0
BackgroundSprite.animationDelay = 0.0
BackgroundSprite.timer = 0.0

-- Background offsets
xOffset = -40
yOffset = -20

-- Create the background with multiple sprites
for x = 0, BackgroundImage.blocks.X - 1 do
	for y = 0, BackgroundImage.blocks.Y - 1 do
		object = GameObject()
		sprite = BackgroundSprite:clone()
		
		sprite.animationStart = x + BackgroundImage.blocks.X * ( BackgroundImage.blocks.Y - 1 - y )
		sprite.animationEnd = sprite.animationStart
		sprite.frame = sprite.animationStart
		
		object.sprites:Add("Background", sprite)
		object.position = Vector2( ( x * 12 + xOffset ) * 32, ( y * 12 + yOffset ) * 32 )
		object.layer = 100
		object.collidable = false
		
		map.objects:Add(object)
	end
end
