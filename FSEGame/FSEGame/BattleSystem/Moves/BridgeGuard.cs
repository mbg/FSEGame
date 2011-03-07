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

namespace FSEGame.BattleSystem.Moves
{
    /// <summary>
    /// 
    /// </summary>
    public class BridgeGuard : IMove
    {
        #region Instance Members
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of this move.
        /// </summary>
        public String Name
        {
            get
            {
                return "BridgeGuard";
            }
        }
        /// <summary>
        /// Gets a user-friendly name for this move.
        /// </summary>
        public String DisplayName
        {
            get
            {
                return "Bridge Guardian";
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BridgeGuard()
        {

        }
        #endregion

        #region Score
        /// <summary>
        /// Evaluates the current game state and returns a score for this
        /// move.
        /// </summary>
        /// <param name="origin">
        /// The actor who is trying to evaluate this move.
        /// </param>
        /// <param name="target">
        /// The target of this move.
        /// </param>
        /// <returns>
        /// Returns a score for this move based on the current state of the
        /// battle. A score of 0 means that the move cannot be performed. The
        /// higher the score the better.
        /// </returns>
        public UInt16 Score(Opponent origin, Opponent target)
        {
            UInt16 score = 5;

            if (origin.CurrentAttributes.Defence + 1
                > origin.BaseAttributes.Defence + 5)
            {
                score = 0;
            }

            return score;
        }
        #endregion

        #region Perform
        /// <summary>
        /// Performs the move.
        /// </summary>
        /// <param name="origin">The actor who is performing this move.</param>
        /// <param name="target">The target actor of this move.</param>
        public void Perform(Opponent origin, Opponent target)
        {
            if (origin.CurrentAttributes.Defence + 1 
                <= origin.BaseAttributes.Defence + 5)
            {
                origin.CurrentAttributes.Defence++;
            }
        }
        #endregion

        #region GetBattleMessage
        /// <summary>
        /// Gets a user-friendly message for the UI.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public String GetBattleMessage(Opponent origin, Opponent target)
        {
            return String.Format(
                "{0} surrounds the bridge with darkness to increase his defence.",
                origin.Name);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::