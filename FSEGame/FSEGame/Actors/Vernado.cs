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
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// Represents the 'Vernado' NPC.
    /// </summary>
    public class Vernado : GenericNPC
    {
        #region Instance Members
        #endregion

        #region Constants
        /// <summary>
        /// The ID of the torch.
        /// </summary>
        private const String TORCH_ID = "T_Village";
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Vernado(ActorProperties properties)
            : base(properties)
        {

        }
        #endregion

        #region GetDialogueName
        /// <summary>
        /// Overrides the default dialogue behaviour. Depending on whether or not
        /// the player has obtained the torch, a different dialogue file path will
        /// be returned.
        /// </summary>
        /// <returns></returns>
        protected override String GetDialogueName()
        {
            if (this.IsPostTorchConstruction())
                return base.Properties.Properties["PostTorchDialogue"];
            else
                return base.Properties.Properties["DefaultDialogue"];
        }
        #endregion

        #region PerformAction
        /// <summary>
        /// Overrides the default action behaviour. If the player has not yet
        /// acquired the torch and the BuildTorch quest has not yet been started,
        /// then start the quest.
        /// </summary>
        protected override void PerformAction()
        {
            if (!this.IsPostTorchConstruction())
            {
                FSEGame game = FSEGame.Singleton;

                if (!game.PersistentStorage.ContainsKey("Q_BuildTorch"))
                    game.PersistentStorage.Add("Q_BuildTorch", new PersistentStorageItem((UInt32)10));
            }

            base.PerformAction();
        }
        #endregion

        #region IsPostTorchConstruction
        /// <summary>
        /// Gets a value indicating whether the player has obtained the torch.
        /// </summary>
        /// <returns></returns>
        private Boolean IsPostTorchConstruction()
        {
            FSEGame game = FSEGame.Singleton;

            if (game.PersistentStorage.ContainsKey(TORCH_ID))
            {
                UInt32 questProgress = (UInt32)game.PersistentStorage[TORCH_ID].Item;

                return questProgress == 10;
            }

            return false;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::