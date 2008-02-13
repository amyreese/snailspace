
--[[
	Weapons.lua
	Library of many different weapons.
]]--

-- Weapon Table
Weapons = {}

--[[ Bullet image, contains many bullets ]]--
Weapons.bulletImage = Image()
Weapons.bulletImage.filename = "Resources/Textures/BulletTable"
Weapons.bulletImage.blocks = Vector2(4, 8)
Weapons.bulletImage.size = Vector2(64, 32);

--[[ Weapon image, contains many weapons ]]--
Weapons.weaponImage = Image()
Weapons.weaponImage.filename = "Resources/Textures/WeaponTable"
Weapons.weaponImage.blocks = Vector2(4, 8)
Weapons.weaponImage.size = Vector2(128, 64);

--[[ Basic bullet sprite for a given section of frames ]]--
function Weapons.bulletSprite( frame, frame2, delay )
	frame2 = frame2 or frame
	delay = delay or 0.25
	
	local sprite = Sprite()
	sprite.image = Weapons.bulletImage;
	sprite.visible = true;
	sprite.effect = "Resources/Effects/effects";
	sprite.frame = frame
	sprite.animationStart = frame
	sprite.animationEnd = frame2
	sprite.animationDelay = delay
	
	return sprite
end

--[[ Basic weapon sprite for a given section of frames ]]--
function Weapons.weaponSprite( frame, frame2, delay )
	frame2 = frame2 or frame
	delay = delay or 0.25
	
	local sprite = Sprite()
	sprite.image = Weapons.weaponImage;
	sprite.visible = true;
	sprite.effect = "Resources/Effects/effects";
	sprite.frame = frame
	sprite.animationStart = frame
	sprite.animationEnd = frame2
	sprite.animationDelay = delay
	
	return sprite
end

--[[ Stinger gun, shoots bee's stingers. ]]--
function Weapons:stinger()
	weapon = Weapon()
	weapon.ammunition = -1
	weapon.cooldown = 700
	weapon.state = { velocity = 0 }
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet()
		bullet.explosion = Explosion()
		
		bullet.sprites:Add("Bullet", Weapons.bulletSprite(1))
		bullet.size = Weapons.bulletImage.size
		bullet.scale = Vector2(0.5, 0.5)
		bullet.damage = 2
		
		Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
	end
	
	return weapon
end

--[[ Minigun, brutal fire rate, loud, fast ]]--
function Weapons:minigun()
	weapon = Weapon()
	weapon.name = "Minigun"
	weapon.ammunition = 100
	weapon.cooldown = 15
	weapon.cue = "explode"
	weapon.state = { velocity = 384 }
	weapon.sprite = Weapons.weaponSprite(1)
	weapon.slot = 2
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet()
		bullet.explosion = Explosion()
                
        bullet.sprites:Add("Bullet", Weapons.bulletSprite(0))
        bullet.size = Weapons.bulletImage.size
        bullet.scale = Vector2(0.4,0.4)
        bullet.damage = 1
            
        Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
	end
	
	return weapon
end

--[[ Basic weapon, single shot ]]--
function Weapons:generic( v )
	weapon = Weapon()
	weapon.name = "Gun"
	weapon.cooldown = 100
	weapon.state = { velocity=v or 0 }
	weapon.sprite = Weapons.weaponSprite(0)
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet()
		bullet.explosion = Explosion()
                
        bullet.sprites:Add("Bullet", Weapons.bulletSprite(0))
        bullet.size = Weapons.bulletImage.size
        bullet.scale = Vector2(0.25, 0.25)
        bullet.damage = 1
            
        Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
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
			bullet = Bullet()
			bullet.explosion = Explosion()
	                
			bullet.sprites:Add("Bullet", Weapons.bulletSprite(0))
			bullet.size = Weapons.bulletImage.size
	        
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

--[[ Single shot helper function ]]--
function Weapons.shootSingleBullet( bullet, velocity, shooter, targetPosition ) 
	bullet.velocity = Vector2.Subtract(targetPosition, shooter.position)
    bullet.velocity = Vector2.Normalize(bullet.velocity)
    
    if ((targetPosition.X - shooter.position.X) < 0) then
		pi = MathHelper.Pi
	else
		pi = 0
	end
	
	bullet.rotation = pi + math.atan((targetPosition.Y - shooter.position.Y) / (targetPosition.X - shooter.position.X));
    bullet.position = Vector2.Add(shooter.position, Vector2.Multiply(bullet.velocity, math.max(shooter.size.X, shooter.size.Y) / 2));
    bullet.maxVelocity = math.max(shooter.terminalVelocity, shooter.maxVelocity) + velocity;
    bullet.velocity = Vector2.Multiply(bullet.velocity, bullet.maxVelocity);
    
    bullet.layer = -0.001
    
    if (shooter == Player.helix) then
		bullet.isPCBullet = true
		Engine.player:shotBullet();
	end
    
    Engine.bullets:Add(bullet);
end
