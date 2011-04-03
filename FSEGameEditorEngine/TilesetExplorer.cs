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
using FSEGame.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using System.Collections.Generic;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class TilesetExplorer : LevelEditorControl
    {
        #region Instance Members
        private Tileset activeTileset = null;
        private SpriteBatch batch;
        private float offset = 0.0f;
        private List<Tile> tiles;
        private DateTime gameStarted;
        private DateTime lastFrameEnded;
        private GameTime time;
        #endregion 

        #region Properties
        /// <summary>
        /// Gets or sets the tileset to display.
        /// </summary>
        public Tileset ActiveTileset
        {
            get
            {
                return this.activeTileset;
            }
            set
            {
                this.activeTileset = value;
                this.offset = 0.0f;
                this.tiles.Clear();

                if (this.activeTileset == null)
                    return;

                foreach (Tile t in this.activeTileset.Tiles.Values)
                    this.tiles.Add(t);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public TilesetExplorer()
        {
            this.tiles = new List<Tile>();
            this.time = new GameTime();
            this.gameStarted = DateTime.Now;
            this.lastFrameEnded = DateTime.Now;

            this.SetStyle(ControlStyles.Selectable, true);
            this.UpdateStyles();
        }
        #endregion

        protected override void Initialise()
        {
            base.Initialise();

            this.batch = new SpriteBatch(this.GraphicsDevice);
        }

        protected override void Draw()
        {
            base.Draw();

            if (this.activeTileset == null)
                return;

            this.time = new GameTime(
                DateTime.Now.Subtract(this.gameStarted),
                DateTime.Now.Subtract(this.lastFrameEnded));

            this.activeTileset.Update(this.time);

            this.batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null);

            float x = 5.0f;
            float y = 5.0f;

            foreach (Tile t in this.activeTileset.Tiles.Values)
            {
                activeTileset.DrawTile(
                    this.batch,
                    t.ID,
                    new Vector2(x, y + offset));

                x += 69.0f;

                if (x + 69.0f > this.ClientSize.Width)
                {
                    x = 5.0f;
                    y += 69.0f;
                }
            }

            this.batch.End();

            this.lastFrameEnded = DateTime.Now;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            this.Focus();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                Tile t = this.GetTileAtPosition(e.X, e.Y);

                if (t != null)
                {
                    this.DoDragDrop(t, DragDropEffects.Link);
                }
            }

            this.Focus();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Int32 rowHeight = this.ComputeRowHeight();
            Int32 maxOffset = 0;

            if (rowHeight > this.ClientSize.Height)
                maxOffset += (rowHeight - this.ClientSize.Height) + 5;

            if (this.offset + e.Delta > 0.0f)
                this.offset = 0.0f;
            else if (this.offset + e.Delta < -maxOffset)
                this.offset = -maxOffset;
            else
                this.offset += e.Delta;

            this.Invalidate();
        }

        private Int32 ComputeTilesPerRow()
        {
            return this.ClientSize.Width / 69;
        }

        private Int32 ComputeNumberOfRows()
        {
            Int32 tilesPerRow = this.ComputeTilesPerRow();
            Int32 fullRows = this.activeTileset.Tiles.Count / tilesPerRow;
            Int32 overflow = this.activeTileset.Tiles.Count % tilesPerRow;

            return fullRows + ((overflow == 0) ? 0 : 1);
        }

        private Int32 ComputeRowHeight()
        {
            return this.ComputeNumberOfRows() * 69;
        }

        public Tile GetTileAtPosition(int x, int y)
        {
            CellPosition position = this.ControlPositionToActual(x, y);
           
            Int32 rowCells = (position.Y - 1) * this.ComputeTilesPerRow();
            Int32 cell = rowCells + position.X;

            if (cell <= this.tiles.Count)
                return this.tiles[cell - 1];

            return null;
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
            Int32 column = (Int32)Math.Ceiling(x / 69.0f);

            if (column == 0) column = 1;

            // :: Next, calculate the row. This is a bit more difficult
            // :: because we have to consider the scroll offset;
            y = y - (int)this.offset;

            Int32 row = (Int32)Math.Ceiling(y / 69.0f);

            if (row == 0) row = 1;

            // :: Not all control space coordinates map to tile
            // :: coordinates.
            return new CellPosition(column, row);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::