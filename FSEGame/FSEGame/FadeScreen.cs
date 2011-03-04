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
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public class FadeScreen
    {
        #region Instance Members
        private Texture2D texture;
        private Int32 opacity = 0;
        private Boolean fadingIn = false;
        private Boolean fadingOut = false;
        private Double timeRemaining = 0.0d;
        #endregion

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
        #endregion

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
        public void Update(GameTime time)
        {
            Double factor = 0.0d;

            if (this.fadingIn)
            {
                this.timeRemaining -= time.ElapsedGameTime.TotalSeconds;
            }
            else if (this.fadingOut)
            {
                factor = (this.opacity / 100) * this.timeRemaining;
                this.timeRemaining -= time.ElapsedGameTime.TotalSeconds;

                this.opacity -= (Int32)Math.Ceiling(Math.Ceiling(factor) * time.ElapsedGameTime.TotalSeconds);

                // ::
                if (this.opacity <= 0 || this.timeRemaining <= 0.0d)
                {
                    this.opacity = 0;
                    this.fadingOut = false;
                }
            }
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the fade screen.
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(
                this.texture,
                new Rectangle(0, 0, 800, 600),
                new Color(255, 255, 255, this.opacity));
        }
        #endregion

        public void FadeIn(Double time)
        {
            this.opacity = 0;
            this.fadingIn = true;
            this.timeRemaining = time;
        }

        public void FadeOut(Double time)
        {
            this.opacity = 255;
            this.fadingOut = true;
            this.timeRemaining = time;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::