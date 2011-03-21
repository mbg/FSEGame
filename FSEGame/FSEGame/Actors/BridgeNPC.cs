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
    /// 
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

        #region HasAcquiredCoal
        /// <summary>
        /// Gets a value indicating whether the player has acquired the coal.
        /// </summary>
        /// <returns>Returns true if the coal has been acquired.</returns>
        protected Boolean HasAcquiredTorch()
        {
            FSEGame game = FSEGame.Singleton;

            // :: The coal was acquired if the I_Coal game state variable is set to 10.
            if (game.PersistentStorage.ContainsKey("T_Village"))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage["T_Village"].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            this.Passable = this.HasAcquiredTorch();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            if(!this.Passable)
                base.Draw(batch);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::