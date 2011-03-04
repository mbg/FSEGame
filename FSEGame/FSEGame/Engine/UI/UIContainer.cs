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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UIContainer : IUIElement
    {
        #region Instance Members
        /// <summary>
        /// The list of children of this UI container.
        /// </summary>
        private List<IUIElement> children;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list of child UI elements of this UI container.
        /// </summary>
        public List<IUIElement> Children
        {
            get
            {
                return this.children;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIContainer()
        {
            this.children = new List<IUIElement>();
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the UI container and its children.
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(GameTime time)
        {
            foreach (IUIElement element in this.children)
            {
                element.Update(time);
            }
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the UI container and its children.
        /// </summary>
        /// <param name="batch"></param>
        public virtual void Draw(SpriteBatch batch)
        {
            foreach (IUIElement element in this.children)
            {
                element.Draw(batch);
            }
        }

        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::