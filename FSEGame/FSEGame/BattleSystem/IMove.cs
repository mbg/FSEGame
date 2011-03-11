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

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// Represents an interface for moves.
    /// </summary>
    public interface IMove
    {
        #region Properties
        /// <summary>
        /// Gets the name of this move.
        /// </summary>
        String Name
        {
            get;
        }
        /// <summary>
        /// Gets a user-friendly name for this move.
        /// </summary>
        String DisplayName
        {
            get;
        }
        #endregion

        #region Methods
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
        UInt16 Score(Opponent origin, Opponent target);
        /// <summary>
        /// Performs the move.
        /// </summary>
        /// <param name="origin">The actor who is performing this move.</param>
        /// <param name="target">The target actor of this move.</param>
        void Perform(Opponent origin, Opponent target);

        void PrePerform(Opponent origin, Opponent target);
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::