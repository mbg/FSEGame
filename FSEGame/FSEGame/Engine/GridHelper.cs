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
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class GridHelper
    {
        private const UInt16 SCALE = 4;
        private const UInt16 CELL_SIZE = 16;

        public static Vector2 GridPositionToAbsolute(Vector2 position)
        {
            return new Vector2(
                position.X * SCALE * CELL_SIZE,
                position.Y * SCALE * CELL_SIZE);
        }

        public static Vector2 AbsolutePositionToGrid(Vector2 position)
        {
            return new Vector2(
                position.X / SCALE / CELL_SIZE,
                position.Y / SCALE / CELL_SIZE);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::