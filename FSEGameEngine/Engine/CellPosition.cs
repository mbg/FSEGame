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
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class CellPosition
    {
        #region Instance Members
        private Int32 x = 0;
        private Int32 y = 0;
        #endregion

        #region Properties
        public Int32 X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public Int32 Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public CellPosition()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public CellPosition(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }

        public CellPosition(Vector2 position)
        {
            this.x = (Int32)position.X;
            this.y = (Int32)position.Y;
        }
        #endregion

        public override Boolean Equals(Object obj)
        {
            if (obj is CellPosition)
            {
                CellPosition position = (CellPosition)obj;
                return position.x == this.x && position.y == this.y;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public Vector2 ToVector2()
        {
            return new Vector2(this.x, this.y);
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}", this.x, this.y);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::