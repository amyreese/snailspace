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

function Traps.BossBounds( trapX, trapY, trapWidth, trapHeight, trapRotation, plats)
	trapRotation = trapRotation or 0
	local trig = Trigger()
	trig.position = Vector2( trapX + xOffset, trapY + yOffset )
	trig.bounds = GameObjectBounds( Vector2( trapWidth / 2, trapHeight ), trig.position, trapRotation )
	trig.state = {}
	map.triggers:Add(trig)

	function trig.state:trigger( character, gameTime )
		for x in plats do
		
		end
	end
	return trig
end