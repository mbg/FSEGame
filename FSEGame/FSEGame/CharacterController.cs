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
using System.IO;
#endregion

namespace FSEGame
{
    /// <summary>
    /// Represents the player character.
    /// </summary>
    public class CharacterController
    {
        #region Instance Members
        /// <summary>
        /// Stores the absolute position of the character.
        /// </summary>
        private Vector2 absolutePosition;
        /// <summary>
        /// Stores the target cell position of the character.
        /// </summary>
        private Vector2 targetPosition;
        /// <summary>
        /// Stores the current cell position of the character.
        /// </summary>
        private Vector2 cellPosition;
        /// <summary>
        /// Stores the orientation of the character.
        /// </summary>
        private float orientation = 180.0f;
        /// <summary>
        /// Stores a value which indicates whether the character is
        /// currently moving or not.
        /// </summary>
        private bool moving = false;
        /// <summary>
        /// Stores the remaining duration for which the character 
        /// controller doesn't accept any input.
        /// </summary>
        private double block = 0.0d;
        /// <summary>
        /// 
        /// </summary>
        private double blockDuration = 0.0d;
        /// <summary>
        /// Stores whether the character controller is enabled or not.
        /// </summary>
        private Boolean enabled = false;

        private Boolean visible = true;

        private Tileset tileset;
        #endregion

        #region Constants
        /// <summary>
        /// The time it takes for the character to rotate by 90 degrees.
        /// </summary>
        private const double ROTATE_DURATION = 0.1d;

        private const UInt32 SPRITE_RIGHT = 0;
        private const UInt32 SPRITE_LEFT = 1;
        private const UInt32 SPRITE_UP = 2;
        private const UInt32 SPRITE_DOWN = 3;
        private const UInt32 SPRITE_RIGHT_ANIMATED = 4;
        private const UInt32 SPRITE_LEFT_ANIMATED = 5;
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
                this.targetPosition = this.cellPosition;
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
        /// <summary>
        /// Gets a value indicating whether the character is currently moving or not.
        /// </summary>
        public Boolean Moving
        {
            get
            {
                return this.moving;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the character controller is enabled or not.
        /// </summary>
        public Boolean Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }
        /// <summary>
        /// Gets or sets whether the character sprite is visible.
        /// </summary>
        public Boolean Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public CharacterController()
        {
            this.tileset = new Tileset(16, 1, 4);
            this.tileset.Load(GameBase.Singleton.Content, @"Tilesets\Character.xml");
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the character controller.
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            if (!this.moving && this.enabled)
            {
                KeyboardState ks = Keyboard.GetState();
                Vector2 newPosition = this.cellPosition;

                if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
                {
                    if (this.orientation == 0.0f)
                    {
                        newPosition.Y -= 1;
                    }
                    else
                    {
                        this.orientation = 0.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.blockDuration = this.block;
                    this.moving = true;
                }
                else if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
                {
                    if (this.orientation == 180.0f)
                    {
                        newPosition.Y += 1;
                    }
                    else
                    {
                        this.orientation = 180.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.blockDuration = this.block;
                    this.moving = true;
                }
                else if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
                {
                    if (this.orientation == 90.0f)
                    {
                        newPosition.X += 1;
                    }
                    else
                    {
                        this.orientation = 90.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.blockDuration = this.block;
                    this.moving = true;
                }
                else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
                {
                    if (this.orientation == 270.0f)
                    {
                        newPosition.X -= 1;
                    }
                    else
                    {
                        this.orientation = 270.0f;
                        this.block = ROTATE_DURATION;
                    }

                    this.blockDuration = this.block;
                    this.moving = true;
                }

                // :: Verify that the target position exists and is passable.
                if (GameBase.Singleton.CurrentLevel.CanMoveTo(newPosition))
                {
                    this.targetPosition = newPosition;
                }
            }
            else if(this.moving)
            {
                StreamWriter sw = new StreamWriter("Movement.log", true);

                Vector2 absoluteEndPosition = GridHelper.GridPositionToAbsolute(this.targetPosition);

                sw.WriteLine();
                sw.WriteLine("Entering frame: Moving from {0},{1} to {2},{3}",
                    this.absolutePosition.X, this.absolutePosition.Y,
                    absoluteEndPosition.X, absoluteEndPosition.Y);
                sw.WriteLine("Time elapsed: {0}", time.ElapsedGameTime.TotalSeconds);

                if (this.absolutePosition == absoluteEndPosition)
                {
                    this.moving = false;

                    this.CellPosition = this.targetPosition;

                    // :: Trigger events if applicable.
                    CellInformation targetCell =
                        GameBase.Singleton.CurrentLevel.GetCellInformation(this.cellPosition);

                    if (targetCell != null && targetCell.Tile.Passable)
                    {
                        // :: If the target cell has an event associated to it, then EventID
                        // :: will be set to the name of the event. Otherwise it will be null.
                        if (targetCell.Cell.EventID != null)
                        {
                            FSEGame.Singleton.CurrentLevel.TriggerEvent(targetCell.Cell.EventID);
                        }
                    }
                }
                else if (this.absolutePosition != absoluteEndPosition)
                {
                    Vector2 difference = absoluteEndPosition - this.absolutePosition;

                    float movement = 4.0f;

                    if (difference.Y < 0)
                    {
                        if (this.absolutePosition.Y - movement < this.targetPosition.Y)
                        {
                            this.absolutePosition.Y = this.targetPosition.Y;
                        }
                        else
                        {
                            this.absolutePosition.Y -= movement;
                        }
                    }
                    else if (difference.Y > 0)
                    {
                        // remove this if things go weird
                        /*if (this.absolutePosition.Y + movement > this.targetPosition.Y)
                        {
                            this.absolutePosition.Y = this.targetPosition.Y;
                            this.block = 0.0f;
                        }
                        else*/
                            this.absolutePosition.Y += movement;
                    }
                    else if (difference.X < 0)
                    {
                        if (this.absolutePosition.X - movement < this.targetPosition.X)
                        {
                            this.absolutePosition.X = this.targetPosition.X;
                        }
                        else
                        {
                            this.absolutePosition.X -= movement;
                        }
                    }
                    else if (difference.X > 0)
                    {
                        // remove this if things go weird
                        /*if (this.absolutePosition.X + movement > this.targetPosition.X)
                        {
                            this.absolutePosition.X = this.targetPosition.X;
                            this.block = 0.0f;
                        }
                        else*/
                            this.absolutePosition.X += movement;
                    }
                }

                sw.Close();
            }
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the character pawn.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.visible)
                return;

            this.tileset.DrawTile(
                spriteBatch, this.GetCurrentSpriteID(), this.absolutePosition);
        }
        #endregion

        #region GetCurrentSpriteID
        /// <summary>
        /// Gets the ID for the current character sprite.
        /// </summary>
        /// <returns></returns>
        private UInt32 GetCurrentSpriteID()
        {
            if (this.orientation == 270.0f)
                return SPRITE_LEFT;
            else if (this.orientation == 0.0f)
                return SPRITE_UP;
            else if (this.orientation == 180.0f)
                return SPRITE_DOWN;
            else
                return SPRITE_RIGHT;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::