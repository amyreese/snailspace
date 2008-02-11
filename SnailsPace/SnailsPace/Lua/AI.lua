
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
	if (self.direction.X == 0 ) then
		self.direction = Vector2(1,0)
	end
	if (self.position.X > pt1) then
		self.direction = Vector2( -1, 0 )
		self.acceleration = 1280
	elseif (self.position.X < pt2) then
		self.direction = Vector2( 1, 0 )
		self.acceleration = 1280
	end	
end

--[[ Fly between two points ]]--
function AI.vertPatrol(self, pt1, pt2)
	self:setSprite("Fly")
	if (self.direction.Y == 0 ) then
		self.direction = Vector2(0,1)
	end
	if (self.position.Y > pt1) then
		self.direction = Vector2(0, -1)
	elseif (self.position.Y < pt2) then
		self.direction = Vector2(0, 1)
	end
end

--[[ Fly between two points ]]--
function AI.diagonalPatrol(self, pt1, pt2)
    --[[
	if (self.direction.Y == 0 ) then
		self.direction = Vector2(0,1)
	end
	if (self.position.Y > pt1) then
		self.direction = Vector2(0, -1)
	elseif (self.position.Y < pt2) then
		self.direction = Vector2(0, 1)
	end
	]]--
	self:setSprite("Fly")
	if self.state.movingDown then
		if self.position.Y > pt2.Y then
			self.direction = Vector2( pt2.X - self.position.X, pt2.Y - self.position.Y )
		else
			self.state.movingDown = false
		end
	else
		self.state.movingDown = false
		if self.position.Y < pt1.Y then
			self.direction = Vector2( pt1.X - self.position.X, pt1.Y - self.position.Y )
		else
			self.state.movingDown = true
		end
	end
end

--[[ Attack Helix ]]--
function AI.shootDirectlyAtHelix(self, gameTime)
    self:ShootAt(helix.position, gameTime)
end

function AI.shootSpiderPattern(self, gameTime)
	self:ShootFanAt(Vector2(self.position.X, self.position.Y-1), 8, .5, gameTime)
end

--[[Patrol on a platform]]--
function AI.platformPatrol(self)
	self.horizontalFriction = 3840.0
	AI.patrol(self, self.startPosition.X + 10, self.startPosition.X - 10)
end
