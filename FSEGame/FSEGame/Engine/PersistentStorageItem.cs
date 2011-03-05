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
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PersistentStorageItem
    {
        #region Instance Members
        private PersistentStorageItemType itemType;
        private Object item;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of the persistent storage item.
        /// </summary>
        public PersistentStorageItemType ItemType
        {
            get
            {
                return this.itemType;
            }
        }
        /// <summary>
        /// Gets the persistent storage item. Use the ItemType property
        /// to determine the type of this object.
        /// </summary>
        public Object Item
        {
            get
            {
                return this.item;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public PersistentStorageItem(Object item)
        {
            if (item is String)
                this.itemType = PersistentStorageItemType.String;
            else if (item is UInt32)
                this.itemType = PersistentStorageItemType.Number;
            else
                throw new ArgumentException("item is neither a string nor an unsigned integer.");

            this.item = item;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::