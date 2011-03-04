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
    public delegate void ButtonActionDelegate(UIButton sender);

    /// <summary>
    /// 
    /// </summary>
    public class UIButton : UIElement
    {
        #region Instance Members
        private Texture2D defaultStateTexture;
        private Texture2D selectedStateTexture;
        private String text;
        private Boolean selected;
        private Int32 width;
        private Int32 height;
        #endregion

        #region Events
        private ButtonActionDelegate onAction = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
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
        public Boolean Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }
        #endregion

        #region Event Properties
        /// <summary>
        /// 
        /// </summary>
        public event ButtonActionDelegate OnAction
        {
            add
            {
                this.onAction += value;
            }
            remove
            {
                this.onAction -= value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIButton(String defaultResource, String selectedResource)
        {
            this.defaultStateTexture = FSEGame.Singleton.Content.Load<Texture2D>(defaultResource);
            this.selectedStateTexture = FSEGame.Singleton.Content.Load<Texture2D>(selectedResource);

            this.text = String.Empty;
            this.selected = false;
            this.width = this.defaultStateTexture.Width;
            this.height = this.defaultStateTexture.Height;
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            // :: Do not render the button if its visibility is set to false.
            if (!base.Visible)
                return;

            // :: Decide which texture to use based on the buttons current
            // :: state.
            Texture2D textureToDraw = this.defaultStateTexture;

            if (this.selected)
                textureToDraw = this.selectedStateTexture;

            // :: Draw the button in the current sprite batch.
            batch.Draw(
                textureToDraw,
                new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    this.width,
                    this.height),
                Color.White);
        }

        #endregion

        public void Trigger()
        {
            if (this.onAction != null)
                this.onAction(this);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::