
-- Set up the trigger
trig = Trigger()
trig.position = Vector2(500,500)
trig.bounds = GameObjectBounds( Vector2( 1000,1000 ), Vector2( 500,500 ), 0 )
trig.state = {}
map.triggers:Add(trig)

-- Trigger function
function trig.state:trigger( character, gameTime )
	print(gameTime.TotalGameTime:ToString() .. ": " .. character.position:ToString())
end
