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
using FSEGame.Engine;
using Microsoft.Xna.Framework.Input;
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
        private bool moving = false;
        private double block = 0.0f;
        #endregion

        private const double ROTATE_DURATION = 0.1d;
        private const double MOVE_DURATION = 0.3d;

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

        public Boolean Moving
        {
            get
            {
                return this.moving;
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

        #region Update
        public void Update(GameTime time)
        {
            if (!this.moving)
            {
                KeyboardState ks = Keyboard.GetState();
                Vector2 newPosition = this.position;

                if (ks.IsKeyDown(Keys.W))
                {
                    if (this.orientation == 0.0f)
                    {
                        newPosition.Y -= 1;
                        this.block = MOVE_DURATION;
                    }
                    else
                    {
                        this.orientation = 0.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.moving = true;
                }
                else if (ks.IsKeyDown(Keys.S))
                {
                    if (this.orientation == 180.0f)
                    {
                        newPosition.Y += 1;
                        this.block = MOVE_DURATION;
                    }
                    else
                    {
                        this.orientation = 180.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.moving = true;
                }
                else if (ks.IsKeyDown(Keys.D))
                {
                    if (this.orientation == 90.0f)
                    {
                        newPosition.X += 1;
                        this.block = MOVE_DURATION;
                    }
                    else
                    {
                        this.orientation = 90.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.moving = true;
                }
                else if (ks.IsKeyDown(Keys.A))
                {
                    if (this.orientation == 270.0f)
                    {
                        newPosition.X -= 1;
                        this.block = MOVE_DURATION;
                    }
                    else
                    {
                        this.orientation = 270.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.moving = true;
                }

                if (FSEGame.Singleton.CurrentLevel.IsValidPosition(newPosition))
                    this.position = newPosition;
            }
            else
            {
                this.block -= time.ElapsedGameTime.TotalSeconds;

                if (this.block <= 0.0d)
                {
                    this.block = 0.0d;
                    this.moving = false;
                }
            }
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
                this.position.X * 16 * 4, this.position.Y * 16 * 4));
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::