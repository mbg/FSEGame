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
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame.Engine.Pathfinding
{
    /// <summary>
    /// Implements the A* search algorithm.
    /// </summary>
    public class GenericPathfinder : IPathfinder
    {
        #region Instance Members
        private Level level;
        private List<CellPosition> explored;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public GenericPathfinder(Level level)
        {
            this.level = level;
        }
        #endregion

        #region FindPath
        /// <summary>
        /// Finds a path from the origin position to the target position or returns
        /// null if there is no path connecting the two points.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Path FindPath(CellPosition origin, CellPosition target)
        {
#if DEBUG
            GameBase.Singleton.Log.WriteLine("A*", String.Format("Trying to find path from {0} to {1}", origin, target));

            DateTime startTime = DateTime.Now;
#endif
           
            Double cost = 0;

            this.explored = new List<CellPosition>();

            PriorityQueue<GenericPathfinderCellInformation> searchQueue = new PriorityQueue<GenericPathfinderCellInformation>(new ComparisonDelegate<GenericPathfinderCellInformation>(this.Compare));
            searchQueue.Enqueue(new GenericPathfinderCellInformation(cost, origin, target));

            while (true)
            {
                // :: Get the next cell to look at.
                GenericPathfinderCellInformation info = searchQueue.Dequeue();

#if DEBUG
                //GameBase.Singleton.Log.WriteLine("A*", String.Format("Expanding node at position {0} with cost {1}", info.Position, info.F));
#endif

                // :: If there are no more cells to look at, then there is no path
                // :: to the specified target cell.
                if (info == null)
                {
#if DEBUG
                    GameBase.Singleton.Log.WriteLine("A*", String.Format("Found no path from {0} to {1} in {2}.", origin, target, DateTime.Now.Subtract(startTime).ToString()));
#endif
                    return null;
                }

                // :: Is the cell we found the one we were looking for? If yes, reconstruct
                // :: the path to it and return.
                if (info.Position.Equals(target))
                {
                    Path resultPath = this.ReconstructPath(info);
#if DEBUG
                    GameBase.Singleton.Log.WriteLine("A*", String.Format("Found a path from {0} to {1} with cost {2} and {3} steps in {4}.",
                        origin, target, info.F, resultPath.Count, DateTime.Now.Subtract(startTime).ToString()));
#endif
                    return resultPath;
                }

                this.explored.Add(info.Position);
                cost += 1.0d;

                foreach (CellPosition position in this.GetAdjacentNodes(info.Position, info))
                {
                    GenericPathfinderCellInformation childInfo = 
                        new GenericPathfinderCellInformation(cost, position, target);
                    childInfo.From = info;

                    searchQueue.Enqueue(childInfo);
                }
            }
        }
        #endregion

        #region ReconstructPath
        /// <summary>
        /// Reconstructs the path to the goal state after it was discovered.
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        private Path ReconstructPath(GenericPathfinderCellInformation to)
        {
            Path result = new Path();
            GenericPathfinderCellInformation current = to;

            while (current.From != null)
            {
                result.Enqueue(current.Position);
                current = current.From;
            }
            

            return result;
        }
        #endregion

        #region GetAdjacentNodes
        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private CellPosition[] GetAdjacentNodes(CellPosition position, GenericPathfinderCellInformation parent)
        {
            List<CellPosition> positions = new List<CellPosition>();

            CellPosition left = new CellPosition(position.X - 1, position.Y);
            CellPosition top = new CellPosition(position.X, position.Y - 1);
            CellPosition right = new CellPosition(position.X + 1, position.Y);
            CellPosition bottom = new CellPosition(position.X, position.Y + 1);

            if (this.level.IsValidPosition(left.ToVector2()) && !this.IsAlreadyExplored(left))
                positions.Add(left);
            if (this.level.IsValidPosition(top.ToVector2()) && !this.IsAlreadyExplored(top))
                positions.Add(top);
            if (this.level.IsValidPosition(right.ToVector2()) && !this.IsAlreadyExplored(right))
                positions.Add(right);
            if (this.level.IsValidPosition(bottom.ToVector2()) && !this.IsAlreadyExplored(bottom))
                positions.Add(bottom);

            return positions.ToArray();
        }
        #endregion

        #region IsAlreadyExplored
        /// <summary>
        /// Gets a value indicating whether the specified position has already been explored. 
        /// </summary>
        /// <param name="position">
        /// The position for which to determine whether it has already
        /// been explored or not.</param>
        /// <returns>
        /// Returns true if the position has already been explored or false if not.
        /// </returns>
        private Boolean IsAlreadyExplored(CellPosition position)
        {
            // TODO: Optimise this.
            foreach (CellPosition pos in this.explored)
            {
                if (pos.Equals(position))
                    return true;
            }

            return false;
        }
        #endregion

        #region Compare
        /// <summary>
        /// Compares two cells and decides which should be explored first.
        /// </summary>
        /// <param name="a">The first cell to compare.</param>
        /// <param name="b">The second cell to compare.</param>
        /// <returns>
        /// Returns true if the first cell has priority over the second cell or 
        /// false if not.
        /// </returns>
        private Boolean Compare
            (GenericPathfinderCellInformation a, GenericPathfinderCellInformation b)
        {
            return a.F < b.F;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::