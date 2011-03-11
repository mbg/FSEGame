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
    /// 
    /// </summary>
    public class ScoredMove
    {
        #region Instance Members
        /// <summary>
        /// The move for which we store the score.
        /// </summary>
        private IMove move;
        /// <summary>
        /// The score of the move in the current battle context.
        /// </summary>
        private UInt16 score;
        #endregion

        #region Properties
        public IMove Move
        {
            get
            {
                return this.move;
            }
        }
        public UInt16 Score
        {
            get
            {
                return this.score;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ScoredMove(IMove move, UInt16 score)
        {
            this.move = move;
            this.score = score;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::