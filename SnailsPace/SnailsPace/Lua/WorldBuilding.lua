--[[
	WorldBuilding.lua
	A library of World Building related functions useful in our application
]]--

WorldBuilding = {}

WorldBuilding.defaultLayer = 5

-- Build a ramp
function WorldBuilding.BuildRamp(rampLength, rampXOffset, rampYOffset, rampSprite, rampAngle, rampOverlap, layer, notCollidable)
	layer = layer or WorldBuilding.defaultLayer
	rampOverlay = rampOverlay or 0
	collidable = notCollidable ~= false
	
	yTweak = math.sin( rampAngle ) * ( rampSprite.image.size.X - rampOverlap )
	xTweak = math.cos( rampAngle ) * ( rampSprite.image.size.X - rampOverlap )
	for x = 0, rampLength - 1 do
		object = WorldBuilding.BuildObject( x * xTweak + rampXOffset, x * yTweak + rampYOffset, rampSprite, nil, layer, rampAngle )
		object.collidable = collidable
	end
end

-- Build a section
function WorldBuilding.BuildSection(sectionWidth, sectionHeight, sectionXOffset, sectionYOffset, sectionSprite, sectionXOverlap, sectionYOverlap, layer, buildDown, notCollidable)
	sectionXOverlap = sectionXOverlap or 0
	sectionYOverlap = sectionYOverlap or 0
	collidable = notCollidable ~= false
	layer = layer or WorldBuilding.defaultLayer
	buildDown = buildDown or false
	yMod = 1
	if buildDown then
		yMod = -1
	end
	for x = 0, sectionWidth - 1 do
		for y = 0, sectionHeight - 1 do
			object = WorldBuilding.BuildObject( x * ( sprite.image.size.X - sectionXOverlap ) + sectionXOffset, yMod * y * ( sprite.image.size.Y - sectionYOverlap ) + sectionYOffset, sectionSprite, nil, layer )
			object.collidable = collidable
		end
	end
end

function WorldBuilding.BuildObject(objectXOffset, objectYOffset, objectSprite, objectSpriteName, layer, rotation, xSizeMod, ySizeMod)
	objectSpriteName = objectSpriteName or "sprite"
	layer = layer or WorldBuilding.defaultLayer
	xSizeMod = xSizeMod or 0
	ySizeMod = ySizeMod or 0
	rotation = rotation or 0
	object = GameObject()
	sprite = objectSprite:clone()
	
	object.sprites:Add(objectSpriteName, sprite)
	object.size = Vector2( sprite.image.size.X + xSizeMod, sprite.image.size.Y + ySizeMod )
	object.rotation = rotation
	object.position = Vector2( objectXOffset + xOffset, objectYOffset + yOffset )
	object.layer = layer

	map.objects:Add(object)
	return object
end
