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
	return trig
end

function Powerups.TriggerFuelPowerup( trigger, powerupObj, character, gameTime )
	if character == Player.helix then
		if trigger.state.unused then
			Engine.sound:play("ding1")
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
	return trig
end

function Powerups.TriggerHealthPowerup( trigger, powerupObj, character, gameTime )
	if character == Player.helix then
		if trigger.state.unused then
			Engine.sound:play("ding2")
			Player.helix.health = Player.helix.health + Player.helix.maxHealth / 4
			powerupObj:setSprite("")
			trigger.state.unused  = false
		end
	end
end

function Powerups.BuildWeaponPowerup( powerupX, powerupY, weaponname, ammo, cooldownModifier )
	ammo = ammo or 100
	cooldownModifier = cooldownModifier or 1
	local trig = Trigger()
	trig.position = Vector2( powerupX + xOffset, powerupY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( 128, 128 ), trig.position, 0 )
	trig.state = {}
	trig.state.unused = true
	map.triggers:Add(trig)

	local weapon = Weapon.load(weaponname)
	weapon.ammunition = ammo
	weapon.cooldown = weapon.cooldown * cooldownModifier
	
	local powerupObj = WorldBuilding.BuildObject( { xOffset=powerupX, yOffset=powerupY, sprite=weapon.sprite, layer=0, collidable=false } )

	function trig.state:trigger( character, gameTime )
		Powerups.TriggerWeaponPowerup( trig, powerupObj, character, gameTime, weapon )
	end
	return trig
end

function Powerups.TriggerWeaponPowerup( trigger, powerupObj, character, gameTime, weapon )
	if character == Player.helix then
		if trigger.state.unused then
			Engine.sound:play("ding1")
			Player.helix:AddWeapon( weapon )
			powerupObj:setSprite("")
			trigger.state.unused  = false
		end
	end
end

function Powerups.BuildBoostPowerup( powerupX, powerupY, boostTime )
	boostTime = boostTime or 0.5
	local trig = Trigger()
	trig.position = Vector2( powerupX + xOffset, powerupY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( 128, 128 ), trig.position, 0 )
	trig.state = {}
	trig.state.unused = true
	map.triggers:Add(trig)

	local powerupObj = WorldBuilding.BuildObject( { xOffset=powerupX, yOffset=powerupY, sprite=boostSprite, layer=0, collidable=false } )

	function trig.state:trigger( character, gameTime )
		Powerups.TriggerBoostPowerup( trig, powerupObj, character, gameTime, boostTime )
	end
	return trig
end

function Powerups.TriggerBoostPowerup( trigger, powerupObj, character, gameTime, boostTime )
	if character == Player.helix then
		if trigger.state.unused then
			Engine.sound:play("ding1")
			Player.helix.boosting = true
			Player.helix.boostPeriod = boostTime
			Player.helix.fuel = Player.helix.fuel + Player.helix.maxFuel / 8
			powerupObj:setSprite("")
			trigger.state.unused  = false
		end
	end
end