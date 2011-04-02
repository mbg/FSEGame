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
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class ActorProperties
    {
        #region Instance Members
        private UInt32 x;
        private UInt32 y;
        private String type;
        private Dictionary<String, String> properties;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of the actor.
        /// </summary>
        public String Type
        {
            get
            {
                return this.type;
            }
        }

        public UInt32 X
        {
            get
            {
                return this.x;
            }
        }

        public UInt32 Y
        {
            get
            {
                return this.y;
            }
        }

        public Dictionary<String, String> Properties
        {
            get
            {
                return this.properties;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        /// <param name="type"></param>
        public ActorProperties(String type, UInt32 x, UInt32 y)
        {
            this.type = type;
            this.x = x;
            this.y = y;

            this.properties = new Dictionary<String, String>();
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::