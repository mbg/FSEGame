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
using System.Collections.Generic;
using System.Drawing.Design;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class ActorEditorProperties
    {
        #region Instance Members
        private ActorProperties properties;
        #endregion

        #region Properties
        [Category("Actor")]
        [DisplayName("Type")]
        [Description("The name of the actor's class in the game.")]
        public String Type
        {
            get
            {
                return this.properties.Type;
            }
            set
            {
                this.properties.Type = value;
            }
        }
        [Category("Actor")]
        [DisplayName("Attributes")]
        [Description("The actor's properties.")]
        [Editor(typeof(ActorPropertyEditor), typeof(UITypeEditor))]
        public Dictionary<String, String> Properties
        {
            get
            {
                return this.properties.Properties;
            }
            set
            {
                this.properties.Properties.Clear();

                foreach (KeyValuePair<String, String> kvp in value)
                {
                    this.properties.Properties.Add(kvp.Key, kvp.Value);
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public ActorEditorProperties(ActorProperties properties)
        {
            this.properties = properties;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::