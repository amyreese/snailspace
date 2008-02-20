
-- Set up the trigger
trig = Trigger()
trig.position = Vector2(500,500)
trig.bounds = GameObjectBounds( Vector2( 1000,1000 ), Vector2( 500,500 ), 0.0 )

boundBuilder = GameObjectBoundsBuilder()
boundBuilder:AddPoint( Vector2( -165, -61 ) )
boundBuilder:AddPoint( Vector2( 0, -61 ) )
boundBuilder:AddPoint( Vector2( 180, -61 ) )
boundBuilder:AddPoint( Vector2( 0, 100 ) )
--trig.bounds = boundBuilder:BuildBounds()

trig.state = {}
map.triggers:Add(trig)

-- Trigger function
function trig.state:trigger( character, gameTime )
	Player.helix.health = Player.helix.maxHealth - Player.helix.health
	if Player.helix.health == 0 then
		Player.helix.health = 1
	end
end
