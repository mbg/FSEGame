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
        public CharacterController(float orientation, Vector2 position)
        {
            this.orientation = orientation;
            this.position = position;
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
            if (/*keys.Pressed = w && */orientation == 0)
                {
                    tileset.DrawTile(spriteBatch, 0, this.position.X - 1, this.position.Y);
                    //move character UP
                }
                else if (/*keys.Pressed = w*/)
                {
                    Orientation = 0;
                }
                else if(/*keys.Pressed = a &&*/ orientation == 270)
                {
                    //move character left
                }
                else if (/*keys.Pressed = a*/)
                {
                    Orientation = 270;
                }
                else if(/*keys.Pressed = s &&*/ orientation == 180)
                {
                    //move character down
                }
                else if (/*keys.Pressed = s*/)
                {
                    Orientation = 180;
                }
                else if(/*keys.Pressed = d &&*/ orientation == 90)
                {
                    //move character right
                }
                else if (/*keys.Pressed = d*/)
                {
                    Orientation = 90;
                }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::