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
using System.IO;
using FSEGame.Engine;
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class BattleManager
    {
        #region Instance Members
        /// <summary>
        /// The current battle data.
        /// </summary>
        private Battle currentBattle;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BattleManager()
        {

        }
        #endregion

        #region Load
        /// <summary>
        /// Loads battle data from the file with the specified path.
        /// </summary>
        /// <param name="filename">
        /// The path of the file from which the battle data should be loaded.
        /// </param>
        public void Load(String filename)
        {
            this.currentBattle = new Battle();

            this.currentBattle.LoadFromFile(Path.Combine(
                GameBase.Singleton.Content.RootDirectory, filename));
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::