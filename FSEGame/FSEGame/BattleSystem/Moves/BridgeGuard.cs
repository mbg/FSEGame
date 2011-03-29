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

            if (origin.CurrentAttributes.Defence + 1 >
                origin.BaseAttributes.Defence + 5)
            {
                score = 0;
            }
            else if (origin.CurrentAttributes.Magic < 10)
            {
                score = 0;
            }
            else if (origin.CurrentAttributes.Health <
                target.CurrentAttributes.Health)
            {
                score -= 2;
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
                origin.CurrentAttributes.Magic -= 10;

                FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                    "{0}'s defence has increased!", origin.Name));
            }
            else
            {
                FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                    "It has no effect."));
            }
        }
        #endregion

        public void PrePerform(Opponent origin, Opponent target)
        {
            FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                "{0} surrounds the bridge with shadows!", origin.Name));
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::