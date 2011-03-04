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
using FSEGame.Engine.UI;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// Represents a base class for menu screens in the game.
    /// </summary>
    public abstract class MenuScreen : UIContainer
    {
        #region Instance Members
        private Texture2D backgroundTexture;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public MenuScreen(String resource)
        {
            this.backgroundTexture = GameBase.Singleton.Content.Load<Texture2D>(resource);
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            // :: Don't draw the menu screen and its children if its
            // :: visibility is set to false.
            if (!this.Visible)
                return;

            // :: Draw the menu screen background texture first and then
            // :: draw the UI elements afterwards so that they appear above
            // :: the background.
            batch.Draw(
                this.backgroundTexture,
                new Rectangle(0, 0, 800, 600),
                Color.White);

            base.Draw(batch);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::