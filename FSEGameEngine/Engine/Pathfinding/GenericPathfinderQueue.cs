// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using System.Collections.Generic;
#endregion

namespace FSEGame.Engine.Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GenericPathfinderQueue
    {
        #region Instance Members
        private List<GenericPathfinderCellInformation> items;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GenericPathfinderQueue()
        {
            this.items = new List<GenericPathfinderCellInformation>();
        }
        #endregion

        public void Enqueue(GenericPathfinderCellInformation item)
        {
            for (Int32 i = 0; i < this.items.Count; i++)
            {
                if (item.F < this.items[i].F)
                {
                    this.items.Insert(i, item);
                    return;
                }
            }

            this.items.Add(item);
        }

        public GenericPathfinderCellInformation Dequeue()
        {
            if (this.items.Count == 0)
                return null;

            GenericPathfinderCellInformation result = this.items[0];
            this.items.RemoveAt(0);

            return result;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::