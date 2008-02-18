
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
function Weapons:stinger(weapon)
	weapon.name = "Stinger"
	weapon.ammunition = -1
	weapon.cooldown = 700
	weapon.state = { velocity = 0 }
	weapon.sprite = Weapons.weaponSprite(3)
	weapon.slot = 1
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet()
		bullet.explosion = Explosion()
		
		bullet.sprites:Add("Bullet", Weapons.bulletSprite(1))
		bullet.size = Weapons.bulletImage.size
		bullet.scale = Vector2(0.5, 0.5)
		bullet.damage = 2
		
		if shooter == Player.helix then
			self.velocity = 250
		end
		Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
	end
end

--[[ Minigun, brutal fire rate, loud, fast ]]--
function Weapons:minigun(weapon)
	weapon.name = "Minigun"
	weapon.ammunition = 200
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
end

function Weapons:grenadelauncher( weapon )
	weapon.name = "Grenade Launcher"
	weapon.slot = 3
	weapon.cooldown = 400
	weapon.state = {}
	weapon.sprite = Weapons.weaponSprite(3)
	weapon.ammunition = -1
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet()
		bullet.explosion = Explosion()
		
		bullet.sprites:Add("Bullet", Weapons.bulletSprite(0))
		bullet.size = Weapons.bulletImage.size
		bullet.scale = Vector2(1,1)
		bullet.damage = 10
		bullet.affectedByGravity = true
		bullet.bounceable = true
		bullet.bounceTime = 4000
		
		Weapons.shootSingleBullet(bullet, 60, shooter, targetPosition)
	end
end

--[[ Flamethrower!!! Rawr. ]]--
function Weapons:flamethrower( weapon )
	weapon.name = "Flamethrower"
	weapon.slot = 3
	weapon.cooldown = 150
	weapon.cue = "flamethrower"
	weapon.state = { velocity = 100 }
	weapon.sprite = Weapons.weaponSprite(2)
	weapon.ammunition = 50
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		bullet = Bullet()
		bullet.explosion = Explosion()
		bullet.destroy = false;
		
		bullet.sprites:Add("Bullet", Weapons.bulletSprite(28,31,0.1))
		bullet.size = Weapons.bulletImage.size
		bullet.scale = Vector2(2.8,1.8)
		bullet.damage = 3
		bullet.range = 600;
		
		Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
	end
end
	

--[[ Basic weapon, single shot ]]--
function Weapons:generic( weapon, v )
	weapon.name = "Gun"
	weapon.cooldown = 100
	weapon.state = { velocity=v or 250 }
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
end

--[[ Fanned shot weapon, multiple shot ]]--
function Weapons:fanshot( weapon, n, o, v )
	weapon.sprite = Weapons.weaponSprite(0)
	weapon.ammunition = 50
	weapon.cooldown = 100
	weapon.state = { velocity=v or 128, number=n or 8, offset=o or 0.3 }
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)     
		transform = Matrix.Multiply(Matrix.Multiply(Matrix.CreateTranslation(Vector3(-shooter.position.X, -shooter.position.Y, 0)), Matrix.CreateRotationZ(-self.offset * ((self.number - 1) / 2))), Matrix.CreateTranslation(Vector3(shooter.position, 0)))

		targetPosition = Vector2.Transform(targetPosition, transform)
		
		transform = Matrix.Multiply(Matrix.Multiply(Matrix.CreateTranslation(Vector3(-shooter.position.X, -shooter.position.Y, 0)), Matrix.CreateRotationZ(self.offset)), Matrix.CreateTranslation(Vector3(shooter.position, 0)))
        
        for i = 1, self.number, 1 do
			bullet = Bullet()
			bullet.explosion = Explosion()
	                
			bullet.sprites:Add("Bullet", Weapons.bulletSprite(5))
			bullet.size = Weapons.bulletImage.size		
			bullet.damage = 1
	        
			Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
			
			targetPosition = Vector2.Transform(targetPosition, transform)
		end
		
		bullet = Bullet()
		bullet.explosion = Explosion()
                
		bullet.sprites:Add("Bullet", Weapons.bulletSprite(5))
		bullet.size = Weapons.bulletImage.size		
		bullet.damage = 1
        
        targetPosition = Vector2(shooter.position.X - 100, shooter.position.Y)
		Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
		
		bullet = Bullet()
		bullet.explosion = Explosion()
                
		bullet.sprites:Add("Bullet", Weapons.bulletSprite(5))
		bullet.size = Weapons.bulletImage.size		
		bullet.damage = 1
        
        targetPosition = Vector2(shooter.position.X + 100, shooter.position.Y)
		Weapons.shootSingleBullet(bullet, self.velocity, shooter, targetPosition)
	end
end

--[[ Scattered shot weapon ]]--
function Weapons:scattershot( weapon, n, v )
	weapon.sprite = Weapons.weaponSprite(0)
	weapon.cooldown = 100
	weapon.state = { velocity=v or 128, number=n or 8 }
	
	function weapon.state:ShootAt(shooter, targetPosition, gameTime)
		for i = 1, self.number, 1 do
			-- Do math
		end
	end
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
