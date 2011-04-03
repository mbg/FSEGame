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

namespace FSELevelEditor
{
    /// <summary>
    /// 
    /// </summary>
    public class TilesetChangedEventArgs : EventArgs
    {
        #region Instance Members
        /// <summary>
        /// Stores a handle to a tileset.
        /// </summary>
        private Tileset tileset;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the handle of the new tileset.
        /// </summary>
        public Tileset Tileset
        {
            get
            {
                return this.tileset;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public TilesetChangedEventArgs(Tileset tileset)
        {
            this.tileset = tileset;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::