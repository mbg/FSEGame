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

namespace FSEGame.Engine.Dialogues
{
    /// <summary>
    /// Represents a string variable in this implementation of the Clarity
    /// Dialogue Engine.
    /// </summary>
    public class DialogueStringVariable : DialogueVariable
    {
        #region Instance Members
        /// <summary>
        /// The value of this string variable.
        /// </summary>
        private String value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the value of this string variable.
        /// </summary>
        public String Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public DialogueStringVariable(String name)
            : base(name, DialogueVariableType.String)
        {

        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::