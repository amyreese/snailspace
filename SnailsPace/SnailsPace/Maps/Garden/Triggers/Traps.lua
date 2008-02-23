Traps = {}

function Traps.SaltPile( trapX, trapY, trapWidth, trapHeight, trapRotation, trapDamage )
	trapDamage = trapDamage or 1
	trapRotation = trapRotation or 0
	local trig = Trigger()
	trig.position = Vector2( trapX + xOffset, trapY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( trapWidth / 2, trapHeight ), trig.position, trapRotation )
	trig.state = {}
	map.triggers:Add(trig)

	function trig.state:trigger( character, gameTime )
		Traps.DamageHelix( trig, trapDamage, character, gameTime )
	end
	return trig
end

function Traps.DamageHelix( trigger, damage, character, gameTime )
	if character == Player.helix then
		Player.helix:takeDamage( damage )
	end
end

function Traps.BossBounds( trapX, trapY, trapWidth, trapHeight, trapRotation, keystone)
	trapRotation = trapRotation or 0
	local trig = Trigger()
	trig.position = Vector2( trapX + xOffset, trapY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( trapWidth / 2, trapHeight ), trig.position, trapRotation )
	trig.state = {}
	trig.state.unused = true
	map.triggers:Add(trig)

	function trig.state:trigger( character, gameTime )
		if(trig.state.unused) then
			keystone.affectedByGravity = true
			trig.state.unused = false
			--TODO: Zoom Out to Show entire boss battle
		end
	end
	return trig
end

function Traps.MiniBossEscape( trapX, trapY, trapWidth, trapHeight, trapRotation, blockStones)
	trapRotation = trapRotation or 0
	local trig = Trigger()
	trig.position = Vector2( trapX + xOffset, trapY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( trapWidth / 2, trapHeight ), trig.position, trapRotation )
	trig.state = {}
	trig.state.unused = true
	map.triggers:Add(trig)

	function trig.state:trigger( character, gameTime )
		if(trig.state.unused) then
			for x=0,6 do
				blockStones[x].name = "fallingPlatform"
			end
		end
	end
	return trig
end