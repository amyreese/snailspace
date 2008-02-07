xOffset = saltPitXOffset
yOffset = saltPitYOffset

-- Salt Pit Spiders
platformOffset = -104
-- Platform 1
spider = Spider(Vector2(xOffset + 1280 + platformOffset, yOffset - 438))
spider.state.attacking = true
-- Platform 2
spider = Spider(Vector2(xOffset + 600 + platformOffset, yOffset - 1152))
spider.state.attacking = true
-- Platform 3
spider = Spider(Vector2(xOffset + 550 + platformOffset, yOffset - 2176))
spider.state.attacking = true
-- Platform 5
spider = Spider(Vector2(xOffset + 808 + platformOffset, yOffset - 3956))
spider.state.attacking = true
-- Platform 6
spider = Spider(Vector2(xOffset + 1600 + platformOffset, yOffset - 4212))
spider.state.attacking = true

-- Salt Ramp Spiders
yTweak = math.sin( -0.65 ) * ( 156 )
xTweak = math.cos( -0.65 ) * ( 156 )
for x = -1, 14 do
	rndNum = math.random() / 2
	spider = Spider(Vector2(xOffset + 1824 + xTweak * x + xTweak * rndNum, yOffset - 5264 + yTweak * x + yTweak * rndNum))
	spider.state.attacking = true
end
for x = 0, 5 do
	spider = Spider(Vector2(xOffset + 3800 + 256 * (x + math.random() / 2), yOffset - 6637))
	spider.state.attacking = true
end

-- Low path Spiders
for x = 0, 16 do
	spider = Spider(Vector2(xOffset + 960 + 256 * (x + math.random() / 2), yOffset -7208))
	spider.state.attacking = true
end
