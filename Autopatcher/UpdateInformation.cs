// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Autopatcher
// :: Copyright 2010 Origin Software
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 7/24/2010 8:27:39 PM
// ::      by: ORI20082009\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
#endregion

namespace Origin.Autopatcher
{
    /// <summary>
    /// 
    /// </summary>
    internal class UpdateInformation
    {
        #region Instance Members
        #endregion

        #region Properties
        internal String Destination
        {
            get;
            set;
        }
        internal String Source
        {
            get;
            set;
        }
        internal Int32 Size
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UpdateInformation()
        {

        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::