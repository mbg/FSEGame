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
    /// 
    /// </summary>
    public class DialogueSpeechNode : DialogueNode
    {
        #region Instance Members
        private String stringVariable;
        private float timeout;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the string variable which contains the
        /// text for this speech node.
        /// </summary>
        public String StringVariable
        {
            get
            {
                return this.stringVariable;
            }
            set
            {
                this.stringVariable = value;
            }
        }
        /// <summary>
        /// Gets or sets the timeout for this speech node.
        /// </summary>
        public float Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                this.timeout = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public DialogueSpeechNode(String id)
            : base(id)
        {

        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::