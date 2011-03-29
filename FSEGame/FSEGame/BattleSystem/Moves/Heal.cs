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
    public class Heal : IMove
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Heal()
        {

        }
        #endregion

        #region IMove Members

        public string Name
        {
            get 
            {
                return "Heal"; 
            }
        }

        public string DisplayName
        {
            get 
            {
                return "Heal"; 
            }
        }

        public UInt16 Score(Opponent origin, Opponent target)
        {
            UInt16 score = 0;

            if ((origin.CurrentAttributes.Health <
                target.CurrentAttributes.Health) &&
                (origin.CurrentAttributes.Magic >= 10) &&
                (origin.CurrentAttributes.Health <
                origin.BaseAttributes.Health / 2))
            {
                score = 10;
            }

            return score;
        }

        public void Perform(Opponent origin, Opponent target)
        {
            if (origin.CurrentAttributes.Magic >= 10 &&
                origin.CurrentAttributes.Health <
                origin.BaseAttributes.Health)
            {
                origin.CurrentAttributes.Magic -= 10;
                origin.CurrentAttributes.Health += 30;
            }
            else
            {
                FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                    "It has no effect."));
            }
        }

        public void PrePerform(Opponent origin, Opponent target)
        {
            FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                "{0} heals himself and restores some health!", origin.Name));
        }

        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::