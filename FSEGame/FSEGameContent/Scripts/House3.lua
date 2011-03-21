-- House3.lua
------------------------------------------------------------------------------
-- This script is executed whenever an event in the House3.xml level is
-- triggered by the character controller.
-- "id" will be set to the name of the event

if id == "Exit" then
	LoadLevel("Levels\\Village.xml", "House3Exit")
end