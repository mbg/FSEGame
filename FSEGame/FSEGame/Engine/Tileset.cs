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
        private String name;
        private String resource;
        private Texture2D texture;
        private TileCollection tiles;
        private UInt16 size = 16;
        private UInt16 rows = 1;
        private UInt16 columns = 1;
        private Boolean initialised = false;
        private const int ERROR_TILE = 7;
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
                    Tile t = new Tile();
                    t.Name = childElement.InnerText;
                    t.ID = Convert.ToUInt32(childElement.GetAttribute("ID"));
                    t.Passable = Convert.ToBoolean(childElement.GetAttribute("Passable"));

                    // :: Add the tile to the index.
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
            batch.Draw(
                this.texture,
                position + FSEGame.Singleton.Camera.Offset,
                sourceRectangle,
                Color.White,
                rotation,
                Vector2.Zero,
                4.0f,
                SpriteEffects.None,
                1.0f);
        }
        #endregion

        public UInt32 GetTileID(String name)
        {
            try
            {
                return this.tiles[name].ID;
            }
            catch
            {
                return ERROR_TILE;
            }
        }

        public Tile GetTile(UInt32 id)
        {
            foreach (Tile t in this.tiles.Values)
            {
                if (t.ID == id || id == ERROR_TILE)
                {
                    return t;
                }
            }

            throw new Exception("No Tile with specified id.");
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::