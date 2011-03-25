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
using FSEGame.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// Represents an actor class whose pawn disappears once a torch is acquired.
    /// </summary>
    public class BridgeNPC : GenericNPC
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BridgeNPC(ActorProperties properties)
            : base(properties)
        {
        }
        #endregion

        #region HasAcquiredTorch
        /// <summary>
        /// Gets a value indicating whether the player has acquired the torch.
        /// </summary>
        /// <returns>Returns true if the torch has been acquired.</returns>
        protected Boolean HasAcquiredTorch()
        {
            FSEGame game = FSEGame.Singleton;

            // :: The torch was acquired if the T_Village game state variable is set to 10.
            if (game.PersistentStorage.ContainsKey("T_Village"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["T_Village"].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the actor.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.Passable = this.HasAcquiredTorch();
            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the actor.
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            if(!this.Passable)
                base.Draw(batch);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::