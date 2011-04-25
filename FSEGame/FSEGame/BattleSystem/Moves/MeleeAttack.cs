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
    class MeleeAttack : IMove
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public MeleeAttack()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the move.
        /// </summary>
        public string Name
        {
            get 
            {
                return "MeleeAttack"; 
            }
        }
        /// <summary>
        /// Gets the display name of the move.
        /// </summary>
        public string DisplayName
        {
            get 
            {
                return "Melee Attack"; 
            }
        }
        #endregion

        #region Score
        public UInt16 Score(Opponent origin, Opponent target)
        {
            // :: The base score for this move is 1. This move is not very
            // :: powerful, but a good fallback with few conditions.
            UInt16 score = 1;

            // :: If this move deals enough damage to kill the opponent,
            // :: then this move is slightly more favourable. 
            if (this.CalculateBaseDamage(origin, target)
                >= target.CurrentAttributes.Health)
            {
                score += 5;
            }

            return score;
        }
        #endregion

        public void Perform(Opponent origin, Opponent target)
        {
            Boolean isCrtical = false;
            Boolean isMiss = false;

            UInt16 damage = this.CalculateDamage(origin, target, out isCrtical, out isMiss);

            // :: Perform the action by reducing the target's health points.
            if (target.CurrentAttributes.Health - damage < 0)
                target.CurrentAttributes.Health = 0;
            else
                target.CurrentAttributes.Health -= damage;

            if (isCrtical)
            {
                FSEGame.Singleton.BattleManager.AddToMessageQueue(
                    "It's a critical hit!");
            }
            else if(isMiss)
            {
                FSEGame.Singleton.BattleManager.AddToMessageQueue(
                    "But it misses!");
            }
        }

        #region CalculateBaseDamage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private UInt16 CalculateBaseDamage(Opponent origin, Opponent target)
        {
            // :: The base damage inflicted by this move is the performing
            // :: actor's current strength minus the target's defence value.
            Int32 damage = (int)Math.Floor(((double)origin.CurrentAttributes.Strength * 1.5d)) -
                target.CurrentAttributes.Defence;

            // :: But always at least 1 health point.
            if (damage < 1)
                damage = 1;

            return (UInt16)damage;
        }
        #endregion

        #region CalculateDamage
        /// <summary>
        /// Calculates the damage.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private UInt16 CalculateDamage(Opponent origin, Opponent target, out Boolean critical, out Boolean miss)
        {
            Random r = new Random();
            critical = r.Next(10) == 5;
            miss = r.Next(20) == 10;

            if (miss)
            {
                return 0;
            }
            else
            {
                return (UInt16)(this.CalculateBaseDamage(origin, target) * (critical ? 2 : 1));
            }
        }
        #endregion

        #region PrePerform
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        public void PrePerform(Opponent origin, Opponent target)
        {
            FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                "{0} attacks {1}!", origin.Name, target.Name));
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::