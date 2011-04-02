// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   Manages a single tileset and allows to render individual tiles.
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Tileset
    {
        #region Instance Members
        /// <summary>
        /// The name of the tileset.
        /// </summary>
        private String name;
        /// <summary>
        /// The filename of the tileset texture.
        /// </summary>
        private String resource;
        /// <summary>
        /// The tileset texture.
        /// </summary>
        private Texture2D texture;
        /// <summary>
        /// The collection of tiles in this tileset.
        /// </summary>
        private TileCollection tiles;
        /// <summary>
        /// The width and height of each tile.
        /// </summary>
        private UInt16 size = 16;
        /// <summary>
        /// The number of rows in the tileset.
        /// </summary>
        private UInt16 rows = 1;
        /// <summary>
        /// The number of columns in the tileset.
        /// </summary>
        private UInt16 columns = 1;
        /// <summary>
        /// A value indicating whether this tileset has been initialised
        /// or not.
        /// </summary>
        private Boolean initialised = false;
        #endregion

        #region Constants
        /// <summary>
        /// The ID of the error tile.
        /// </summary>
        private const UInt32 ERROR_TILE = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of this tileset.
        /// </summary>
        public String Name
        {
            get
            {
                return this.name;
            }
        }
        /// <summary>
        /// Gets the name of the tileset's resource.
        /// </summary>
        public String Resource
        {
            get
            {
                return this.resource;
            }
        }
        /// <summary>
        /// Gets the size (in pixels) of each  of the tiles' edges. This
        /// size to the power of two represents the total number of pixels
        /// in each tile.
        /// </summary>
        public UInt16 Size
        {
            get
            {
                return this.size;
            }
        }
        /// <summary>
        /// Gets the number of rows in this tileset.
        /// </summary>
        public UInt16 Rows
        {
            get
            {
                return this.rows;
            }
        }
        /// <summary>
        /// Gets the number of columns in this tileset.
        /// </summary>
        public UInt16 Columns
        {
            get
            {
                return this.columns;
            }
        }
        /// <summary>
        /// Gets a collection of all tiles in this tileset.
        /// </summary>
        public TileCollection Tiles
        {
            get
            {
                return this.tiles;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Tileset(UInt16 size, UInt16 rows, UInt16 columns)
        {
            this.size = size;
            this.rows = rows;
            this.columns = columns;
        }
        #endregion

        #region Load
        /// <summary>
        /// Loads the tileset from the game resources.
        /// </summary>
        /// <param name="contentManager">The content manager to use.</param>
        /// <param name="name">The relative filename of the XML file describing the tileset.</param>
        public void Load(ContentManager contentManager, String name)
        {
            if (contentManager == null)
                throw new ArgumentNullException("contentManager");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            String path = Path.Combine(contentManager.RootDirectory, name);

            if (!File.Exists(path))
                throw new FileNotFoundException(null, path);

            this.tiles = new TileCollection();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlElement rootElement = doc.DocumentElement;

            this.name = rootElement.GetAttribute("Name");
            this.resource = rootElement.GetAttribute("Resource");

            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;

                XmlElement childElement = (XmlElement)childNode;

                if(childElement.Name.Equals("Tile"))
                {
                    Tile t = new Tile(
                        childElement.InnerText,
                        Convert.ToBoolean(childElement.GetAttribute("Passable")));

                    t.Frames.Enqueue(Convert.ToUInt32(childElement.GetAttribute("ID")));

                    // :: Add the tile to the index.
                    this.tiles.Add(t.Name, t);
                }
                else if (childElement.Name.Equals("AnimatedTile"))
                {
                    Tile t = new Tile(
                        childElement.GetAttribute("Name"),
                        Convert.ToBoolean(childElement.GetAttribute("Passable")),
                        true);

                    t.Speed = Convert.ToSingle(childElement.GetAttribute("Speed"));

                    foreach (XmlNode frameNode in childElement.ChildNodes)
                    {
                        if (frameNode.NodeType != XmlNodeType.Element)
                            continue;

                        XmlElement frameElement = (XmlElement)frameNode;

                        if (frameElement.Name.Equals("Frame"))
                        {
                            t.Frames.Enqueue(
                                Convert.ToUInt32(frameElement.GetAttribute("ID")));
                        }
                    }

                    // :: Add the animated tile to the index.
                    this.tiles.Add(t.Name, t);
                }
            }

            // :: We can only initialise the tileset once.
            if (this.initialised)
                throw new InvalidOperationException("Tileset is already initialised.");

            // :: Load the texture tileset texture.
            this.texture = contentManager.Load<Texture2D>(this.resource);

            this.initialised = true;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the tiles in this tileset with the current game time.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (Tile t in this.tiles.Values)
            {
                t.Update(gameTime);
            }
        }
        #endregion

        #region DrawTile
        /// <summary>
        /// Draws a single tile at the specified position.
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="tile"></param>
        /// <param name="position"></param>
        public void DrawTile(SpriteBatch batch, UInt32 tile, Vector2 position)
        {
            this.DrawTile(batch, tile, position, 0.0f);
        }
        /// <summary>
        /// Draws a single tile at the specified position with the specified rotation.
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="tile"></param>
        /// <param name="position"></param>
        public void DrawTile(SpriteBatch batch, UInt32 tile, Vector2 position, float rotation)
        {
            if (!this.initialised)
                return;

            // :: Calculate the offset of the tile in the tileset.
            UInt32 column = tile / this.rows;
            UInt32 row = tile % this.rows;

            Rectangle sourceRectangle = new Rectangle(
                (Int32)(column * this.size),
                (Int32)(row * this.size),
                this.size, this.size);

            // :: Draw the tile from the tileset.
            Vector2 offset = Vector2.Zero;

            if (GameBase.Singleton != null)
            {
                offset = GameBase.Singleton.Offset;
            }

            batch.Draw(
                this.texture,
                position + offset,
                sourceRectangle,
                Color.White,
                rotation,
                Vector2.Zero,
                4.0f,
                SpriteEffects.None,
                1.0f);
        }
        #endregion

        #region GetTileID
        /// <summary>
        /// Gets the ID of the tile with the specified name.
        /// </summary>
        /// <param name="name">The name of the tile to find.</param>
        /// <returns>
        /// Returns the ID of the tile with the specified name or the 
        /// ID of the error tile if no tile with the specified name could 
        /// be found.
        /// </returns>
        public UInt32 GetTileID(String name)
        {
            if (this.tiles.ContainsKey(name))
                return this.tiles[name].ID;
            else
                return ERROR_TILE;
        }
        #endregion

        #region GetTile
        /// <summary>
        /// Gets the tile with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the tile to get.</param>
        /// <returns>Returns the tile with the specified ID.</returns>
        public Tile GetTile(UInt32 id)
        {
            foreach (Tile t in this.tiles.Values)
            {
                if (t.ID == id)
                {
                    return t;
                }
            }

            throw new Exception("No Tile with specified id.");
        }
        /// <summary>
        /// Gets the tile with the specified name.
        /// </summary>
        /// <param name="name">The name of the tile to get.</param>
        /// <returns>
        /// Returns the tile with the specified name or the error tile if no 
        /// tile with the specified name could be found.
        /// </returns>
        public Tile GetTile(String name)
        {
            if (this.tiles.ContainsKey(name))
                return this.tiles[name];
            else
                return this.GetTile(ERROR_TILE);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::