﻿// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
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
    public class ScriptedActor : Actor
    {
        #region Instance Members
        private LuaFunction function;
        #endregion

        #region Properties
        public override bool Passable
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ScriptedActor(ActorProperties properties)
        {
            try
            {
                this.function = FSEGame.Singleton.ScriptManager.State.GetFunction(
                    properties.Properties["Function"]);

                this.function.Call(new Object[] { });
            }
            catch (Exception ex)
            {
                GameBase.Singleton.Log.WriteLine("ScriptedActor", ex.ToString());
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::