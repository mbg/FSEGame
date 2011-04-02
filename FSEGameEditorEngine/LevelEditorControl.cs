
#region References
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelEditorControl : GraphicsDeviceControl 
    {
        #region Instance Members
        private ContentManager contentManager;
        #endregion

        #region Properties
        public ContentManager ContentManager
        {
            get
            {
                return this.contentManager;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public LevelEditorControl()
        {
            this.contentManager = new ContentManager(this.Services);
        }
        #endregion

        protected override void Initialise()
        {
            
        }

        protected override void Draw()
        {
            
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::