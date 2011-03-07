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
using System.IO;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// Represents a place where variables may be stored in such a way that they
    /// are saved when the game is saved and restored when a game state is loaded.
    /// </summary>
    public sealed class PersistentStorage : Dictionary<String, PersistentStorageItem>
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public PersistentStorage()
        {
            
        }
        #endregion

        #region Save
        /// <summary>
        /// Saves the items which are currently stored in the persistent
        /// storage to the specified binary writer.
        /// </summary>
        /// <param name="bw"></param>
        public void Save(BinaryWriter bw)
        {
            bw.Write(this.Count);

            foreach (String key in this.Keys)
            {
                PersistentStorageItem item = this[key];

                bw.Write(key);
                bw.Write((byte)item.ItemType);

                if (item.ItemType == PersistentStorageItemType.String)
                {
                    bw.Write((String)item.Item);
                }
                else if (item.ItemType == PersistentStorageItemType.Number)
                {
                    bw.Write((UInt32)item.Item);
                }
            }
        }
        #endregion

        #region Load
        /// <summary>
        /// Loads persistent storage items from the specified binary reader.
        /// Items that are currently stored are being removed from the
        /// persistent storage.
        /// </summary>
        /// <param name="br"></param>
        public void Load(BinaryReader br)
        {
            this.Clear();

            Int32 elements = br.ReadInt32();

            for (Int32 i = 0; i < elements; i++)
            {
                String key = br.ReadString();
                PersistentStorageItemType type = (PersistentStorageItemType)br.ReadByte();
                PersistentStorageItem item = null;

                if (type == PersistentStorageItemType.String)
                {
                    item = new PersistentStorageItem(br.ReadString());
                }
                else if (type == PersistentStorageItemType.Number)
                {
                    item = new PersistentStorageItem(br.ReadUInt32());
                }

                this.Add(key, item);
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::