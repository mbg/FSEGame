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
    public delegate void ObjectSelectedDelegate(Object value);

    /// <summary>
    /// 
    /// </summary>
    public class LevelEditor : LevelEditorControl
    {
        #region Instance Members
        private TilesetManager tilesetManager;
        private SpriteBatch batch;
        private EditorLevel level;
        private Int32 offsetX = 0;
        private Int32 offsetY = 0;
        private Point restoreMousePosition;
        private Point lastMousePosition;
        private EditorMode mode = EditorMode.Tiles;
        private Boolean edit = false;
        private Tile selectedTile = null;
        #endregion

        #region Properties
        public TilesetManager TilesetManager
        {
            get
            {
                return this.tilesetManager;
            }
            set
            {
                this.tilesetManager = value;
            }
        }
        public EditorLevel CurrentLevel
        {
            get
            {
                return this.level;
            }
        }
        public Tile SelectedTile
        {
            get
            {
                return this.selectedTile;
            }
            set
            {
                this.selectedTile = value;
            }
        }
        /// <summary>
        /// Gets or sets the editor's mode.
        /// </summary>
        public EditorMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }
        /// <summary>
        /// Gets or sets whether the level editor is in edit mode.
        /// </summary>
        public Boolean EditMode
        {
            get
            {
                return this.edit;
            }
            set
            {
                this.edit = value;
            }
        }
        #endregion

        public event ObjectSelectedDelegate ObjectSelected;

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public LevelEditor()
        {
            this.AllowDrop = true;
        }
        #endregion

        public void New()
        {
            this.CurrentLevel.New();
            this.ResetView();
        }

        public void ResetView()
        {
            this.offsetX = 0;
            this.offsetY = 0;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
            {
                this.lastMousePosition = new Point(e.X, e.Y);
                this.restoreMousePosition = this.PointToScreen(this.lastMousePosition);

                Cursor.Hide();
            }
            else if (e.Button == MouseButtons.Left)
            {
                // :: Calculate the row and column numbers of the cell.
                Point p = new Point(e.X, e.Y);
                CellPosition position = this.ControlPositionToActual(p.X, p.Y);

                if (this.edit)
                {
                    if (this.ObjectSelected != null)
                    {
                        position.X--;
                        position.Y--;

                        if (this.mode == EditorMode.Tiles)
                        {
                            LevelCell cell = this.level.GetCellAtPosition(position);

                            if (cell == null)
                                this.ObjectSelected(null);
                            else
                                this.ObjectSelected(new CellProperties(cell));
                        }
                        else if (this.mode == EditorMode.Actors)
                        {
                            ActorProperties actor = this.level.GetActorAtPosition(position);

                            if (actor == null)
                                this.ObjectSelected(null);
                            else
                                this.ObjectSelected(new ActorEditorProperties(actor));
                        }
                        else
                        {
                            LevelEntryPoint ep;
                        }
                    }
                }
                else
                {
                    if (this.selectedTile != null)
                    {
                        this.PlaceTile(position, this.selectedTile);
                    }
                }
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
            }
            else if (drgevent.Data.GetDataPresent(typeof(String)))
            {
                drgevent.Effect = DragDropEffects.Link;
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            if (drgevent.Data.GetDataPresent(typeof(Tile)))
            {
                drgevent.Effect = DragDropEffects.Link;
            }
            else if (drgevent.Data.GetDataPresent(typeof(String)))
            {
                drgevent.Effect = DragDropEffects.Link;
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            Point p = this.PointToClient(new Point(
                    drgevent.X, drgevent.Y));

            CellPosition position = this.ControlPositionToActual(p.X, p.Y);

            if (drgevent.Data.GetDataPresent(typeof(Tile)))
            {
                drgevent.Effect = DragDropEffects.Link;

                this.PlaceTile(position, (Tile)drgevent.Data.GetData(typeof(Tile)));
            }
            else if (drgevent.Data.GetDataPresent(typeof(String)))
            {
                drgevent.Effect = DragDropEffects.Link;

                this.PlaceActor(position, (String)drgevent.Data.GetData(typeof(String)));
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
            }
        }

        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
        {
            base.OnQueryContinueDrag(qcdevent);
        }

        protected override void Initialise()
        {
            base.Initialise();

            this.ContentManager.RootDirectory = @"FSEGame\";

            this.batch = new SpriteBatch(this.GraphicsDevice);
            this.level = new EditorLevel(this.ContentManager, this.tilesetManager);
        }

        protected override void Draw()
        {
            base.Draw();

            this.batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            this.level.Render(this.batch, this.offsetX, this.offsetY);

            this.batch.End();
        }

        private void PlaceTile(CellPosition position, Tile tile)
        {
            this.level.AddCell(position.X - 1, position.Y - 1, tile);
        }

        private void PlaceActor(CellPosition position, String type)
        {
            this.level.AddActor(position.X - 1, position.Y - 1, type);
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