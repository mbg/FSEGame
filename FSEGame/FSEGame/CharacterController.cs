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
    public delegate void OnChangeLevelDelegate(String id);

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
        private float orientation = 0.0f;
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
        #endregion

        #region Events
        private event OnChangeLevelDelegate onChangeLevel = null;
        #endregion

        #region Constants
        /// <summary>
        /// The time it takes for the character to rotate by 90 degrees.
        /// </summary>
        private const double ROTATE_DURATION = 0.1d;
        /// <summary>
        /// The time it takes for the character to move by one tile.
        /// </summary>
        private const double MOVE_DURATION = 0.3d;

        private const UInt32 SPRITE_RIGHT = 0;
        private const UInt32 SPRITE_LEFT = 6;
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
        #endregion

        #region Event Properties
        public event OnChangeLevelDelegate OnChangeLevel
        {
            add
            {
                this.onChangeLevel += value;
            }
            remove
            {
                this.onChangeLevel -= value;
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
        /// <summary>
        /// Updates the character controller.
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            if (!this.moving)
            {
                KeyboardState ks = Keyboard.GetState();
                Vector2 newPosition = this.cellPosition;

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

                    this.blockDuration = this.block;
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

                    this.blockDuration = this.block;
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

                    this.blockDuration = this.block;
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

                    this.blockDuration = this.block;
                    this.moving = true;
                }

                // :: Verify that the target position exists and is passable.
                CellInformation targetCell = 
                    FSEGame.Singleton.CurrentLevel.GetCellInformation(newPosition);

                if (targetCell != null && targetCell.Tile.Passable)
                {
                    this.targetPosition = newPosition;
                }
            }
            else
            {
                // :: Decrease the remaining block duration by the
                // :: total number of seconds which have elapsed since
                // :: the last frame.
                this.block -= time.ElapsedGameTime.TotalSeconds;

                if (this.block <= 0.0d)
                {
                    this.block = 0.0d;
                    this.moving = false;

                    this.CellPosition = this.targetPosition;

                    // :: Trigger events if applicable.
                    CellInformation targetCell =
                        FSEGame.Singleton.CurrentLevel.GetCellInformation(this.cellPosition);

                    if (targetCell != null && targetCell.Tile.Passable)
                    {
                        switch (targetCell.Cell.EventType)
                        {
                            case CellEventType.ChangeLevel:
                                if (this.onChangeLevel != null)
                                    this.onChangeLevel(targetCell.Cell.EventID);
                                break;
                        }
                    }
                }
                else if (this.cellPosition != this.targetPosition)
                {
                    Vector2 absoluteEndPosition = GridHelper.GridPositionToAbsolute(this.targetPosition);
                    Vector2 difference = absoluteEndPosition - this.absolutePosition;

                    float movement = 213.3f * (float)time.ElapsedGameTime.TotalSeconds;

                    if (difference.Y < 0)
                    {
                        this.absolutePosition.Y -= movement;
                    }
                    else if (difference.Y > 0)
                    {
                        this.absolutePosition.Y += movement;
                    }
                    else if (difference.X < 0)
                    {
                        this.absolutePosition.X -= movement;
                    }
                    else if (difference.X > 0)
                    {
                        this.absolutePosition.X += movement;
                    }
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
            tileset.DrawTile(spriteBatch, this.GetCurrentSpriteID(), this.absolutePosition);
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
            else
                return SPRITE_RIGHT;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::