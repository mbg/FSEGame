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
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// Represents an abstract base class for UI elements.
    /// </summary>
    public abstract class UIElement : IUIElement
    {
        #region Instance Members
        /// <summary>
        /// The position of the UI element.
        /// </summary>
        private Vector2 position;
        /// <summary>
        /// A value indicating whether this UI element is visible or not.
        /// </summary>
        private Boolean visible;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the position of the UI element.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this UI element is visible
        /// or not.
        /// </summary>
        public Boolean Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIElement()
        {
            this.position = Vector2.Zero;
            this.visible = true;
        }
        #endregion

        #region IUIElement Members
        /// <summary>
        /// Updates the UI element with the current game time.
        /// </summary>
        /// <param name="time"></param>
        public abstract void Update(GameTime time);
        /// <summary>
        /// Draws the UI element.
        /// </summary>
        /// <param name="batch"></param>
        public abstract void Draw(SpriteBatch batch);
        #endregion

        #region Show
        /// <summary>
        /// Shows the menu screen.
        /// </summary>
        public virtual void Show()
        {
            this.visible = true;
        }
        #endregion

        #region Hide
        /// <summary>
        /// Hides the menu screen.
        /// </summary>
        public virtual void Hide()
        {
            this.visible = false;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::