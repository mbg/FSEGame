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
        private String name = "Untitled";
        private String tilesetFilename;
        private String scriptFilename;
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

        private Vector2 GetDimensions()
        {
            Vector2 size = new Vector2(1, 1);

            foreach (LevelCell cell in this.cells)
            {
                if (cell.X + 1 > size.X)
                    size.X = cell.X + 1;
                if (cell.Y + 1 > size.Y)
                    size.Y = cell.Y + 1;
            }

            return size;
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

        #region Save
        /// <summary>
        /// Saves the level to disk.
        /// </summary>
        /// <param name="filename"></param>
        public void Save(String filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Level");

            Vector2 dimensions = this.GetDimensions();

            // :: Create the attributes for the root element.
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            XmlAttribute tilesetAttribute = doc.CreateAttribute("Tileset");
            XmlAttribute scriptAttribute = doc.CreateAttribute("Script");
            XmlAttribute sizeXAttribute = doc.CreateAttribute("SizeX");
            XmlAttribute sizeYAttribute = doc.CreateAttribute("SizeY");

            nameAttribute.Value = this.name;
            tilesetAttribute.Value = this.tilesetFilename;
            scriptAttribute.Value = this.scriptFilename;
            sizeXAttribute.Value = dimensions.X.ToString();
            sizeYAttribute.Value = dimensions.Y.ToString();

            root.Attributes.Append(nameAttribute);
            root.Attributes.Append(tilesetAttribute);
            root.Attributes.Append(scriptAttribute);
            root.Attributes.Append(sizeXAttribute);
            root.Attributes.Append(sizeYAttribute);

            // :: Save all entry points to the file.

            // :: Save all cells to the file.
            XmlElement cellsElement = doc.CreateElement("Cells");

            foreach (LevelCell cell in this.cells)
            {
                XmlElement cellElement = doc.CreateElement("Cell");
                XmlAttribute xAttribute = doc.CreateAttribute("X");
                XmlAttribute yAttribute = doc.CreateAttribute("Y");
                XmlAttribute tileAttribute = doc.CreateAttribute("Tile");

                xAttribute.Value = cell.X.ToString();
                yAttribute.Value = cell.Y.ToString();
                tileAttribute.Value = cell.Tile.Name;

                cellElement.Attributes.Append(xAttribute);
                cellElement.Attributes.Append(yAttribute);
                cellElement.Attributes.Append(tileAttribute);

                // :: Append an event element if required.
                if (!String.IsNullOrWhiteSpace(cell.EventID))
                {
                    XmlAttribute eventAttribute = doc.CreateAttribute("Event");
                    eventAttribute.Value = cell.EventID;

                    cellElement.Attributes.Append(eventAttribute);
                }

                cellsElement.AppendChild(cellElement);
            }

            // :: Save all actors (TODO)

            // :: End the XML document and save it to disk.
            root.AppendChild(cellsElement);

            doc.AppendChild(root);
            doc.Save(filename);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::