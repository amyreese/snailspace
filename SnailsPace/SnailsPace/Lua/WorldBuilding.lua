--[[
	WorldBuilding.lua
	A library of World Building related functions useful in our application
]]--

WorldBuilding = {}

WorldBuilding.defaultLayer = 5

-- Build a ramp
function WorldBuilding.BuildRampDEPR(rampLength, rampXOffset, rampYOffset, rampSprite, rampAngle, rampOverlap, layer, collidable)
	args = args or {}
	args.overlap = rampOverlap or 0
	args.collidable = collidable ~= false
	args.layer = layer or WorldBuilding.defaultLayer
	args.layerOffset = args.layerOffset or 0
	args.buildDown = args.buildDown or false
	args.xSizeMod = args.xSizeMod or 0
	args.ySizeMod = args.ySizeMod or 0
	args.length = rampLength or 1
	args.sprite = rampSprite
	args.rotation = rampAngle
	args.xOffset = rampXOffset
	args.yOffset = rampYOffset
	WorldBuilding.BuildRamp(args)
end

function WorldBuilding.BuildRamp(args)
	args = args or {}
	args.overlap = args.overlap or 0
	args.collidable = args.collidable ~= false
	args.layer = args.layer or WorldBuilding.defaultLayer
	args.layerOffset = args.layerOffset or 0
	args.buildDown = args.buildDown or false
	args.xSizeMod = args.xSizeMod or 0
	args.ySizeMod = args.ySizeMod or 0
	args.length = args.length or 1

	yTweak = math.sin( args.rotation ) * ( args.sprite.image.size.X - args.overlap )
	xTweak = math.cos( args.rotation ) * ( args.sprite.image.size.X - args.overlap )
	for x = 0, args.length - 1 do
		xPos = x * xTweak + args.xOffset
		yPos = x * yTweak + args.yOffset
		WorldBuilding.BuildObject( { collidable=args.collidable, layer=args.layer, layerOffset=args.layerOffset, xSizeMod=args.xSizemod, ySizeMod=args.ySizeMod, sprite=args.sprite, xOffset=xPos, yOffset=yPos, spriteName=args.spriteName, rotation=args.rotation} )
	end
end

-- Build a section
function WorldBuilding.BuildSection(args)
	args = args or {}
	args.xOverlap = args.xOverlap or 0
	args.yOverlap = args.yOverlap or 0
	args.collidable = args.collidable ~= false
	args.layer = args.layer or WorldBuilding.defaultLayer
	args.layerOffset = args.layerOffset or 0
	args.buildDown = args.buildDown or false
	args.xSizeMod = args.xSizeMod or 0
	args.ySizeMod = args.ySizeMod or 0
	args.width = args.width or 1
	args.height = args.height or 1

	yMod = 1
	if args.buildDown then
		yMod = -1
	end
	for x = 0, args.width - 1 do
		for y = 0, args.height - 1 do
			xPos = x * ( args.sprite.image.size.X - args.xOverlap ) + args.xOffset
			yPos = yMod * y * ( args.sprite.image.size.Y - args.yOverlap ) + args.yOffset
			WorldBuilding.BuildObject( { collidable=args.collidable, layer=args.layer, layerOffset=args.layerOffset, xSizeMod=args.xSizemod, ySizeMod=args.ySizeMod, sprite=args.sprite, xOffset=xPos, yOffset=yPos, spriteName=args.spriteName, rotation=args.rotation} )
		end
	end
end

function WorldBuilding.BuildObject( args )
	args = args or {}
	args.xOverlap = args.xOverlap or 0
	args.yOverlap = args.yOverlap or 0
	args.collidable = args.collidable ~= false
	args.layer = args.layer or WorldBuilding.defaultLayer
	args.layerOffset = args.layerOffset or 0
	args.xSizeMod = args.xSizeMod or 0
	args.ySizeMod = args.ySizeMod or 0
	args.rotation = args.rotation or 0
	args.spriteName = args.spriteName or "sprite"

	object = GameObject()
	sprite = args.sprite:clone()
	
	object.sprites:Add(args.spriteName, sprite)
	object.size = Vector2( sprite.image.size.X + args.xSizeMod, sprite.image.size.Y + args.ySizeMod )
	object.rotation = args.rotation
	object.position = Vector2( args.xOffset + xOffset, args.yOffset + yOffset )
	object.collidable = args.collidable
	object.layer = args.layer + args.layerOffset

	map.objects:Add(object)
	return object
end
function WorldBuilding.BuildObjectDEPR(objectXOffset, objectYOffset, objectSprite, objectSpriteName, layer, rotation, xSizeMod, ySizeMod)
	return WorldBuilding.BuildObject( {xOffset=objectXOffset, yOffset=objectYOffset, sprite=objectSprite, spriteName=objectSpriteName, layer=layer, rotation=rotation, xSizeMod=xSizeMod, ySizeMod=ySizeMod } )
end