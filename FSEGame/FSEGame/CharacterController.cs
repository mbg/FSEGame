// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: FSEGame
// :: Copyright 2011 Warren Jackson, Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: ---
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Notes:   Manages a single tileset and allows to render individual tiles.
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FSEGame.Engine;
#endregion

namespace FSEGame
{
    /// <summary>
    /// Represents the player character.
    /// </summary>
    public class CharacterController
    {
        #region Instance Members
        private Vector2 position;
        private float orientation = 0.0f;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the position of the character.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
        /// <summary>
        /// Gets or sets the orientation of the character.
        /// </summary>
        public float Orientation
        {
            get
            {
                return orientation;
            }

            set
            {
                this.orientation = value;                
            }
        }
        #endregion
        
        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public CharacterController()
        {
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the character.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="tileset"></param>
        public void Draw(SpriteBatch spriteBatch, Tileset tileset)
        {
            tileset.DrawTile(spriteBatch, 0, new Vector2(
                this.position.X * 16 * 4, this.position.Y * 16 * 4), this.orientation);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::