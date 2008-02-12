
--[[
	Weapons.lua
	Library of many different weapons.
]]--

-- Weapon Table
Weapons = {}

--[[ Basic bullets, pink shot ]]--
Weapons.genericImage = Image()
Weapons.genericImage.filename = "Resources/Textures/Bullet"
Weapons.genericImage.blocks = Vector2(1.0, 1.0)
Weapons.genericImage.size = Vector2(16.0, 8.0);

Weapons.genericSprite = Sprite();
Weapons.genericSprite.image = Weapons.genericImage;
Weapons.genericSprite.visible = true;
Weapons.genericSprite.effect = "Resources/Effects/effects";

--[[ Basic weapon, single shot ]]--
function Weapons:generic( v )
	weapon = Weapon()
	weapon.cooldown = 100
	weapon.state = { velocity=v or 128 }
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet();
		bullet.explosion = Explosion()
                
        bullet.sprites:Add("Bullet", Weapons.genericSprite)
        bullet.size = Weapons.genericImage.size
        
        bullet.velocity = Vector2.Subtract(targetPosition, shooter.position)
        bullet.velocity = Vector2.Normalize(bullet.velocity)
        
        if ((targetPosition.X - shooter.position.X) < 0) then
			pi = MathHelper.Pi
		else
			pi = 0
		end
		
		bullet.rotation = pi + math.atan((targetPosition.Y - shooter.position.Y) / (targetPosition.X - shooter.position.X));
        bullet.position = Vector2.Add(shooter.position, Vector2.Multiply(bullet.velocity, math.max(shooter.size.X, shooter.size.Y) / 2));
        bullet.maxVelocity = math.max(shooter.terminalVelocity, shooter.maxVelocity) + self.velocity;
        bullet.velocity = Vector2.Multiply(bullet.velocity, bullet.maxVelocity);
        
        bullet.layer = -0.001;
        bullet.damage = 1;
        
        if (shooter == Player.helix) then
			bullet.isPCBullet = true
			Engine.player:shotBullet();
		end
        
        Engine.bullets:Add(bullet);
	end
	
	return weapon;
end

--[[ Fanned shot weapon, multiple shot ]]--
function Weapons:fanshot( n, o, v )
	weapon = Weapon()
	weapon.cooldown = 100
	weapon.state = { velocity=v or 128, number=n, offset=o }
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		if (self.number % 2 == 0) then
			targetPosition.X = targetPosition.X - self.offset * (self.number / 2) - (self.offset / 2)
        else
            targetPosition.X = targetPosition.X - self.offset * (self.number / 2);
        end
        
        for i = 0, self.number, 1 do
			bullet = Bullet();
			bullet.explosion = Explosion()
	                
			bullet.sprites:Add("Bullet", Weapons.genericSprite)
			bullet.size = Weapons.genericImage.size
	        
			bullet.velocity = Vector2.Subtract(targetPosition, shooter.position)
			bullet.velocity = Vector2.Normalize(bullet.velocity)
	        
			if ((targetPosition.X - shooter.position.X) < 0) then
				pi = MathHelper.Pi
			else
				pi = 0
			end
			
			bullet.rotation = pi + math.atan((targetPosition.Y - shooter.position.Y) / (targetPosition.X - shooter.position.X))
			bullet.position = Vector2.Add(shooter.position, Vector2.Multiply(bullet.velocity, math.max(shooter.size.X, shooter.size.Y) / 2))
			bullet.maxVelocity = math.max(shooter.terminalVelocity, shooter.maxVelocity) + self.velocity
			bullet.velocity = Vector2.Multiply(bullet.velocity, bullet.maxVelocity)
	        
			bullet.layer = -0.001
			bullet.damage = 1
	        
			if (shooter == Player.helix) then
				bullet.isPCBullet = true
				Engine.player:shotBullet()
			end
	        
			Engine.bullets:Add(bullet)
			
			targetPosition.X = targetPosition.X + self.offset
		end
	end
	
	return weapon;
end