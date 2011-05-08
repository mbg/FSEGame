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
using FSEGame.Engine.Actors;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GameInfo
    {
        #region Instance Members
        #endregion

        #region Properties
        /// <summary>
        /// If overriden, this property gets the name of the game.
        /// </summary>
        public abstract String Name
        {
            get;
        }
        /// <summary>
        /// If overriden, this property gets the player controller of the
        /// game.
        /// </summary>
        public abstract Controller PlayerController
        {
            get;
        }
        /// <summary>
        /// If overriden, this property gets the default pawn of this
        /// game.
        /// </summary>
        public abstract Pawn DefaultPawn
        {
            get;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GameInfo()
        {

        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::