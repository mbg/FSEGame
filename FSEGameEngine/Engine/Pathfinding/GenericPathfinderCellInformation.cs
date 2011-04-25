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
#endregion

namespace FSEGame.Engine.Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GenericPathfinderCellInformation
    {
        #region Instance Members
        private CellPosition position;
        private GenericPathfinderCellInformation from;
        private Double g;
        private Double h;
        private Double f;
        #endregion

        #region Properties
        public GenericPathfinderCellInformation From
        {
            get
            {
                return this.from;
            }
            set
            {
                this.from = value;
            }
        }
        public CellPosition Position
        {
            get
            {
                return this.position;
            }
        }
        public Double G
        {
            get
            {
                return this.g;
            }
        }
        public Double H
        {
            get
            {
                return this.h;
            }
        }
        public Double F
        {
            get
            {
                return this.f;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GenericPathfinderCellInformation(Double g, CellPosition position, CellPosition target)
        {
            this.position = position;
            this.g = g;
            this.h = this.CalculateManhattenDistance(position, target);
            this.f = this.g + this.h;
        }
        #endregion

        private Double CalculateManhattenDistance(CellPosition a, CellPosition b)
        {
            Double result = Math.Abs(a.X - b.X);
            result += Math.Abs(a.Y - b.Y);

            return result;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::