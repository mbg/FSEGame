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
using System.Windows.Forms;
using FSEGame.Engine;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelEditor : LevelEditorControl
    {
        #region Instance Members
        private SpriteBatch batch;
        private EditorLevel level;
        private Int32 offsetX = 0;
        private Int32 offsetY = 0;
        private Point restoreMousePosition;
        private Point lastMousePosition;
        #endregion

        #region Properties
        public EditorLevel CurrentLevel
        {
            get
            {
                return this.level;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public LevelEditor()
        {
            this.AllowDrop = true;

            this.level = new EditorLevel();
        }
        #endregion

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
            {
                this.lastMousePosition = new Point(e.X, e.Y);
                this.restoreMousePosition = this.PointToScreen(this.lastMousePosition);

                Cursor.Hide();
            }

            this.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Right)
            {
                Point currentMousePosition = new Point(e.X, e.Y);
                Point relativeMovement = new Point(
                    currentMousePosition.X - this.lastMousePosition.X,
                    currentMousePosition.Y - this.lastMousePosition.Y);

                this.offsetX -= relativeMovement.X;
                this.offsetY -= relativeMovement.Y;

                if (this.offsetX < 0)
                    this.offsetX = 0;
                if (this.offsetY < 0)
                    this.offsetY = 0;

                this.lastMousePosition = currentMousePosition;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Position = this.restoreMousePosition;
                Cursor.Show();
            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            if (drgevent.Data.GetDataPresent(typeof(Tile)))
            {
                drgevent.Effect = DragDropEffects.Link;
                return;
            }

            drgevent.Effect = DragDropEffects.None;
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            if (drgevent.Data.GetDataPresent(typeof(Tile)))
            {
                drgevent.Effect = DragDropEffects.Link;
                return;
            }

            drgevent.Effect = DragDropEffects.None;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (drgevent.Data.GetDataPresent(typeof(Tile)))
            {
                drgevent.Effect = DragDropEffects.Link;

                Point p = this.PointToClient(new Point(
                    drgevent.X, drgevent.Y));

                CellPosition position = this.ControlPositionToActual(p.X, p.Y);

                this.level.AddCell(position.X - 1, position.Y - 1, (Tile)drgevent.Data.GetData(typeof(Tile)));

                return;
            }

            drgevent.Effect = DragDropEffects.None;
        }

        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
        {
            base.OnQueryContinueDrag(qcdevent);
        }

        protected override void Initialise()
        {
            base.Initialise();

            this.batch = new SpriteBatch(this.GraphicsDevice);
        }

        protected override void Draw()
        {
            base.Draw();

            this.batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            this.level.Render(this.batch, this.offsetX, this.offsetY);

            this.batch.End();
        }

        #region ControlPositionToActual
        /// <summary>
        /// Converts control space coordinates into actual tile
        /// coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CellPosition ControlPositionToActual(int x, int y)
        {
            // :: Firstly, calculate the column. This is easy because
            // :: we can't scroll horizontally.
            x = x + this.offsetX;
            Int32 column = (Int32)Math.Ceiling(x / 64.0f);

            if (column == 0) column = 1;

            // :: Next, calculate the row. This is a bit more difficult
            // :: because we have to consider the scroll offset;
            y = y + this.offsetY;

            Int32 row = (Int32)Math.Ceiling(y / 64.0f);

            if (row == 0) row = 1;

            // :: Not all control space coordinates map to tile
            // :: coordinates.
            return new CellPosition(column, row);
        }
        #endregion

        private const int WM_CONTEXTMENU = 0x007B;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CONTEXTMENU)
            {
                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::