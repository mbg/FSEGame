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
    /// 
    /// </summary>
    public class UIProgressBar : UIElement
    {
        #region Instance Members
        private Texture2D backgroundTexture;
        private Texture2D foregroundTexture;
        private UInt32 maximum;
        private UInt32 value;
        private Int32 visibleWidth;
        #endregion

        #region Properties
        public UInt32 Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                this.maximum = value;
            }
        }

        public UInt32 Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIProgressBar(String backgroundResource, String foregroundResource)
        {
            this.backgroundTexture = GameBase.Singleton.Content.Load<Texture2D>(backgroundResource);
            this.foregroundTexture = GameBase.Singleton.Content.Load<Texture2D>(foregroundResource);
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            if (this.maximum == 0)
                return;

            if (this.value > this.maximum)
            {
                throw new ArgumentOutOfRangeException(
                    "The current value cannot be greater than the maximum value.");
            }

            float percentage = (float)this.value / (float)this.maximum;
            this.visibleWidth = (Int32)Math.Ceiling(percentage * this.backgroundTexture.Width);
        }
        #endregion

        #region Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            if (!this.Visible)
                return;

            batch.Draw(
                this.backgroundTexture,
                base.Position,
                new Rectangle(
                    0,
                    0,
                    this.backgroundTexture.Width,
                    this.backgroundTexture.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                4.0f,
                SpriteEffects.None,
                0.0f);

            batch.Draw(
                this.foregroundTexture,
                base.Position,
                new Rectangle(
                    0,
                    0,
                    this.visibleWidth,
                    this.foregroundTexture.Height),
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