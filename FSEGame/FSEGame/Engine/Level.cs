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
    public delegate Actor CreateActorDelegate(ActorProperties properties);

    /// <summary>
    /// 
    /// </summary>
    public class Level
    {
        #region Instance Members
        private String name;
        private String levelFilename;
        private String tilesetFilename;
        private UInt32 width;
        private UInt32 height;
        private List<LevelEntryPoint> entryPoints;
        private List<Actor> actors;
        private LevelCell[,] cells;
        private Boolean levelLoaded = false;
        #endregion

        #region Events
        private event CreateActorDelegate onCreateActor = null;
        #endregion

        #region Properties
        public String Name
        {
            get
            {
                return this.name;
            }
        }

        public String LevelFilename
        {
            get
            {
                return this.levelFilename;
            }
        }
        #endregion

        #region Event Properties
        public event CreateActorDelegate OnCreateActor
        {
            add
            {
                this.onCreateActor += value;
            }
            remove
            {
                this.onCreateActor -= value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Level()
        {
            
        }
        #endregion

        public void Load(ContentManager contentManager, String filename)
        {
            if (contentManager == null)
                throw new ArgumentNullException("contentManager");

            // :: Generate the full pathname and verify that a file
            // :: with the specified name exists.
            this.levelFilename = Path.Combine(contentManager.RootDirectory, filename);

            if (!File.Exists(this.levelFilename))
                throw new FileNotFoundException(null, this.levelFilename);

            this.entryPoints = new List<LevelEntryPoint>();
            this.actors = new List<Actor>();

            XmlDocument doc = new XmlDocument();
            doc.Load(this.levelFilename);

            XmlElement rootElement = doc.DocumentElement;

            // :: Get the name of the level.
            if (!rootElement.HasAttribute("Name"))
                throw new LevelLoadException("Trying to load a level but it has no name!");

            this.name = rootElement.GetAttribute("Name");

            // :: Get the name of the tileset used by the level.
            if (!rootElement.HasAttribute("Tileset"))
                throw new LevelLoadException("Trying to load a level but it has no tileset!");

            this.tilesetFilename = rootElement.GetAttribute("Tileset");

            GameBase.Singleton.LoadTileset(this.tilesetFilename);

            if (!rootElement.HasAttribute("SizeX"))
                throw new LevelLoadException("Trying to load a level but no width was specified!");
            if (!rootElement.HasAttribute("SizeY"))
                throw new LevelLoadException("Trying to load a level but no height was specified!");

            this.width = Convert.ToUInt32(rootElement.GetAttribute("SizeX"));
            this.height = Convert.ToUInt32(rootElement.GetAttribute("SizeY"));
            this.cells = new LevelCell[this.height,this.width];

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
                else if (childElement.Name.Equals("Actors"))
                {
                    this.LoadActors(childElement);
                }
            }

            this.levelLoaded = true;
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

                    cell.Tile = GameBase.Singleton.CurrentTileset.GetTile(childElement.GetAttribute("Tile"));
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

                    this.cells[cell.Y, cell.X] = cell;
                }
            }
        }
        #endregion

        #region LoadActors
        /// <summary>
        /// Loads a list of actors from the specified XmlElement.
        /// </summary>
        /// <param name="root"></param>
        private void LoadActors(XmlElement root)
        {
            // :: The OnCreateActor event is required to initialise actor classes,
            // :: do not proceed if it is not set.
            if (this.onCreateActor == null)
                return;

            foreach (XmlNode childNode in root.ChildNodes)
            {
                // :: We only care about elements.
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                // :: This code is only responsible for parsing the actor's properties
                // :: from the XML file. Everything else is up to the OnCreateActor
                // :: event handler which may be defined in one of the game's classes.
                XmlElement childElement = (XmlElement)childNode;

                if (childElement.Name.Equals("Actor"))
                {
                    ActorProperties properties = new ActorProperties(
                        childElement.GetAttribute("Type"),
                        Convert.ToUInt32(childElement.GetAttribute("X")),
                        Convert.ToUInt32(childElement.GetAttribute("Y")));

                    foreach (XmlNode propertyNode in childElement.ChildNodes)
                    {
                        // :: We only care about elements.
                        if (propertyNode.NodeType != XmlNodeType.Element)
                            continue;

                        XmlElement propertyElement = (XmlElement)propertyNode;

                        if (propertyElement.Name.Equals("Property"))
                        {
                            properties.Properties.Add(
                                propertyElement.GetAttribute("Name"),
                                propertyElement.InnerText);
                        }
                    }

                    // :: Call the OnCreateActor event handler to attempt to
                    // :: initialise the actor using the given properties.
                    Actor act = this.onCreateActor(properties);

                    if(act != null)
                        this.actors.Add(act);
                }
            }
        }
        #endregion

        #region ToCellEventType
        /// <summary>
        /// Converts a System.String into a CellEventType.
        /// </summary>
        /// <param name="name">The string to convert.</param>
        /// <returns></returns>
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

        #region Update
        /// <summary>
        /// Updates the actors in this level.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (!this.levelLoaded)
                return;

            foreach (Actor a in this.actors)
            {
                a.Update(gameTime);
            }
        }
        #endregion

        #region DrawLevel
        /// <summary>
        /// Renders the level.
        /// </summary>
        /// <param name="batch"></param>
        public void DrawLevel(SpriteBatch batch)
        {
            if (!this.levelLoaded)
                return;

            UInt32 y = 0;
            UInt32 x = 0;

            // :: Firstly, render all the level tiles so they appear at the lowest
            // :: layer - then render the actors on top of the tiles so they are
            // :: visibile on screen.
            for (; y < this.height; y++)
            {
                for (x = 0; x < this.width; x++)
                {
                    Vector2 position = new Vector2(x, y);

                    if (this.IsVisible(position))
                    {
                        GameBase.Singleton.CurrentTileset.DrawTile(
                            batch,
                            this.cells[y, x].Tile.ID,
                            GridHelper.GridPositionToAbsolute(position));
                    }
                }
            }

            foreach (Actor a in this.actors)
            {
                a.Draw(batch);
            }
        }
        #endregion

        #region IsVisible
        /// <summary>
        /// Gets a value indicating whether the tile at the specified
        /// position is visible or not.
        /// </summary>
        /// <param name="position">The grid position of the tile.</param>
        /// <returns></returns>
        public Boolean IsVisible(Vector2 position)
        {
            Vector2 absolute = GridHelper.GridPositionToAbsolute(position);

            return true;
        }
        #endregion

        #region IsValidPosition
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Boolean IsValidPosition(Vector2 position)
        {
            foreach (LevelCell cell in this.cells)
            {
                if ((cell.X == position.X) && (cell.Y == position.Y))
                    return cell.Tile.Passable;
            }

            return false;
        }
        #endregion

        #region GetCellInformation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public CellInformation GetCellInformation(Vector2 position)
        {
            if (position.X < 0 || position.Y < 0)
                return null;

            UInt32 x = Convert.ToUInt32(position.X);
            UInt32 y = Convert.ToUInt32(position.Y);

            if (x > this.width - 1 || y > this.height - 1)
                return null;

            return new CellInformation(
                this.cells[y, x].Tile,
                this.cells[y, x]);
        }
        #endregion

        public LevelEntryPoint GetEntryPoint(String name)
        {
            foreach (LevelEntryPoint entryPoint in this.entryPoints)
            {
                if(entryPoint.Name.Equals(name))
                    return entryPoint;
            }

            throw new ArgumentException("No entry point with this name.");
        }

        public Boolean CanMoveTo(Vector2 position)
        {
            // :: Negative coordinates are invalid.
            if (position.X < 0 || position.Y < 0)
                return false;

            // :: Coordinates outside the level are invalid.
            if (position.X > this.width - 1 || position.Y > this.height - 1)
                return false;

            // :: If the tile type is not passable, one cannot move to
            // :: a cell of that tile type.
            Tile t = this.cells[(int)position.Y, (int)position.X].Tile;

            if (!t.Passable)
                return false;

            // :: Lastly, if there is an entity occupying the cell,
            // :: another entity cannot move there.
            foreach (Actor a in this.actors)
            {
                if (a.CellPosition == position)
                    return false;
            }

            return true;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::