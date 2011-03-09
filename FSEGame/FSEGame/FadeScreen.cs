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
using FSEGame.Engine;
using FSEGame.Engine.UI;
#endregion

namespace FSEGame
{
    public delegate void FadeScreenEventDelegate();

    /// <summary>
    /// 
    /// </summary>
    public class FadeScreen : UIElement
    {
        #region Instance Members
        private Texture2D texture;
        private float opacity = 0.0f;
        private Boolean fadingIn = false;
        private Boolean fadingOut = false;
        private Double timeRemaining = 0.0d;
        private Boolean enabled = false;
        #endregion

        private event FadeScreenEventDelegate onFinished = null;

        #region Properties
        public Boolean IsFadingIn
        {
            get
            {
                return this.fadingIn;
            }
        }
        public Boolean IsFadingOut
        {
            get
            {
                return this.fadingOut;
            }
        }
        /// <summary>
        /// Gets or sets whether the FadeScreen is enabled.
        /// </summary>
        public Boolean Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }
        #endregion

        public event FadeScreenEventDelegate Finished
        {
            add
            {
                this.onFinished += value;
            }
            remove
            {
                this.onFinished -= value;
            }
        }

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public FadeScreen()
        {
            this.texture = GameBase.Singleton.Content.Load<Texture2D>("black");
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            if (!this.enabled)
                return;

            if (this.fadingIn)
            {
                float change = (float)time.ElapsedGameTime.TotalSeconds;

                if (this.opacity + change >= 1.0f)
                {
                    this.opacity = 1.0f;
                    this.fadingIn = false;

                    if (this.onFinished != null)
                        this.onFinished();
                }
                else
                {
                    this.opacity += change;
                }
            }
            else if (this.fadingOut)
            {
                float change = (float)time.ElapsedGameTime.TotalSeconds;

                if (this.opacity - change <= 0.0f)
                {
                    this.opacity = 0.0f;
                    this.fadingOut = false;

                    if (this.onFinished != null)
                        this.onFinished();
                }
                else
                {
                    this.opacity -= change;
                }
            }
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the fade screen.
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            if (!this.Visible)
                return;

            batch.Draw(
                this.texture,
                new Rectangle(0, 0, 800, 600),
                new Color(1.0f, 1.0f, 1.0f, this.opacity));
        }
        #endregion

        public void FadeIn(Double time)
        {
            this.opacity = 0.0f;
            this.fadingIn = true;
            this.timeRemaining = time;
        }

        public void FadeOut(Double time)
        {
            this.opacity = 1.0f;
            this.fadingOut = true;
            this.timeRemaining = time;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::