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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// Represents a static image.
    /// </summary>
    public class StaticImage : UIElement
    {
        #region Instance Members
        /// <summary>
        /// The image texture.
        /// </summary>
        private Texture2D texture;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public StaticImage(String resource)
        {
            this.texture = GameBase.Singleton.Content.Load<Texture2D>(resource);
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the static image.
        /// </summary>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            // :: We don't have to do anything...
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the static image.
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            if (!this.Visible)
                return;

            batch.Draw(
                this.texture,
                this.Position,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                4.0f,
                SpriteEffects.None,
                0.0f);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::