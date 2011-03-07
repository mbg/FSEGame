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
using System.Collections.Generic;
using System.Xml;
#endregion

namespace FSEGame.BattleSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Battle
    {
        #region Instance Members
        private Queue<Opponent> opponents;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Battle()
        {
            this.opponents = new Queue<Opponent>();
        }
        #endregion

        #region LoadFromFile
        /// <summary>
        /// Loads the battle data from an XML document.
        /// </summary>
        /// <param name="filename">
        /// The path to the file containing the XML document.
        /// </param>
        public void LoadFromFile(String filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlElement rootElement = doc.DocumentElement;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::