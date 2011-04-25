-- ForestActors.lua
---------------------------------------------------------------------------------
-- Contains script functions for the actors in the Forest level

function Hermit_IsQuestInProgress()
	if PSIsNumber('Q_Hermit') then
		return PSGetNumber('Q_Hermit') == 10
	end
	
	return false
end

function Hermit_QuestGiven()
	RegisterDefaultDialogueHandlers()
	PSSetNumber('Q_Hermit', 10)
end

function Hermit_Event()
	UnregisterDefaultDialogueHandlers()
	PlayDialogueAndRun('FSEGame\\Dialogues\\DLC1\\HermitIntro.xml', Hermit_QuestGiven)
end

function Hermit()
	Me.Event = Hermit_Event
end