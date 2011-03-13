-- Test.lua
------------------------------------------------------------------------------
-- This script is executed whenever an event in the Test.xml level is
-- triggered by the character controller.
-- "id" will be set to the name of the event

if id == "EnterPlayerHouse" then
	LoadLevel("Levels\\PlayerHouse.xml", "Entrance")
elseif id == "House1Exit" then
	LoadLevel("Levels\\Test.xml", "House1Exit")
elseif id == "TestHouseDoor2" then
	LoadLevel("Levels\\House2.xml", "Default")
elseif id == "House2Exit" then
	LoadLevel("Levels\\Test.xml", "House2Exit")
elseif id == "BridgeBattle" then
	BeginBattle("BattleData\\BridgeBattle.xml");
end