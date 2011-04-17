
function DummyActor_Update()
end

function DummyActor_Event()
	BeginBattle("BattleData\\BridgeBattle.xml", DummyActor_Update);
end

function DummyActor()
	Me.UpdateEvent = DummyActor_Update
	Me.Event = DummyActor_Event
end