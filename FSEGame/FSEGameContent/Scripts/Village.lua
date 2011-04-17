-- Village.lua
------------------------------------------------------------------------------
-- This script is executed whenever an event in the Village.xml level is
-- triggered by the character controller.
-- "id" will be set to the name of the event

function EndBridgeBattle(victory)
	if victory then
		PlayOutro()
	else
		GameOver()
	end
end

if id == "EnterPlayerHouse" then
	LoadLevel("Levels\\PlayerHouse.xml", "Entrance")
elseif id == "EnterHouse1" then
	LoadLevel("Levels\\House1.xml", "Entrance")
elseif id == "EnterHouse2" then
	LoadLevel("Levels\\House2.xml", "Entrance")
elseif id == "EnterHouse3" then
	LoadLevel("Levels\\House3.xml", "Entrance")
elseif id == "EnterHouse4" then
	LoadLevel("Levels\\House4.xml", "Entrance")
elseif id == "BridgeBattle" then
	BeginBattle("BattleData\\BridgeBattle.xml", EndBridgeBattle);
end