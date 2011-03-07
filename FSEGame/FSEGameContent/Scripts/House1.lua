-- House1.lua
------------------------------------------------------------------------------
-- This script is executed whenever an event in the House1.xml level is
-- triggered by the character controller.
-- "id" will be set to the name of the event

if id == "House1Exit" then
	LoadLevel("Levels\\Test.xml", "House1Exit")
end