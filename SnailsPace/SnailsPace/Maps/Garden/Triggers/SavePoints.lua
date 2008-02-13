SavePoints = {}
SavePoints.points = {}
SavePoints.numPoints = 0

function SavePoints.BuildSavePoint( savePointX, savePointY, sprite )
	sprite = sprite or savepointSprite
	local trig = Trigger()
	trig.position = Vector2( savePointX + xOffset, savePointY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( 128, 128 ), trig.position, 0 )
	trig.state = { saved=False }
	local saveObject = WorldBuilding.BuildObject( {xOffset=savePointX, yOffset=savePointY, sprite=sprite, spriteName="savePoint", collidable=false})
	map.triggers:Add(trig)

	function trig.state:triggerIn( character, gameTime )
		if (not saved) then
			SavePoints.TriggerSave( trig, saveObject, character, gameTime )
		end
	end
	SavePoints.points[SavePoints.numPoints] = saveObject
	SavePoints.numPoints = SavePoints.numPoints + 1
	return trig
end

function SavePoints.TriggerSave( trigger, saveObject, character, gameTime )
	if character == Player.helix then
		if saveObject.sprites["savePoint"].frame == 1 then
			for k,v in pairs(SavePoints.points) do
				v.sprites["savePoint"].frame=1
				v.sprites["savePoint"].animationStart=1
				v.sprites["savePoint"].animationEnd=1
			end
			saveObject.sprites["savePoint"].frame=0
			saveObject.sprites["savePoint"].animationStart=0
			saveObject.sprites["savePoint"].animationEnd=0
			Engine.player:save(trigger.position)
			Engine.sound:play("ding1")
			character.health = character.maxHealth
		end
	end
end