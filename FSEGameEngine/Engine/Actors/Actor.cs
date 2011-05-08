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
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
#endregion

namespace FSEGame.Engine.Actors
{
    /// <summary>
    /// Represents a base class for actors.
    /// </summary>
    public abstract class Actor
    {
        #region Instance Members
        /// <summary>
        /// The cell position of the actor.
        /// </summary>
        private Vector2 cellPosition;
        /// <summary>
        /// The absolute rendering position of the actor.
        /// </summary>
        private Vector2 absolutePosition;
        /// <summary>
        /// The movement speed (in pixels) of the actor.
        /// </summary>
        private float movementSpeed = 1.0f;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the absolute position of the character.
        /// </summary>
        public Vector2 AbsolutePosition
        {
            get
            {
                return this.absolutePosition;
            }
        }
        /// <summary>
        /// Gets or sets the cell position of the character.
        /// </summary>
        public Vector2 CellPosition
        {
            get
            {
                return this.cellPosition;
            }
            set
            {
                this.cellPosition = value;
                this.absolutePosition = GridHelper.GridPositionToAbsolute(value);
            }
        }
        /// <summary>
        /// Gets or sets the movement speed of the actor (in pixels).
        /// </summary>
        public float MovementSpeed
        {
            get
            {
                return this.movementSpeed;
            }
            set
            {
                this.movementSpeed = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the actor is passable or not.
        /// </summary>
        public abstract Boolean Passable
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Actor()
        {

        }
        #endregion

        #region Update
        /// <summary>
        /// If overriden, this method updates the actor with the current 
        /// game time.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }
        #endregion

        #region Draw
        /// <summary>
        /// If overriden, this method draws the actor in the specified 
        /// sprite batch.
        /// </summary>
        /// <param name="batch"></param>
        public virtual void Draw(SpriteBatch batch)
        {
        }
        #endregion

        #region MoveTo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public virtual void MoveTo(CellPosition target)
        {
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::