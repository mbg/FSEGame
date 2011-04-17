-- TestActors.lua
---------------------------------------------------------------------------------
-- Contains various test functions for the ScriptedActor class

---------------------------------------------------------------------------------
-- DummyActor
-- Very basic actor that tests the functionality of the ScriptedActor class,
-- doesn't actually do anything useful
---------------------------------------------------------------------------------
function DummyActor_Update()
	-- This gets called every frame
end

function DummyActor_Event()
	-- This gets called when the user interacts with the actor
	BeginBattle("BattleData\\BridgeBattle.xml", DummyActor_Update);
end

function DummyActor()
	-- This gets called when an instance of the actor is created
	-- "Me" is set to the instance handle
	Me.UpdateEvent = DummyActor_Update
	Me.Event = DummyActor_Event
end

---------------------------------------------------------------------------------
-- ScriptedVernado
-- Pretty much the same as the Vernado NPC class, but scripted. Used to test the
-- persistent storage script functions.
---------------------------------------------------------------------------------
function ScriptedVernado_IsPostTorchConstruction()
	-- Find out whether the torch has been constructed or not,
	-- T_Village will be set to 10 if it has
	if PSIsNumber('T_Village') then
		return PSGetNumber('T_Village') == 10
	end
	
	return false
end

function ScriptedVernado_Event()
	-- Play a different dialogue depending on whether the torch has been
	-- constructed or not
	if not ScriptedVernado_IsPostTorchConstruction() then
		PSSetNumber('Q_BuildTorch', 10)
		PlayDialogue(Me:GetProperty('DefaultDialogue'))
	else
		PlayDialogue(Me:GetProperty('PostTorchDialogue'))
	end
end

function ScriptedVernado()
	Me.Event = ScriptedVernado_Event
end