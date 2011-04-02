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

namespace FSEGame.Engine
{
    /// <summary>
    /// Represents an entry point for a level.
    /// </summary>
    public class LevelEntryPoint
    {
        #region Instance Members
        /// <summary>
        /// The name of the entry point.
        /// </summary>
        private String name;
        /// <summary>
        /// The x coordinate of the entry point.
        /// </summary>
        private UInt32 x;
        /// <summary>
        /// The y coordinate of the entry point.
        /// </summary>
        private UInt32 y;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of this entry point.
        /// </summary>
        public String Name
        {
            get
            {
                return this.name;
            }
        }
        /// <summary>
        /// Gets or sets the x coordinate of this entry point.
        /// </summary>
        public UInt32 X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        /// <summary>
        /// Gets or sets the y coordinate of this entry point.
        /// </summary>
        public UInt32 Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public LevelEntryPoint(String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.name = name;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::