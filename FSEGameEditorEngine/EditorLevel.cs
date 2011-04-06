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
using Microsoft.Xna.Framework.Content;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditorLevel
    {
        #region Instance Members
        private TilesetManager tilesetManager;
        private String name = "Untitled";
        private String tilesetFilename;
        private String scriptFilename;
        private List<LevelCell> cells;
        private List<LevelEntryPoint> entryPoints;
        private List<ActorProperties> actors;
        private Tileset tileset;
        private Texture2D actorsTexture;
        private Texture2D entryPointTexture;
        private Texture2D eventTexture;
        private Texture2D passableTexture;
        private Texture2D impassableTexture;
        private Boolean showActors = true;
        private Boolean showEntryPoints = true;
        private Boolean showEvents = true;
        private Boolean showPassableRegions = false;
        private Boolean showImpassableRegions = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        /// <summary>
        /// Gets or sets the filename of the script that is associated with
        /// this level.
        /// </summary>
        public String ScriptFilename
        {
            get
            {
                return this.scriptFilename;
            }
            set
            {
                this.scriptFilename = value;
            }
        }
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
        /// <summary>
        /// Gets or sets whether actor overlays show be shown.
        /// </summary>
        public Boolean ShowActors
        {
            get
            {
                return this.showActors;
            }
            set
            {
                this.showActors = value;
            }
        }
        /// <summary>
        /// Gets or sets whether entry point overlays should be shown.
        /// </summary>
        public Boolean ShowEntryPoints
        {
            get
            {
                return this.showEntryPoints;
            }
            set
            {
                this.showEntryPoints = value;
            }
        }
        /// <summary>
        /// Gets or sets whether event overlays should be shown.
        /// </summary>
        public Boolean ShowEvents
        {
            get
            {
                return this.showEvents;
            }
            set
            {
                this.showEvents = value;
            }
        }

        public Boolean ShowPassableRegions
        {
            get
            {
                return this.showPassableRegions;
            }
            set
            {
                this.showPassableRegions = value;
            }
        }
        public Boolean ShowImpassableRegions
        {
            get
            {
                return this.showImpassableRegions;
            }
            set
            {
                this.showImpassableRegions = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public EditorLevel(ContentManager contentManager, TilesetManager tilesetManager)
        {
            this.tilesetManager = tilesetManager;
            this.cells = new List<LevelCell>();
            this.entryPoints = new List<LevelEntryPoint>();
            this.actors = new List<ActorProperties>();

            this.eventTexture = contentManager.Load<Texture2D>(@"EditorContent\Event");
            this.passableTexture = contentManager.Load<Texture2D>(@"EditorContent\Passable");
            this.impassableTexture = contentManager.Load<Texture2D>(@"EditorContent\Impassable");
            this.entryPointTexture = contentManager.Load<Texture2D>(@"EditorContent\EntryPoint");
            this.actorsTexture = contentManager.Load<Texture2D>(@"EditorContent\Actor");
        }
        #endregion

        public void New()
        {
            this.cells = new List<LevelCell>();
            this.entryPoints = new List<LevelEntryPoint>();
            this.actors = new List<ActorProperties>();
            this.name = "Untitled";
            this.scriptFilename = "";
            this.tilesetFilename = "";
        }

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

                if (c.Tile.Passable && this.showPassableRegions)
                {
                    this.DrawOverlay(batch, absolutePosition, this.passableTexture, 0.5f);
                }
                else if(!c.Tile.Passable && this.showImpassableRegions)
                {
                    this.DrawOverlay(batch, absolutePosition, this.impassableTexture, 0.5f);
                }

                if (!String.IsNullOrWhiteSpace(c.EventID) && this.showEvents)
                {
                    this.DrawOverlay(batch, absolutePosition, this.eventTexture, 1.0f);
                }
            }

            if (this.showEntryPoints)
            {
                foreach (LevelEntryPoint ep in this.entryPoints)
                {
                    Vector2 absolutePosition =
                        GridHelper.GridPositionToAbsolute(new Vector2(ep.X, ep.Y));

                    absolutePosition.X -= offsetX;
                    absolutePosition.Y -= offsetY;

                    this.DrawOverlay(batch, absolutePosition, this.entryPointTexture, 1.0f);
                }
            }

            if (this.showActors)
            {
                foreach (ActorProperties actor in this.actors)
                {
                    Vector2 absolutePosition =
                        GridHelper.GridPositionToAbsolute(new Vector2(actor.X, actor.Y));

                    absolutePosition.X -= offsetX;
                    absolutePosition.Y -= offsetY;

                    this.DrawOverlay(batch, absolutePosition, this.actorsTexture, 1.0f);
                }
            }
        }

        private void DrawOverlay(SpriteBatch batch, Vector2 position, Texture2D texture, float opacity)
        {
            batch.Draw(
                texture,
                new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    64,
                    64),
                new Color(1.0f, 1.0f, 1.0f, opacity));
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

            this.cells.Clear();
            this.entryPoints.Clear();
            this.actors.Clear();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

                XmlElement root = doc.DocumentElement;

                this.name = root.GetAttribute("Name");
                this.tilesetFilename = root.GetAttribute("Tileset");
                this.scriptFilename = root.GetAttribute("Script");

                // :: Find the tileset used by the level. 
                this.tileset = this.tilesetManager.GetByFilename(this.tilesetFilename);

                if (this.tileset == null)
                    throw new LevelLoadException("Cannot load level because its tileset could not be found.");

                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                        continue;

                    XmlElement childElement = (XmlElement)node;

                    if (childElement.Name.Equals("EntryPoints"))
                    {
                        // :: Extract the names and positions of the entry points in this
                        // :: level.
                        foreach (XmlNode entryPointNode in childElement.ChildNodes)
                        {
                            if (entryPointNode.NodeType != XmlNodeType.Element)
                                continue;

                            XmlElement entryPointElement = (XmlElement)entryPointNode;

                            LevelEntryPoint entryPoint = new LevelEntryPoint(
                                entryPointElement.GetAttribute("Name"));
                            entryPoint.X = Convert.ToUInt32(entryPointElement.GetAttribute("X"));
                            entryPoint.Y = Convert.ToUInt32(entryPointElement.GetAttribute("Y"));

                            this.entryPoints.Add(entryPoint);
                        }
                    }
                    else if (childElement.Name.Equals("Cells"))
                    {
                        foreach (XmlNode cellNode in childElement.ChildNodes)
                        {
                            if (cellNode.NodeType != XmlNodeType.Element)
                                continue;

                            XmlElement cellElement = (XmlElement)cellNode;

                            LevelCell cell = new LevelCell();
                            cell.X = Convert.ToUInt32(cellElement.GetAttribute("X"));
                            cell.Y = Convert.ToUInt32(cellElement.GetAttribute("Y"));
                            cell.Tile = this.tileset.GetTile(cellElement.GetAttribute("Tile"));

                            if (cellElement.HasAttribute("Event"))
                            {
                                cell.EventID = cellElement.GetAttribute("Event");
                            }

                            this.cells.Add(cell);
                        }
                    }
                    else if (childElement.Name.Equals("Actors"))
                    {
                        foreach (XmlNode actorNode in childElement.ChildNodes)
                        {
                            if (actorNode.NodeType != XmlNodeType.Element)
                                continue;

                            XmlElement actorElement = (XmlElement)actorNode;

                            ActorProperties actor = new ActorProperties(
                                actorElement.GetAttribute("Type"),
                                Convert.ToUInt32(actorElement.GetAttribute("X")),
                                Convert.ToUInt32(actorElement.GetAttribute("Y")));

                            foreach (XmlNode propertyNode in actorElement.ChildNodes)
                            {
                                if (propertyNode.NodeType != XmlNodeType.Element)
                                    continue;

                                XmlElement propertyElement = (XmlElement)propertyNode;

                                actor.Properties.Add(
                                    propertyElement.GetAttribute("Name"),
                                    propertyElement.InnerText);
                            }

                            this.actors.Add(actor);
                        }
                    }
                }
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
            XmlElement entryPointsElement = doc.CreateElement("EntryPoints");

            foreach (LevelEntryPoint ep in this.entryPoints)
            {
                XmlElement entryPointElement = doc.CreateElement("EntryPoint");
                XmlAttribute epnAttribute = doc.CreateAttribute("Name");
                XmlAttribute xAttribute = doc.CreateAttribute("X");
                XmlAttribute yAttribute = doc.CreateAttribute("Y");

                epnAttribute.Value = ep.Name;
                xAttribute.Value = ep.X.ToString();
                yAttribute.Value = ep.Y.ToString();

                entryPointElement.Attributes.Append(epnAttribute);
                entryPointElement.Attributes.Append(xAttribute);
                entryPointElement.Attributes.Append(yAttribute);

                entryPointsElement.AppendChild(entryPointElement);
            }

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

            // :: Save all actors
            XmlElement actorsElement = doc.CreateElement("Actors");

            foreach (ActorProperties actor in this.actors)
            {
                XmlElement actorElement = doc.CreateElement("Actor");
                XmlAttribute xAttribute = doc.CreateAttribute("X");
                XmlAttribute yAttribute = doc.CreateAttribute("Y");
                XmlAttribute typeAttribute = doc.CreateAttribute("Type");

                xAttribute.Value = actor.X.ToString();
                yAttribute.Value = actor.Y.ToString();
                typeAttribute.Value = actor.Type;

                actorElement.Attributes.Append(xAttribute);
                actorElement.Attributes.Append(yAttribute);
                actorElement.Attributes.Append(typeAttribute);

                foreach (String key in actor.Properties.Keys)
                {
                    XmlElement propertyElement = doc.CreateElement("Property");
                    XmlAttribute keyAttribute = doc.CreateAttribute("Name");

                    keyAttribute.Value = key;

                    propertyElement.Attributes.Append(keyAttribute);
                    propertyElement.InnerText = actor.Properties[key];

                    actorElement.AppendChild(propertyElement);
                }

                actorsElement.AppendChild(actorElement);
            }

            // :: End the XML document and save it to disk.
            root.AppendChild(entryPointsElement);
            root.AppendChild(cellsElement);
            root.AppendChild(actorsElement);

            doc.AppendChild(root);
            doc.Save(filename);
        }
        #endregion

        #region GetCellAtPosition
        /// <summary>
        /// Gets the cell at the specified position or null if there is no
        /// cell at the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public LevelCell GetCellAtPosition(CellPosition position)
        {
            foreach (LevelCell cell in this.cells)
            {
                if (cell.X == position.X && cell.Y == position.Y)
                    return cell;
            }

            return null;
        }
        #endregion

        public ActorProperties GetActorAtPosition(CellPosition position)
        {
            foreach (ActorProperties actor in this.actors)
            {
                if (actor.X == position.X && actor.Y == position.Y)
                    return actor;
            }

            return null;
        }

        public LevelEntryPoint GetEntryPointAtPosition(CellPosition position)
        {
            foreach (LevelEntryPoint ep in this.entryPoints)
            {
                if (ep.X == position.X && ep.Y == position.Y)
                    return ep;
            }

            return null;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::