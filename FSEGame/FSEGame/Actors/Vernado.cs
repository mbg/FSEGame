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
    /// 
    /// </summary>
    public class Vernado : GenericNPC
    {
        #region Instance Members
        #endregion

        private const String TORCH_ID = "T_Village";

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

        protected override String GetDialogueName()
        {
            if (this.IsPostTorchConstruction())
                return base.Properties.Properties["PostTorchDialogue"];
            else
                return base.Properties.Properties["DefaultDialogue"];
        }

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
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::