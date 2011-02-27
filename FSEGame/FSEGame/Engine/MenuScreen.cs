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

namespace FSEGame.Engine
{
    /// <summary>
    /// Represents a base class for menu screens in the game.
    /// </summary>
    public abstract class MenuScreen
    {
        #region Instance Members
        private Texture2D backgroundTexture;
        private List<IUIElement> children;
        private Boolean visible = true;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the menu screen is visible.
        /// </summary>
        public Boolean IsVisible
        {
            get
            {
                return this.visible;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public MenuScreen(String resource)
        {
            this.children = new List<IUIElement>();
            this.backgroundTexture = FSEGame.Singleton.Content.Load<Texture2D>(resource);
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }
        #endregion

        #region Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        public virtual void Draw(SpriteBatch batch)
        {
            // :: Don't draw the menu screen and its children if its
            // :: visibility is set to false.
            if (!this.visible)
                return;

            // :: Draw the menu screen background texture first and then
            // :: draw the UI elements afterwards so that they appear above
            // :: the background.
            batch.Draw(
                this.backgroundTexture,
                new Rectangle(0, 0, 800, 600),
                Color.White);

            foreach (IUIElement child in this.children)
            {
                child.Draw(batch);
            }
        }
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