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
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    class CommandLineParser
    {
        #region Instance Members
        private String levelToLoad = null;
        private String entryPoint = null;
        #endregion

        #region Properties
        public String LevelToLoad
        {
            get
            {
                return this.levelToLoad;
            }
        }
        public String EntryPoint
        {
            get
            {
                return this.entryPoint;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public CommandLineParser(String[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                switch (arguments[i])
                {
                    case "-l":
                        this.levelToLoad = arguments[++i];
                        break;
                    case "-e":
                        this.entryPoint = arguments[++i];
                        break;
                }
            }

            //this.levelToLoad = @"Levels\Test2.xml";
            //this.entryPoint = "Default";
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::