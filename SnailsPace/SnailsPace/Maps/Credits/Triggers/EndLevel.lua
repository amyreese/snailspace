EndLevel = {}


function EndLevel.BuildLevelEnd( endLevelX, endLevelY )
	local trig = Trigger()
	trig.position = Vector2( endLevelX + xOffset, endLevelY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( 256, 256 ), trig.position, 0 )
	trig.state = { used=false }
	map.triggers:Add(trig)
	function trig.state:triggerIn( character, gameTime )
		if not used then
			EndLevel.TriggerEndLevel( trig, character, gameTime )
		end
	end
end

function EndLevel.TriggerEndLevel( trigger, character, gameTime )
	if character == Player.helix then
		trigger.state.used = true
		Engine:EndLevel()
	end
end