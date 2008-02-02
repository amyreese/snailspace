
-- Set up the trigger
trig = Trigger()
trig.location = Rectangle(-500,200,2000,2000)
trig.state = {}
map.triggers:Add(trig)

-- Trigger function
function trig.state:trigger( character, gameTime )
	print(gameTime.TotalGameTime:ToString() .. ": " .. character.position:ToString())
end
