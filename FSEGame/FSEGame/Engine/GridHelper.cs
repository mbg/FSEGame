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
    /// Provides helper functions to translate grid coordinates into
    /// absolute coordinates.
    /// </summary>
    public static class GridHelper
    {
        #region Constants
        /// <summary>
        /// The scale using which textures are drawn.
        /// </summary>
        private const UInt16 SCALE = 4;
        /// <summary>
        /// The width and height of each tile.
        /// </summary>
        private const UInt16 CELL_SIZE = 16;
        #endregion

        #region GridPositionToAbsolute
        /// <summary>
        /// Converts a grid position to an absolute position.
        /// </summary>
        /// <param name="position">The grid position to convert.</param>
        /// <returns>
        /// Returns the absolute representation of the grid position.
        /// </returns>
        public static Vector2 GridPositionToAbsolute(Vector2 position)
        {
            return new Vector2(
                position.X * SCALE * CELL_SIZE,
                position.Y * SCALE * CELL_SIZE);
        }
        #endregion

        #region AbsolutePositionToGrid
        /// <summary>
        /// Converts an absolute position to a grid position.
        /// </summary>
        /// <param name="position">The absolute position to convert.</param>
        /// <returns>
        /// Returns the grid representation of the absolute position.
        /// </returns>
        public static Vector2 AbsolutePositionToGrid(Vector2 position)
        {
            return new Vector2(
                position.X / SCALE / CELL_SIZE,
                position.Y / SCALE / CELL_SIZE);
        }
        #endregion

        public static Double Distance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::