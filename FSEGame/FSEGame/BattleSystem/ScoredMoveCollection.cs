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
using System.Collections.Generic;
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class ScoredMoveCollection
    {
        #region Instance Members
        private List<ScoredMove> scoredMoves;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ScoredMoveCollection()
        {
            this.scoredMoves = new List<ScoredMove>();
        }
        #endregion

        public void Add(ScoredMove move)
        {
            for (Int32 i = 0; i < this.scoredMoves.Count; i++)
            {
                if (move.Score > this.scoredMoves[i].Score)
                {
                    this.scoredMoves.Insert(i, move);
                    return;
                }
            }

            this.scoredMoves.Add(move);
        }

        public ScoredMove GetTopRated()
        {
            if (this.scoredMoves.Count == 0)
                return null;

            return this.scoredMoves[0];
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::