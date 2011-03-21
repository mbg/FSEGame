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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame.Actors
{
    /// <summary>
    /// 
    /// </summary>
    public class BridgeNPC : Actor
    {
        #region Instance Members
        private ActorProperties properties;
        private Tileset tileset;
        private Boolean torchAcquired = false;
        #endregion

        #region Properties
        public override bool Passable
        {
            get
            {
                return this.torchAcquired;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public BridgeNPC(ActorProperties properties)
        {
            this.properties = properties;

            this.tileset = new Tileset(16, 1, Convert.ToUInt16(properties.Properties["States"]));
            this.tileset.Load(GameBase.Singleton.Content, properties.Properties["Tileset"]);
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (FSEGame.Singleton.PersistentStorage.ContainsKey(
                this.properties.Properties["TorchID"]))
            {
                PersistentStorageItem item =
                    FSEGame.Singleton.PersistentStorage[this.properties.Properties["TorchID"]];

                if (item.ItemType == PersistentStorageItemType.Number)
                {
                    this.torchAcquired = false;
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (this.torchAcquired)
                return;

            this.tileset.DrawTile(batch, 0, this.AbsolutePosition);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::