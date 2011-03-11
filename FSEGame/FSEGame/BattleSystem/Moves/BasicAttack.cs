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
    public class BasicAttack : IMove
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
                return "BasicAttack";
            }
        }
        /// <summary>
        /// Gets a user-friendly name for this move.
        /// </summary>
        public String DisplayName
        {
            get
            {
                return "Basic Attack";
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BasicAttack()
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
            // :: The base score for this move is 1. This move is not very
            // :: powerful, but a good fallback with few conditions.
            UInt16 score = 1;

            // :: If this move deals enough damage to kill the opponent,
            // :: then this move is slightly more favorable. 
            if (this.CalculateBaseDamage(origin, target) 
                >= target.CurrentAttributes.Health)
            {
                score += 5;
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
            Boolean isCrtical = false;
            UInt16 damage = this.CalculateDamage(origin, target, out isCrtical);

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
                "{0} attacks {1}.",
                origin.Name, target.Name);
        }
        #endregion

        private UInt16 CalculateBaseDamage(Opponent origin, Opponent target)
        {
            // :: The base damage inflicted by this move is the performing
            // :: actor's current strength minus the target's defence value.
            Int32 damage = origin.CurrentAttributes.Strength -
                target.CurrentAttributes.Defence;

            // :: But always at least 1 health point.
            if (damage < 1)
                damage = 1;

            return (UInt16)damage;
        }

        #region CalculateDamage
        /// <summary>
        /// Calculates the damage.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private UInt16 CalculateDamage(Opponent origin, Opponent target, out Boolean critical)
        {
            Random r = new Random();
            critical = r.Next(10) == 5;

            return (UInt16)(this.CalculateBaseDamage(origin, target) * (critical ? 2 : 1));
        }
        #endregion

        public void PrePerform(Opponent origin, Opponent target)
        {
            FSEGame.Singleton.BattleManager.AddToMessageQueue(String.Format(
                "{0} hits {1} with a torch!", origin.Name, target.Name));
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::