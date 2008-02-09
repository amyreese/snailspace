xOffset = saltPitXOffset
yOffset = saltPitYOffset

-- Salt Ramp Fuel
yTweak = math.sin( -0.65 ) * ( 768 )
xTweak = math.cos( -0.65 ) * ( 768 )
for x = 0, 2 do
	rndNum = ( math.random() - 0.5 ) / 2
	Powerups.BuildFuelPowerup( 1824 + xTweak * x + xTweak * rndNum, - 5392 + yTweak * x + yTweak * rndNum )
end
for x = 0, 2 do
	rndNum = ( math.random() - 0.5 ) / 2
	Powerups.BuildHealthPowerup( 1824 + xTweak * x + xTweak * rndNum, - 5392 + yTweak * x + yTweak * rndNum )
end