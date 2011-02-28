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
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Actor
    {
        #region Instance Members
        private Vector2 cellPosition;
        private Vector2 absolutePosition;
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
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Actor()
        {

        }
        #endregion

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch batch)
        {
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::