﻿// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Autopatcher
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 2/21/2011 7:31:12 PM
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
#endregion

namespace Origin.Autopatcher
{
    /// <summary>
    /// Contains static update information for this autopatcher.
    /// </summary>
    internal static class Autopatcher
    {
        #region Static Members
        private static String patchInformationFile = "http://download.syngate.co.uk/FSEGame/Patch.xml";
        private static String applicationName = "FSEGame";
        private static String applicationExecutable = "FSEGame.exe";
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static String PatchInformationFile
        {
            get
            {
                return Autopatcher.patchInformationFile;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static String ApplicationName
        {
            get
            {
                return Autopatcher.applicationName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static String ApplicationExecutable
        {
            get
            {
                return Autopatcher.applicationExecutable;
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::