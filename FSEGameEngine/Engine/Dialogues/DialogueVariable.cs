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
    /// Represents the base class for variables in this implementation of the
    /// Clarity Dialogue Engine.
    /// </summary>
    public abstract class DialogueVariable
    {
        #region Instance Members
        /// <summary>
        /// The name of this variable.
        /// </summary>
        private String name;
        /// <summary>
        /// The type of this variable.
        /// </summary>
        private DialogueVariableType type;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of this variable.
        /// </summary>
        public String Name
        {
            get
            {
                return this.name;
            }
        }
        /// <summary>
        /// Gets the type of this variable.
        /// </summary>
        public DialogueVariableType Type
        {
            get
            {
                return this.type;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public DialogueVariable(String name, DialogueVariableType type)
        {
            this.name = name;
            this.type = type;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::