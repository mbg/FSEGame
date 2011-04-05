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
using System.ComponentModel;
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CellProperties
    {
        #region Instance Members
        private LevelCell cell;
        #endregion

        #region Properties
        [Category("Cell")]
        [DisplayName("Event")]
        [Description("The ID of the event that should be associated with this cell.")]
        public String Event
        {
            get
            {
                return this.cell.EventID;
            }
            set
            {
                this.cell.EventID = value;
            }
        }
        [Category("Tile")]
        [DisplayName("Passable")]
        [Description("Indicates whether the tile used by this cell is passable.")]
        public Boolean Passable
        {
            get
            {
                return this.cell.Tile.Passable;
            }
        }
        [Category("Tile")]
        [DisplayName("Animated")]
        [Description("Indicates whether the tile used by this cell is animated.")]
        public Boolean Animated
        {
            get
            {
                return this.cell.Tile.Animated;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public CellProperties(LevelCell cell)
        {
            this.cell = cell;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::