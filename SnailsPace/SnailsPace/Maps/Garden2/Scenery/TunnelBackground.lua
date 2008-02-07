
--[[
	Background.lua
	Defines the map's background scenery.
]]--

-- Generic background image
BackgroundImage = Image()
BackgroundImage.filename = "Resources/Textures/Tunnel"
BackgroundImage.blocks = Vector2(16.0, 12.0)
BackgroundImage.size = Vector2(768.0, 768.0)

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
xOffset = -BackgroundImage.size.X
yOffset = -BackgroundImage.size.Y * (BackgroundImage.blocks.Y - 0.25);

-- Create the background with multiple sprites
for x = 0, BackgroundImage.blocks.X - 1 do
	for y = 0, BackgroundImage.blocks.Y - 1 do
		object = GameObject()
		sprite = BackgroundSprite:clone()
		
		sprite.animationStart = x + BackgroundImage.blocks.X * ( BackgroundImage.blocks.Y - 1 - y )
		sprite.animationEnd = sprite.animationStart
		sprite.frame = sprite.animationStart
		
		object.sprites:Add("Background", sprite)
		object.position = Vector2( x * BackgroundImage.size.X + xOffset, y * BackgroundImage.size.Y + yOffset )
		object.layer = 100
		object.collidable = false
		
		map.objects:Add(object)
	end
end
