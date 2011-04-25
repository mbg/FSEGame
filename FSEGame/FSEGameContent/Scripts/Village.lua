-- Village.lua
------------------------------------------------------------------------------
-- This script is executed whenever an event in the Village.xml level is
-- triggered by the character controller.
-- "id" will be set to the name of the event

-- Load the game assembly so that we can use the GameState enumeration
luanet.load_assembly("FSEGame")
GameState = luanet.import_type("FSEGame.GameState")

-- This function returns true if the bridge guardian has been defeated or
-- false if not.
function IsGuardianDefeated()
	if PSIsNumber('S_VillageGuardianDefeated') then
		return PSGetNumber('S_VillageGuardianDefeated') >= 10
	end

	return false
end

-- Event handler that runs the village outro sequence.
function RunVillageOutro()
	-- The battle will disable the player and unregister
	-- the default dialogue event handlers, so we have to
	-- undo that
	EnablePlayer()
	RegisterDefaultDialogueHandlers()
	
	PlayDialogue("FSEGame\\Dialogues\\DLC1\\VillageOutro.xml")
end

function EndBridgeBattle(victory)
	if victory then
		--PlayOutro()
		PSSetNumber('S_VillageGuardianDefeated', 10)
		LoadLevelAndRun("Levels\\Village.xml", "FromBridgeBattle", RunVillageOutro)
	else
		GameOver()
	end
end

------------------------------------------------------------------------------------------
-- Perform a different action depending on the ID of the event that was triggered
------------------------------------------------------------------------------------------
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
elseif id == "ToForest" then
	LoadLevel("Levels\\DLC1\\Forest.xml", "FromVillage")
elseif id == "BridgeBattle" then
	-- Do not trigger the bridge battle if the guardian has already been defeated.
	if not IsGuardianDefeated() then
		BeginBattle("BattleData\\BridgeBattle.xml", EndBridgeBattle);
	end
end