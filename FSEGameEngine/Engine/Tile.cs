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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// Represents a single tile in a tileset.
    /// </summary>
    public class Tile
    {
        #region Instance Members
        /// <summary>
        /// The name of this tile.
        /// </summary>
        private String name;
        /// <summary>
        /// A value indicating whether this tile is passable or not.
        /// </summary>
        private Boolean passable;
        /// <summary>
        /// A value indicating whether this tile is animated or not.
        /// </summary>
        private Boolean animated = false;
        /// <summary>
        /// The frame queue for this tile.
        /// </summary>
        private Queue<UInt32> frames;
        /// <summary>
        /// The animation speed of this tile.
        /// </summary>
        private float speed;
        /// <summary>
        /// The time which has elapsed since the last frame.
        /// </summary>
        private float elapsedTime = 0.0f;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the tile.
        /// </summary>
        public String Name
        {
            get
            {
                return this.name;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this tile is passable.
        /// </summary>
        public Boolean Passable
        {
            get
            {
                return this.passable;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this tile is animated or not.
        /// </summary>
        public Boolean Animated
        {
            get
            {
                return this.animated;
            }
        }
        /// <summary>
        /// Gets the tile ID of the first frame.
        /// </summary>
        public UInt32 ID
        {
            get
            {
                return this.frames.ToArray()[0];
            }
        }
        /// <summary>
        /// Gets the frame queue of this tile.
        /// </summary>
        public Queue<UInt32> Frames
        {
            get
            {
                return this.frames;
            }
        }
        /// <summary>
        /// Gets or sets the animation speed of this tile.
        /// </summary>
        public float Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the tile with the current game time.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (!this.animated)
                return;

            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.elapsedTime >= this.speed)
            {
                this.frames.Enqueue(this.frames.Dequeue());
                this.elapsedTime -= this.speed;
            }
        }
        #endregion

        #region Tile
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        /// <param name="name">The name of the tile.</param>
        /// <param name="passable">A value indicating whether this tile is passable or not.</param>
        public Tile(String name, Boolean passable)
            : this(name, passable, false)
        {
            
        }
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        /// <param name="name">The name of the tile.</param>
        /// <param name="passable">A value indicating whether this tile is passable or not.</param>
        /// <param name="animated">A value indicating whether this tile is animated or not.</param>
        public Tile(String name, Boolean passable, Boolean animated)
        {
            this.name = name;
            this.passable = passable;
            this.animated = animated;

            this.frames = new Queue<UInt32>();
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::