// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: PatchGenerator
// :: Copyright 2010 Origin Software
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 7/27/2010 3:48:14 AM
// ::      by: ORI20082009\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
#endregion

namespace Origin.PatchGenerator
{
    /// <summary>
    /// 
    /// </summary>
    internal class PatchFileInfo
    {
        #region Instance Members
        #endregion

        #region Properties
        internal String Hash
        {
            get;
            set;
        }
        internal String Filename
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public PatchFileInfo()
        {

        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::