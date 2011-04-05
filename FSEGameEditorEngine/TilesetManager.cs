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
using System.IO;
using FSEGame.Engine;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class TilesetManager
    {
        #region Instance Members
        private ContentManager contentManager;
        private List<Tileset> tilesets;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a list of all tilesets that have been loaded.
        /// </summary>
        public List<Tileset> Tilesets
        {
            get
            {
                return this.tilesets;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public TilesetManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            this.tilesets = new List<Tileset>();
        }
        #endregion

        #region LoadTilesets
        /// <summary>
        /// Loads all tilesets from the "Tilesets" subdirectory.
        /// </summary>
        public void LoadTilesets()
        {
            String path = Path.Combine(this.contentManager.RootDirectory, @"Tilesets\");
            DirectoryInfo dir = new DirectoryInfo(path);

            this.tilesets.Clear();

            foreach (FileInfo file in dir.GetFiles("*.xml"))
            {
                try
                {
                    Tileset tileset = new Tileset(16, 6, 8);
                    tileset.Load(this.contentManager, Path.Combine(@"Tilesets\", file.Name));

                    this.tilesets.Add(tileset);
                }
                catch (Exception ex)
                {
                    // todo: handle error
                }
            }
        }
        #endregion

        public Tileset GetByFilename(String filename)
        {
            foreach (Tileset t in this.tilesets)
            {
                if (t.Filename.Equals(filename))
                    return t;
            }

            return null;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::