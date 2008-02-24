levelSplit = ";"
levelSubsplit = ":"


levelsTable = {}
table.insert( levelsTable, { name="The Chloride Conundrum", file="Garden" } )
table.insert( levelsTable, { name="Infiltrating the Fort", file="Tree Fort" } )
table.insert( levelsTable, { name="A Brush with Royalty", file="Garden2" } )
table.insert( levelsTable, { name="Credits", file="Credits" } )

levels = ""
for i,v in ipairs( levelsTable ) do
	if i ~= 1 then
		levels = levels .. levelSplit
	end
	levels = levels .. v.name .. levelSubsplit .. v.file
end

