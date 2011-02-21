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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
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
        private Texture2D texture;
        private UInt16 size = 16;
        private UInt16 rows = 1;
        private UInt16 columns = 1;
        private Boolean initialised = false;
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

        public void Load(ContentManager contentManager, String name)
        {
            if (contentManager == null)
                throw new ArgumentNullException("contentManager");
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (this.initialised)
                throw new InvalidOperationException("Tileset is already initialised.");

            this.name = name;

            // :: Load the texture tileset texture.
            this.texture = contentManager.Load<Texture2D>(name);

            this.initialised = true;
        }

        #region DrawTile
        /// <summary>
        /// Draws a single tile at the specified position.
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="tile"></param>
        /// <param name="position"></param>
        public void DrawTile(SpriteBatch batch, UInt32 tile, Vector2 position)
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

            batch.Draw(this.texture, position, sourceRectangle, Color.White, .0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0.0f);
            
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::