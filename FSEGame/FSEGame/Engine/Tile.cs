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
    /// 
    /// </summary>
    public class Tile
    {
        private String name;
        private Boolean passable;
        private Boolean animated = false;
        private Queue<UInt32> frames;
        private float speed;
        private float elapsedTime = 0.0f;

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
                if(this.animated)
                    return this.frames.ToArray()[0];
                else
                return this.frames.ToArray()[0];
            }
        }

        public Queue<UInt32> Frames
        {
            get
            {
                return this.frames;
            }
        }

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

        #region Tile
        public Tile(String name, Boolean passable)
            : this(name, passable, false)
        {
            
        }

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