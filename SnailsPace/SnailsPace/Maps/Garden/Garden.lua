
-- Enemy Character Definitions
include("Enemies/Bee.lua")
include("Enemies/FireAnt.lua")
include("Enemies/BlackAnt.lua")
include("Enemies/Spider.lua")

-- Set up the background
include("Scenery/Background.lua")

-- Set up map platforms
include("Scenery/Platforms.lua")
include("Scenery/Plane1.lua")
include("Scenery/Plane2.lua")

-- Create an enemy
fireant = FireAnt()
fireant.position = Vector2(256, 0)

blackant = BlackAnt()
blackant.position = Vector2(512, 0)

bee = Bee()
bee.position = Vector2(512, 512)

spider = Spider()
spider.position = Vector2(768, 0)