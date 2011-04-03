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
using FSEGame.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditorLevel
    {
        #region Instance Members
        private List<LevelCell> cells;
        private Tileset tileset;
        #endregion

        #region Properties
        public Tileset Tileset
        {
            get
            {
                return this.tileset;
            }
            set
            {
                this.tileset = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public EditorLevel()
        {
            this.cells = new List<LevelCell>();
        }
        #endregion

        public void Render(SpriteBatch batch, Int32 offsetX, Int32 offsetY)
        {
            foreach (LevelCell c in this.cells)
            {
                Vector2 absolutePosition =
                    GridHelper.GridPositionToAbsolute(new Vector2(c.X, c.Y));

                absolutePosition.X -= offsetX;
                absolutePosition.Y -= offsetY;

                this.tileset.DrawTile(
                    batch,
                    c.Tile.ID,
                    absolutePosition);
            }
        }

        public void AddCell(Int32 x, Int32 y, Tile t)
        {
            LevelCell cell = new LevelCell();
            cell.Tile = t;
            cell.X = (UInt32)x;
            cell.Y = (UInt32)y;

            if (this.HasCellAtPosition(x, y))
                this.cells.Remove(this.GetCellAtPosition(x, y));

            this.cells.Add(cell);
        }

        public LevelCell GetCellAtPosition(Int32 x, Int32 y)
        {
            foreach (LevelCell cell in this.cells)
            {
                if ((cell.X == x) && (cell.Y == y))
                    return cell;
            }

            throw new Exception("No cell at the specified position");
        }

        public Boolean HasCellAtPosition(Int32 x, Int32 y)
        {
            foreach (LevelCell cell in this.cells)
            {
                if ((cell.X == x) && (cell.Y == y))
                    return true;
            }

            return false;
        }

        #region Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void Load(String filename)
        {
            if (!File.Exists(filename))
                throw new LevelLoadException("File not found");

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

                XmlElement root = doc.DocumentElement;

                this.cells.Clear();
            }
            catch (XmlException ex)
            {
                throw new LevelLoadException(
                    "Invalid XML", ex);
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::