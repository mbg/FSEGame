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
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Level
    {
        #region Instance Members
        private String name;
        private String tilesetFilename;
        private UInt32 width;
        private UInt32 height;
        private List<LevelEntryPoint> entryPoints;
        private LevelCell[][] ncells;
        private List<LevelCell> cells;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Level(ContentManager contentManager, String filename)
        {
            if (contentManager == null)
                throw new ArgumentNullException("contentManager");

            // :: Generate the full pathname and verify that a file
            // :: with the specified name exists.
            String path = Path.Combine(contentManager.RootDirectory, filename);

            if (!File.Exists(path))
                throw new FileNotFoundException(null, path);

            // :: Parse the XML document and load its data into memory.
            this.Load(path);
        }
        #endregion

        private void Load(String filename)
        {
            this.entryPoints = new List<LevelEntryPoint>();
            this.cells = new List<LevelCell>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlElement rootElement = doc.DocumentElement;

            if (!rootElement.HasAttribute("Name"))
                throw new LevelLoadException("Trying to load a level but it has no name!");

            this.name = rootElement.GetAttribute("Name");

            if (!rootElement.HasAttribute("Tileset"))
                throw new LevelLoadException("Trying to load a level but it has no tileset!");

            this.tilesetFilename = rootElement.GetAttribute("Tileset");

            FSEGame.Singleton.LoadTileset(this.tilesetFilename);


            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                // :: We only care about elements.
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)childNode;

                if (childElement.Name.Equals("EntryPoints"))
                {
                    this.LoadEntryPoints(childElement);
                }
                else if (childElement.Name.Equals("Cells"))
                {
                    this.LoadCells(childElement);
                }
            }
        }

        #region LoadEntryPoints
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        private void LoadEntryPoints(XmlElement root)
        {
            foreach (XmlNode childNode in root.ChildNodes)
            {
                // :: We only care about elements.
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)childNode;

                if (childElement.Name.Equals("EntryPoint"))
                {
                    LevelEntryPoint entryPoint = new LevelEntryPoint(childElement.GetAttribute("Name"));
                    entryPoint.X = Convert.ToUInt32(childElement.GetAttribute("X"));
                    entryPoint.Y = Convert.ToUInt32(childElement.GetAttribute("Y"));

                    this.entryPoints.Add(entryPoint);
                }
            }
        }
        #endregion

        #region LoadCells
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        private void LoadCells(XmlElement root)
        {
            foreach (XmlNode childNode in root.ChildNodes)
            {
                // :: We only care about elements.
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)childNode;

                if (childElement.Name.Equals("Cell"))
                {
                    LevelCell cell = new LevelCell();

                    cell.Tile = FSEGame.Singleton.CurrentTileset.GetTileID(childElement.GetAttribute("Tile"));
                    cell.X = Convert.ToUInt32(childElement.GetAttribute("X"));
                    cell.Y = Convert.ToUInt32(childElement.GetAttribute("Y"));
                    cell.EventType = CellEventType.None;
                    cell.EventID = String.Empty;

                    if (childElement.HasAttribute("Event"))
                    {
                        cell.EventType = this.ToCellEventType(childElement.GetAttribute("Event"));
                    }
                    if (childElement.HasAttribute("EventID"))
                    {
                        cell.EventID = childElement.GetAttribute("EventID");
                    }

                    this.cells.Add(cell);
                }
            }
        }
        #endregion

        #region ToCellEventType
        private CellEventType ToCellEventType(String name)
        {
            switch (name.Trim().ToUpper())
            {
                case "CHANGELEVEL":
                    return CellEventType.ChangeLevel;
            }

            return CellEventType.None;
        }
        #endregion

        public void DrawLevel(SpriteBatch batch)
        {
            foreach (LevelCell cell in this.cells)
            {
                FSEGame.Singleton.CurrentTileset.DrawTile(
                    batch, cell.Tile, GridHelper.GridPositionToAbsolute(new Vector2(cell.X, cell.Y)));

                /*if(cell.EventType != CellEventType.None)
                {
                    batch.DrawString(
                        FSEGame.Singleton.DefaultFont,
                        cell.EventID,
                        GridHelper.GridPositionToAbsolute(new Vector2(cell.X, cell.Y)) + FSEGame.Singleton.Camera.Offset,
                        Color.White,
                        0.0f,
                        Vector2.Zero,
                        2.0f,
                        SpriteEffects.None,
                        0.0f); 
                }*/

            }
        }

        public Boolean IsValidPosition(Vector2 position)
        {
            foreach (LevelCell cell in this.cells)
            {
                if ((cell.X == position.X) && (cell.Y == position.Y))
                    return FSEGame.Singleton.CurrentTileset.GetTile(cell.Tile).Passable;
            }

            return false;
        }

        public CellInformation GetCellInformation(Vector2 position)
        {
            foreach (LevelCell cell in this.cells)
            {
                if ((cell.X == position.X) && (cell.Y == position.Y))
                    return new CellInformation(FSEGame.Singleton.CurrentTileset.GetTile(cell.Tile), cell);
            }

            return null;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::