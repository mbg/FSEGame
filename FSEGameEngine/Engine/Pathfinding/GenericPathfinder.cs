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
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Path FindPath(CellPosition origin, CellPosition target)
        {
            GameBase.Singleton.Log.WriteLine("A*", String.Format("Trying to find path from {0} to {1}", origin, target));

            Path result = new Path();
            Double cost = 0;

            this.explored = new List<CellPosition>();

            GenericPathfinderQueue searchQueue = new GenericPathfinderQueue();
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
                    return null;
                }

                // :: Is the cell we found the one we were looking for? If yes, reconstruct
                // :: the path to it and return.
                if (info.Position.Equals(target))
                {
                    return this.ReconstructPath(info);
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

        #region GetAdjacentNodes
        /// <summary>
        /// 
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

            if (this.level.IsValidPosition(left.ToVector2()) && !this.IsAlreadyExplored(left, parent))
                positions.Add(left);
            if (this.level.IsValidPosition(top.ToVector2()) && !this.IsAlreadyExplored(top, parent))
                positions.Add(top);
            if (this.level.IsValidPosition(right.ToVector2()) && !this.IsAlreadyExplored(right, parent))
                positions.Add(right);
            if (this.level.IsValidPosition(bottom.ToVector2()) && !this.IsAlreadyExplored(bottom, parent))
                positions.Add(bottom);

            return positions.ToArray();
        }
        #endregion

        private Boolean IsAlreadyExplored(CellPosition position, GenericPathfinderCellInformation parent)
        {
            // TODO: Optimise this.
            foreach (CellPosition pos in this.explored)
            {
                if (pos.Equals(position))
                    return true;
            }

            return false;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::