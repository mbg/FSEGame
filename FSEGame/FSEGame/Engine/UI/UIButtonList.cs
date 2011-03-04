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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class UIButtonList : UIElement
    {
        #region Instance Members
        private List<UIButton> buttons;
        private float timeElapsed;
        private float timeout;
        private Int32 selectedIndex = -1;
        private Boolean scrollThrough = false;
        #endregion

        #region Constants
        private const float DEFAULT_TIMEOUT = 0.3f;
        #endregion

        #region Properties
        public List<UIButton> Buttons
        {
            get
            {
                return this.buttons;
            }
        }
        public float Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                this.timeout = value;
            }
        }
        public Boolean ScrollThrough
        {
            get
            {
                return this.scrollThrough;
            }
            set
            {
                this.scrollThrough = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIButtonList()
        {
            this.buttons = new List<UIButton>();
            this.timeElapsed = 0.0f;
            this.timeout = DEFAULT_TIMEOUT;
        }
        #endregion

        public override void Update(GameTime time)
        {
            KeyboardState ks = Keyboard.GetState();

            this.timeElapsed += (float)time.ElapsedGameTime.TotalSeconds;

            // :: If there are buttons in the list, but no button has been selected,
            // :: then select the first button in the list.
            if (this.buttons.Count > 0 && this.selectedIndex == -1)
            {
                this.Select(0);
            }

            // :: Update the buttons, this is may be required if the list contains instances
            // :: of classes which inherit from the UIButton class.
            foreach (UIButton button in this.buttons)
            {
                button.Update(time);
            }

            // :: Perform keyboard events if the timeout has elapsed.
            if (this.timeElapsed > this.timeout)
            {
                if (ks.IsKeyDown(Keys.Enter) && this.selectedIndex >= 0)
                {
                    this.buttons[this.selectedIndex].Trigger();
                    this.timeElapsed = 0.0f;
                }
                else if ((ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up)) && this.buttons.Count >= 1)
                {
                    if (this.selectedIndex == 0 && this.scrollThrough)
                    {
                        this.Select(this.buttons.Count - 1);
                        this.timeElapsed = 0.0f;
                    }
                    else if(this.selectedIndex != 0)
                    {
                        this.Select(this.selectedIndex - 1);
                        this.timeElapsed = 0.0f;
                    }
                }
                else if ((ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down)) && this.buttons.Count >= 1)
                {
                    if (this.selectedIndex == (this.buttons.Count - 1) && this.scrollThrough)
                    {
                        this.Select(0);
                        this.timeElapsed = 0.0f;
                    }
                    else if (this.selectedIndex != (this.buttons.Count - 1))
                    {
                        this.Select(this.selectedIndex + 1);
                        this.timeElapsed = 0.0f;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (UIButton button in this.buttons)
            {
                button.Draw(batch);
            }
        }

        private void Select(Int32 index)
        {
            this.Deselect();

            this.selectedIndex = index;
            this.buttons[index].Selected = true;
        }

        #region Deselect
        /// <summary>
        /// Deselects the button that is currently selected.
        /// </summary>
        private void Deselect()
        {
            if(this.selectedIndex >= 0)
                this.buttons[this.selectedIndex].Selected = false;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::