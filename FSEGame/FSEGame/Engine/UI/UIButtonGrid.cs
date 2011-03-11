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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class UIButtonGrid : UIElement
    {
        #region Instance Members
        private UIGridButton[,] buttons;
        private StaticImage selector;
        private UInt16 rows = 0;
        private UInt16 columns = 0;
        private UInt16 selectedRow = 0;
        private UInt16 selectedColumn = 0;
        private UInt64 frames = 0;
        #endregion

        #region Properties
        public UIGridButton[,] Buttons
        {
            get
            {
                return this.buttons;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIButtonGrid(UInt16 rows, UInt16 columns)
        {
            this.rows = rows;
            this.columns = columns;

            this.buttons = new UIGridButton[rows, columns];
            this.selector = new StaticImage("GridButtonSelector");
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            if (!this.Visible)
                return;

            this.frames++;

            if (this.frames > 1)
            {
                GameBase gb = GameBase.Singleton;

                if (gb.IsKeyPressed(Keys.W) || gb.IsKeyPressed(Keys.Up))
                {
                    if (this.selectedRow > 0)
                        this.selectedRow--;
                }
                else if (gb.IsKeyPressed(Keys.S) || gb.IsKeyPressed(Keys.Down))
                {
                    if (this.selectedRow < this.rows - 1)
                        this.selectedRow++;
                }
                else if (gb.IsKeyPressed(Keys.D) || gb.IsKeyPressed(Keys.Right))
                {
                    if (this.selectedColumn < this.columns - 1)
                        this.selectedColumn++;
                }
                else if (gb.IsKeyPressed(Keys.A) || gb.IsKeyPressed(Keys.Left))
                {
                    if (this.selectedColumn > 0)
                        this.selectedColumn--;
                }
                else if (gb.IsKeyPressed(Keys.Enter) || gb.IsKeyPressed(Keys.Space))
                {
                    UIGridButton button = this.buttons[this.selectedRow, this.selectedColumn];

                    if (button != null)
                        button.Trigger();
                }
            }

            foreach (UIGridButton button in this.buttons)
            {
                if(button != null)
                    button.Update(time);
            }

            this.selector.Position = this.CalculateSelectorPosition();
            this.selector.Update(time);
        }
        #endregion

        public override void Draw(SpriteBatch batch)
        {
            if (!this.Visible)
                return;

            foreach (UIGridButton button in this.buttons)
            {
                if(button != null)
                    button.Draw(batch);
            }

            this.selector.Draw(batch);
        }

        private Vector2 CalculateSelectorPosition()
        {
            return this.GetActiveButtonPosition() - new Vector2(28, -4);
        }

        private Vector2 GetActiveButtonPosition()
        {
            UIGridButton button = this.buttons[this.selectedRow, this.selectedColumn];

            if (button != null)
                return button.Position;
            else
                return Vector2.Zero;
        }

        public override void Show()
        {
            base.Show();

            this.frames = 0;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::