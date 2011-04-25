-- VillageActors.lua
---------------------------------------------------------------------------------
-- Contains script functions for the actors in the Village level

-- Load the game assembly so that we can use the GameState enumeration
luanet.load_assembly("FSEGame")
GameState = luanet.import_type("FSEGame.GameState")

function Village_IsPostTorchConstruction()
	local TORCH_ID = 'T_Village'
	
	-- Find out whether the torch has been constructed or not,
	-- T_Village will be set to 10 if it has
	if PSIsNumber(TORCH_ID) then
		return PSGetNumber(TORCH_ID) == 10
	end
	
	return false
end

function Village_IsPostBattle()
	if PSIsNumber('S_VillageGuardianDefeated') then
		return PSGetNumber('S_VillageGuardianDefeated') >= 10
	end
	
	return false
end

function Village_HasSpokenToVernado()
	if PSIsNumber('Q_BuildTorch') then
		return PSGetNumber('Q_BuildTorch') == 10
	end
	
	return false
end

function Village_HasAcquiredStick()
	if PSIsNumber('I_Stick') then
		return PSGetNumber('I_Stick') == 10
	end
	
	return false
end

function Village_HasAcquiredCoal()
	if PSIsNumber('I_Coal') then
		return PSGetNumber('I_Coal') == 10
	end
	
	return false
end

function Village_GiveCoal()
	PSSetNumber('I_Coal', 10)
	
	if Village_HasAcquiredStick() then
		Village_GiveTorch()
	end
end

function Village_GiveStick()
	PSSetNumber('I_Stick', 10)
	
	if Village_HasAcquiredCoal() then
		Village_GiveTorch()
	end
end

function Village_GiveTorch()
	PSSetNumber('T_Village', 10)
	-- TODO: Give BasicAttack move
	
	PlayDialogue('FSEGame\\Dialogues\\TorchObtained.xml')
end

---------------------------------------------------------------------------------
-- Vernado
-- 
---------------------------------------------------------------------------------

function Vernado_Event()
	-- Play a different dialogue depending on whether the torch has been
	-- constructed or not
	if not Village_IsPostTorchConstruction() then
		PSSetNumber('Q_BuildTorch', 10)
		PlayDialogue(Me:GetProperty('DefaultDialogue'))
	else
		PlayDialogue(Me:GetProperty('PostTorchDialogue'))
	end
end

function Vernado()
	Me.Event = Vernado_Event
end

---------------------------------------------------------------------------------
-- Markus
-- 
---------------------------------------------------------------------------------

Markus_Victory = false

function Markus_GiveStick()
	RegisterDefaultDialogueHandlers()
	SetGameState(GameState.Exploring)
	
	Village_GiveStick()
end

function Markus_LevelLoaded()
	-- TODO: Restore health
	
	if Markus_Victory then
		UnregisterDefaultDialogueHandlers()
		SetGameState(GameState.Cutscene)
		
		PlayDialogueAndRun('FSEGame\\Dialogues\\MarkusPostTutorial.xml', Markus_GiveStick)
	else
		RegisterDefaultDialogueHandlers()
		PlayDialogue('FSEGame\\Dialogues\\MarkusPostTutorialLoss.xml')
	end
end

function Markus_BattleEnded(victory)
	Markus_Victory = victory
	LoadLevelAndRun('Levels\\House3.xml', 'TutorialEnd', Markus_LevelLoaded)
end

function Markus_IntroDialogueEnded()
	RegisterDefaultDialogueHandlers()
	
	BeginBattle('BattleData\\Tutorial1.xml', Markus_BattleEnded)
end

function Markus_Event()
	if Village_HasAcquiredStick() then
		PlayDialogue('FSEGame\\Dialogues\\MarkusPostStick.xml')
	elseif Village_HasSpokenToVernado() then
		UnregisterDefaultDialogueHandlers()
		SetGameState(GameState.Cutscene)
		
		PlayDialogueAndRun('FSEGame\\Dialogues\\MarkusStick.xml', Markus_IntroDialogueEnded)
	else
		PlayDialogue('FSEGame\\Dialogues\\MarkusDefault.xml')
	end
end

function Markus()
	Me.Event = Markus_Event
end

---------------------------------------------------------------------------------
-- Maro
-- 
---------------------------------------------------------------------------------

function Maro_DialogueEnded()
	RegisterDefaultDialogueHandlers()
	SetGameState(GameState.Exploring)
	
	Village_GiveCoal()
end

function Maro_Event()
	if Village_HasAcquiredCoal() then
		PlayDialogue('FSEGame\\Dialogues\\MaroPostCoal.xml')
	elseif Village_HasSpokenToVernado() then
		UnregisterDefaultDialogueHandlers()
		SetGameState(GameState.Cutscene)
		
		PlayDialogueAndRun('FSEGame\\Dialogues\\MaroCoal.xml', Maro_DialogueEnded)
	else
		PlayDialogue('FSEGame\\Dialogues\\MaroDefault.xml')
	end
end

function Maro()
	Me.Event = Maro_Event
end

---------------------------------------------------------------------------------
-- Post Battle Actors
-- 
---------------------------------------------------------------------------------

function Village_PostBattleActor_Update()
	if Village_IsPostBattle() then
		Me.Visible = true
		Me.Passable = false
	else
		Me.Visible = false
		Me.Passable = true
	end
end

function Village_PostBattleActor_Event()
	PlayDialogue(Me:GetProperty('Dialogue'))
end

function Village_PostBattleActor()
	Me.UpdateEvent = Village_PostBattleActor_Update
	Me.Event = Village_PostBattleActor_Event
end
