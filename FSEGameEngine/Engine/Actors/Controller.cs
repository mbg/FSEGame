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
#endregion

namespace FSEGame.Engine.Actors
{
    /// <summary>
    /// Represents a base class for controllers. Controllers are objects in a
    /// level or game that do not need to be rendered but require to be updated
    /// each frame. Controllers usually possess a pawn.
    /// </summary>
    public abstract class Controller : Actor
    {
        #region Instance Members
        /// <summary>
        /// The handle of the pawn that is currently being controlled by the
        /// controller.
        /// </summary>
        private Pawn pawn;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the handle of the pawn that is possessed by this controller or
        /// null if this controller doesn't currently possess a pawn.
        /// </summary>
        public Pawn Pawn
        {
            get
            {
                return this.pawn;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Controller()
        {

        }
        #endregion

        #region Possess
        /// <summary>
        /// Possesses the pawn with the given handle. Only one pawn may be
        /// possessed at a time by one controller.
        /// </summary>
        /// <param name="pawn">
        /// The pawn that should be possessed by this controller.
        /// </param>
        public void Possess(Pawn pawn)
        {
            this.pawn = pawn;
        }
        #endregion

        #region Unpossess
        /// <summary>
        /// Unpossesses the pawn that is currently being possessed by this
        /// controller.
        /// </summary>
        public void Unpossess()
        {
            this.pawn = null;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::