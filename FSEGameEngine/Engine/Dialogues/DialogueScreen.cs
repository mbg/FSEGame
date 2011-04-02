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
using Microsoft.Xna.Framework.Input;
using FSEGame.Engine.UI;
#endregion

namespace FSEGame.Engine.Dialogues
{
    /// <summary>
    /// Represents the main UI component for the dialogue engine.
    /// </summary>
    public class DialogueScreen : IUIElement
    {
        #region Instance Members
        /// <summary>
        /// The background texture for the UI screen.
        /// </summary>
        private Texture2D texture = null;
        /// <summary>
        /// The alternate texture for the UI screen.
        /// </summary>
        private Texture2D alternateTexture = null;
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

        private float totalTimeElapsed = 0.0f;
        /// <summary>
        /// The interval at which the text should be updated.
        /// </summary>
        private float updateInterval = DISPLAY_SPEED;
        /// <summary>
        /// The current length of the string to display.
        /// </summary>
        private Int32 currentWordEnd = 0;
        private UInt16 visibleLines = 1;
        #endregion

        #region Constants
        /// <summary>
        /// The text display speed.
        /// </summary>
#if DISPLAY_BY_CHARACTER
        private const float DISPLAY_SPEED = 0.05f;
#else
        private const float DISPLAY_SPEED = 0.2f;
#endif
        private const float TEXT_SCALE = 3.0f;
        private const float SCREEN_WIDTH = 750.0f;
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

                this.totalTimeElapsed = 0.0f;

#if DISPLAY_BY_CHARACTER
                this.visibleText = this.text[0].ToString();
                this.currentWordEnd = 1;
                this.visibleLines = 1;
#else
                this.visibleText = String.Empty;
                this.currentWordEnd = -1;

                this.DisplayNextWord();
#endif
            }
        }
        /// <summary>
        /// Gets or sets the layer of the dialogue screen.
        /// </summary>
        public UInt32 Layer
        {
            get
            {
                return 100;
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

        #region DisplayNextWord
        /// <summary>
        /// Adds the next word (or character) to the visible text.
        /// </summary>
        private void DisplayNextWord()
        {
            if (this.finished)
                return;

#if DISPLAY_BY_CHARACTER
            if (this.currentWordEnd + 1 > this.text.Length)
            {
                this.finished = true;
            }
            else
            {
                if (this.text[this.currentWordEnd] == '\n')
                {
                    this.Scroll();

                    this.visibleLines++;
                }

                this.visibleText += this.text[this.currentWordEnd++];

                float width = GameBase.Singleton.DefaultFont.MeasureString(this.visibleText).X * TEXT_SCALE;

                if (width > SCREEN_WIDTH)
                {
                    this.Scroll();
                    
                    Int32 lastWhitespace = this.visibleText.LastIndexOf(' ');
                    Char[] characters = this.visibleText.ToCharArray();
                    characters[lastWhitespace] = '\n';
                    this.visibleText = new String(characters);

                    
                    this.visibleLines++;
                    
                }
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
        #endregion

        #region Scroll
        /// <summary>
        /// Scrolls (removes the first line that is being displayed) if required.
        /// </summary>
        private void Scroll()
        {
            if (this.visibleLines > 1)
            {
                Int32 lastNewLine = this.visibleText.IndexOf('\n');
                this.visibleText = this.visibleText.Substring(lastNewLine + 1);
            }
        }
        #endregion

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
            this.totalTimeElapsed += (float)time.ElapsedGameTime.TotalSeconds;

            if (this.timeElapsed >= this.updateInterval || 
                ((GameBase.Singleton.IsKeyPressed(Keys.Enter) || GameBase.Singleton.IsKeyPressed(Keys.Space)) 
                && this.totalTimeElapsed >= 1.0f))
            {
                this.DisplayNextWord();
                this.timeElapsed = 0.0f;
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

            // :: Load textures if they have not yet been loaded.
            if(this.texture == null)
                this.texture = GameBase.Singleton.Content.Load<Texture2D>("SpeechBox");
            if (this.alternateTexture == null)
                this.alternateTexture = GameBase.Singleton.Content.Load<Texture2D>("SpeechBoxFinished");

            batch.Draw(
                this.finished ? this.alternateTexture : this.texture, 
                new Vector2(
                    GameBase.Singleton.GraphicsDevice.Viewport.Width / 2 - 400, 
                    GameBase.Singleton.GraphicsDevice.Viewport.Height - 108), 
                null, 
                Color.White, 
                0.0f, 
                Vector2.Zero, 
                4.0f, 
                SpriteEffects.None, 
                0.0f);

            batch.DrawString(
                GameBase.Singleton.DefaultFont, 
                this.visibleText, 
                new Vector2(
                    20, 
                    GameBase.Singleton.GraphicsDevice.Viewport.Height - 103), 
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