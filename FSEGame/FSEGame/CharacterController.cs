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
        // :: [TODO]: Implement appropriate properties for the instance members.
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
            // :: [TODO]: Render the character at the correct position/rotation
            // ::         using the tileset instance. 
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::