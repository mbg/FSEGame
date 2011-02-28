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

namespace FSEGame.Engine.Dialogues
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogueScreen : IUIElement
    {
        #region Instance Members
        private Texture2D texture = null;
        private Boolean visible = false;
        private String text = String.Empty;
        #endregion

        #region Properties
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

        public String Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public DialogueScreen()
        {
        }
        #endregion

        #region Update
        public void Update(GameTime time)
        {
            
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the dialogue screen.
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            if (!this.visible)
                return;

            if(this.texture == null)
                this.texture = FSEGame.Singleton.Content.Load<Texture2D>("SpeechBox");

            batch.Draw(
                this.texture, 
                new Vector2(
                    FSEGame.Singleton.GraphicsDevice.Viewport.Width / 2 - 400, 
                    FSEGame.Singleton.GraphicsDevice.Viewport.Height - 100), 
                null, 
                Color.White, 
                0.0f, 
                Vector2.Zero, 
                4.0f, 
                SpriteEffects.None, 
                0.0f);

            batch.DrawString(
                FSEGame.Singleton.DefaultFont, 
                this.text, 
                new Vector2(
                    20, 
                    FSEGame.Singleton.GraphicsDevice.Viewport.Height - 95), 
                Color.Black, 
                0.0f, 
                Vector2.Zero, 
                3.0f, 
                SpriteEffects.None, 
                0.0f);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::