-- ChangeLevel.lua
------------------------------------------------------------------------------
-- Triggered when the player character lands on a tile with a ChangeLevel
-- event associated to it.
-- "id" will be set to the name of the trigger

if id == "TestHouseDoor" then
	LoadLevel("Levels\\House1.xml", "Default")
elseif id == "House1Exit" then
	LoadLevel("Levels\\Test.xml", "House1Exit")
elseif id == "TestHouseDoor2" then
	LoadLevel("Levels\\House2.xml", "Default")
elseif id == "House2Exit" then
	LoadLevel("Levels\\Test.xml", "House2Exit")
end