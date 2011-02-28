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

namespace FSEGame.Engine.Dialogues
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DialogueNode
    {
        #region Instance Members
        /// <summary>
        /// The ID of the node.
        /// </summary>
        private String id;
        /// <summary>
        /// The child nodes of this node.
        /// </summary>
        private List<DialogueNode> nodes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the ID of the node.
        /// </summary>
        public String ID
        {
            get
            {
                return this.id;
            }
        }
        /// <summary>
        /// Gets the child nodes of this node.
        /// </summary>
        public List<DialogueNode> Nodes
        {
            get
            {
                return this.nodes;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public DialogueNode(String id)
        {
            this.id = id;
            this.nodes = new List<DialogueNode>();
        }
        #endregion

        #region GetFirstChild
        /// <summary>
        /// Gets the first child of the node or null if the node has no
        /// children.
        /// </summary>
        /// <returns></returns>
        public DialogueNode GetFirstChild()
        {
            if (this.nodes.Count < 1)
                return null;

            return this.nodes[0];
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::