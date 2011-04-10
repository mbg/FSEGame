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
using System.ComponentModel;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class EntryPointProperties
    {
        #region Instance Members
        private LevelEntryPoint entryPoint;
        #endregion

        #region Properties
        [Category("Entry Point")]
        [DisplayName("Orientation")]
        [Description("The entry point's orientation which is used to determine the orientation of the player.")]
        public float Orientation
        {
            get
            {
                return this.entryPoint.Orientation;
            }
            set
            {
                this.entryPoint.Orientation = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public EntryPointProperties(LevelEntryPoint entryPoint)
        {
            this.entryPoint = entryPoint;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::