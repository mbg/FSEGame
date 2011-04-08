-- Village2.lua
------------------------------------------------------------------------------
-- This script is executed whenever an event in the Village2.xml level is
-- triggered by the character controller.
-- "id" will be set to the name of the event

if id == "ExitToVillage1" then
	LoadLevel("Levels\\Village.xml", "PlayerHouseExit")
end