// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#define DISPLAY_BY_CHARACTER

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
        /// <summary>
        /// The background texture for the UI screen.
        /// </summary>
        private Texture2D texture = null;
        /// <summary>
        /// Indicates whether the screen should be rendered or not.
        /// </summary>
        private Boolean visible = false;
        /// <summary>
        /// Indicates whether the entire text string has been displayed or not.
        /// </summary>
        private Boolean finished = false;
        /// <summary>
        /// The text to display.
        /// </summary>
        private String text = String.Empty;
        /// <summary>
        /// The text that is currently being displayed.
        /// </summary>
        private String visibleText = String.Empty;
        /// <summary>
        /// The time that has elapsed since the display text was last updated.
        /// </summary>
        private float timeElapsed = 0.0f;
        /// <summary>
        /// The interval at which the text should be updated.
        /// </summary>
        private float updateInterval = DISPLAY_SPEED;
        /// <summary>
        /// The current length of the string to display.
        /// </summary>
        private Int32 currentWordEnd = 0;
        #endregion

        #region Constants
        /// <summary>
        /// The text display speed.
        /// </summary>
#if DISPLAY_BY_CHARACTER
        private const float DISPLAY_SPEED = 0.1f;
#else
        private const float DISPLAY_SPEED = 0.2f;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether this UI element is visible.
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
        /// <summary>
        /// Gets a value indicating whether all text has been displayed.
        /// </summary>
        public Boolean Finished
        {
            get
            {
                return this.finished;
            }
        }
        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public String Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.finished = false;

#if DISPLAY_BY_CHARACTER
                this.visibleText = this.text[0].ToString();
                this.currentWordEnd = 1;
#else
                this.visibleText = String.Empty;
                this.currentWordEnd = -1;

                this.DisplayNextWord();
#endif
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

        private void DisplayNextWord()
        {
            if (this.finished)
                return;

#if DISPLAY_BY_CHARACTER
            if (this.currentWordEnd + 1 > this.text.Length)
            {
                this.visibleText = this.text;
                this.finished = true;
            }
            else
            {
                this.currentWordEnd++;
                this.visibleText = this.text.Substring(0, this.currentWordEnd);
            }
#else
            this.currentWordEnd = this.text.IndexOf(' ', this.currentWordEnd + 1);

            if (this.currentWordEnd == -1)
            {
                this.visibleText = this.text;
                this.finished = true;
            }
            else
            {
                this.visibleText = this.text.Substring(0, this.currentWordEnd);
            }
#endif
        }

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            if (!this.visible)
                return;

            this.timeElapsed += (float)time.ElapsedGameTime.TotalSeconds;

            if (this.timeElapsed >= this.updateInterval)
            {
                this.DisplayNextWord();
                this.timeElapsed -= this.updateInterval;
            }
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
                this.visibleText, 
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