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

        #region Constants
        /// <summary>
        /// The amount of mana required to cast this spell.
        /// </summary>
        private const UInt16 MANA_USAGE = 20; // used to be 10
        /// <summary>
        /// The amount of health that is restored each time this spell is cast.
        /// </summary>
        private const UInt16 HEALTH_RESTORED = 30; // used to be 30
        #endregion

        #region Properties
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

        public UInt16 Score(Opponent origin, Opponent target)
        {
            UInt16 score = 0;

            if ((origin.CurrentAttributes.Health <
                target.CurrentAttributes.Health) &&
                (origin.CurrentAttributes.Magic >= MANA_USAGE) &&
                (origin.CurrentAttributes.Health <
                origin.BaseAttributes.Health / 2))
            {
                score = 10;
            }

            return score;
        }

        public void Perform(Opponent origin, Opponent target)
        {
            if (origin.CurrentAttributes.Magic >= MANA_USAGE &&
                origin.CurrentAttributes.Health <
                origin.BaseAttributes.Health)
            {
                origin.CurrentAttributes.Magic -= MANA_USAGE;
                origin.CurrentAttributes.Health += HEALTH_RESTORED;
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