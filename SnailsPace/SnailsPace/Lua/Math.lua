
--[[
	Math.lua
	A library of Math related functions useful in our application
]]--

-- Math Table
Math = {}

-- Find the distance between two GameObjects
function Math.distance( o1, o2 )
	p1 = o1.position
	p2 = o2.position
	return math.abs( ( ( p2.X - p1.X )^2 + ( p2.Y - p1.Y )^2 )^0.5 )
end

-- Find the movement components toward a target
-- @return distance, component X, component Y
function Math.components( o1, o2 )
	distance = Math.distance( o1, o2 )
	p1 = o1.position
	p2 = o2.position
	
	cx = p2.X - p1.X
	cy = p2.Y - p1.Y
	
	return distance, cx / distance, cy / distance
end