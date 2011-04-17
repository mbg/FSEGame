// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using FSEGame.Engine;
using LuaInterface;
using Microsoft.Xna.Framework;
using FSEGame.Editor;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// 
    /// </summary>
    [SuggestedProperty("Function", @"DummyActor")]
    [SuggestedProperty("States", "2")]
    [SuggestedProperty("Tileset", "Tilesets\\GenericNPC.xml")]
    [SuggestedProperty("Dialogue", "FSEGame\\Dialogues\\TestDialogue.xml")]
    public class ScriptedActor : GenericNPC
    {
        #region Instance Members
        private LuaFunction actorFunction = null;
        private LuaFunction updateFunction = null;
        private LuaFunction eventFunction = null;
        #endregion

        #region Properties
        public LuaFunction UpdateEvent
        {
            get
            {
                return this.updateFunction;
            }
            set
            {
                this.updateFunction = value;
            }
        }
        public LuaFunction Event
        {
            get
            {
                return this.eventFunction;
            }
            set
            {
                this.eventFunction = value;
            }
        }
        #endregion

        public ScriptedActor(ActorProperties properties)
            : base(properties)
        {
            this.actorFunction = 
                GameBase.Singleton.ScriptManager.State.GetFunction(properties.Properties["Function"]);

            if (this.actorFunction != null)
            {
                GameBase.Singleton.ScriptManager.State["Me"] = this;

                this.actorFunction.Call();
            }
            else
            {
                GameBase.Singleton.Log.WriteLine(
                    "ScriptedActor",
                    "WARNING: Could not find function {0}", properties.Properties["Function"]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.updateFunction != null)
            {
                this.updateFunction.Call();
            }

            base.Update(gameTime);
        }

        protected override void PerformAction()
        {
            if (this.eventFunction != null)
            {
                this.eventFunction.Call();
            }
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::