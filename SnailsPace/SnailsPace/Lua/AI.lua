
--[[
	AI.lua
	Library routines useful with all AI characters.
]]--

library('Math')

-- AI Table
AI = {}

-- Move the AI towards Helix with certain limits
function AI.moveToHelix( self, dmax, dmin, vmax )
	if ( vmax == nil ) then
		vmax = self.maxVelocity
	if ( dmin == nil ) then
		dmin = 0.0
	if ( dmax == nil ) then
		dmax = 0.0
	end; end; end;
			
	dth, cx, cy = Math.components( self, helix )
		
	if ( dth < dmin ) then
		cx, cy = -1 * cx, -1 * cy
	elseif ( dth < dmax ) then
		cx, cy = 0, 0
	end
	
	vprime = Vector2( vmax * cx, vmax * cy )
	self.velocity = vprime
end