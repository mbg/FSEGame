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
using FSEGame.Engine.UI;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class StaticText : IUIElement
    {
        #region Instance Members
        private SpriteFont font;
        private String text;
        private Vector2 position;
        private Color textColour;
        private Boolean shadow = true;
        private Boolean visible = true;
        #endregion

        #region Properties
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

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public Color TextColour
        {
            get
            {
                return this.textColour;
            }
            set
            {
                this.textColour = value;
            }
        }

        public Boolean HasShadow
        {
            get
            {
                return this.shadow;
            }
            set
            {
                this.shadow = value;
            }
        }

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
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public StaticText(SpriteFont font)
        {
            this.font = font;
            this.text = String.Empty;
            this.position = Vector2.Zero;
            this.textColour = Color.White;
        }
        #endregion

        #region IUIElement Members

        public void Draw(SpriteBatch batch)
        {
            if (!this.visible)
                return;

            if (this.shadow)
            {
                batch.DrawString(
                    this.font,
                    this.text,
                    new Vector2(this.position.X + 2.0f, this.position.Y + 2.0f),
                    new Color(0.0f, 0.0f, 0.0f, 1.0f),
                    0.0f,
                    Vector2.Zero,
                    2.0f,
                    SpriteEffects.None,
                    0.0f);
            }

            batch.DrawString(
                this.font,
                this.text,
                this.position,
                this.textColour,
                0.0f,
                Vector2.Zero,
                2.0f,
                SpriteEffects.None,
                0.0f);
        }

        public void Update(GameTime time)
        {
            
        }

        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::