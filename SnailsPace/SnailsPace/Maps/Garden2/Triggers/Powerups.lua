Powerups = {}

function Powerups.BuildFuelPowerup( powerupX, powerupY )
	local trig = Trigger()
	trig.position = Vector2( powerupX + xOffset, powerupY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( 128, 128 ), trig.position, 0 )
	trig.state = {}
	trig.state.unused = true
	map.triggers:Add(trig)

	local powerupObj = WorldBuilding.BuildObject( { xOffset=powerupX, yOffset=powerupY, sprite=fuelSprite, layer=0, collidable=false } )

	function trig.state:trigger( character, gameTime )
		Powerups.TriggerFuelPowerup( trig, powerupObj, character, gameTime )
	end

end

function Powerups.TriggerFuelPowerup( trigger, powerupObj, character, gameTime )
	if character == Player.helix then
		if trigger.state.unused then
			Player.helix.fuel = Player.helix.fuel + Player.helix.maxFuel / 4
			powerupObj:setSprite("")
			trigger.state.unused  = false
		end
	end
end

function Powerups.BuildHealthPowerup( powerupX, powerupY )
	local trig = Trigger()
	trig.position = Vector2( powerupX + xOffset, powerupY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( 128, 128 ), trig.position, 0 )
	trig.state = {}
	trig.state.unused = true
	map.triggers:Add(trig)

	local powerupObj = WorldBuilding.BuildObject( { xOffset=powerupX, yOffset=powerupY, sprite=healthSprite, layer=0, collidable=false } )

	function trig.state:trigger( character, gameTime )
		Powerups.TriggerHealthPowerup( trig, powerupObj, character, gameTime )
	end

end

function Powerups.TriggerHealthPowerup( trigger, powerupObj, character, gameTime )
	if character == Player.helix then
		if trigger.state.unused then
			Player.helix.health = Player.helix.health + Player.helix.maxHealth / 4
			powerupObj:setSprite("")
			trigger.state.unused  = false
		end
	end
end