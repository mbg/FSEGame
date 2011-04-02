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
    public delegate void UIGridButtonEventDelegate(UIGridButton sender);

    /// <summary>
    /// 
    /// </summary>
    public class UIGridButton : UIElement
    {
        #region Instance Members
        private String text;
        private Object tag;
        #endregion

        private event UIGridButtonEventDelegate triggered = null;

        #region Properties
        /// <summary>
        /// Gets or sets the display text of the button.
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
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Object Tag
        {
            get
            {
                return this.tag;
            }
            set
            {
                this.tag = value;
            }
        }
        #endregion

        public event UIGridButtonEventDelegate Triggered
        {
            add
            {
                this.triggered += value;
            }
            remove
            {
                this.triggered -= value;
            }
        }

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIGridButton(String text, Vector2 position)
        {
            this.text = text;
            this.Position = position;
        }
        #endregion

        public override void Update(GameTime time)
        {
            
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.DrawString(
                GameBase.Singleton.DefaultFont,
                this.text,
                this.Position,
                Color.Black,
                0.0f,
                Vector2.Zero,
                3.0f,
                SpriteEffects.None,
                0.0f);
        }

        public void Trigger()
        {
            if (this.triggered != null)
                this.triggered(this);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::