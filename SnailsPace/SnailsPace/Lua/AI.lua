
--[[
	AI.lua
	Library routines useful with all AI characters.
]]--

library('Math')

-- AI Table
AI = {}

--[[ Stop the character's movement ]]--
function AI.stop( self )
	self.direction = Vector2(0,0)
end

--[[ Determine if the character can 'see' Helix ]]--
-- TODO: Implement a 'better' algorithm
function AI.canSeeHelix( self, dmin )
	if ( dmin == nil ) then
		dmin = 15.0
	end
	
	return ( Math.distance( self, helix ) < dmin )
end

--[[ Move the AI towards Helix with certain limits ]]--
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
	
    self.desiredMaxVelocity = vmax;
	self.direction = Vector2( cx, cy )
end

--[[ Patrol between two points ]]--
function AI.patrol(self, pt1, pt2)
	self:setSprite("Walk")
	if (self.position.X > pt1) then
		self.direction = Vector2( -1, 0 )
	elseif (self.position.X < pt2) then
		self.direction = Vector2( 1, 0 )
	end
    self.desiredMaxVelocity = vmax;
end