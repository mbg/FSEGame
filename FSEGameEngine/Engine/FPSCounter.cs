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
#endregion

namespace FSEGame.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class FPSCounter
    {
        #region Instance Members
        private float interval = 1.0f;
        private float lastUpdate = 0.0f;
        private UInt64 frames = 0;
        private float fps = 0.0f;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the update interval in seconds.
        /// </summary>
        public float Interval
        {
            get
            {
                return this.interval;
            }
            set
            {
                this.interval = value;
            }
        }
        /// <summary>
        /// Gets a value representing the current FPS.
        /// </summary>
        public float FPS
        {
            get
            {
                return this.fps;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public FPSCounter()
        {

        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the FPS counter with the elapsed game time.
        /// </summary>
        /// <param name="time">The elapsed game time.</param>
        public void Update(GameTime time)
        {
            // :: This event should get called every frame, so increment the
            // :: frame counter and the time that has elapsed since the last
            // :: update.
            this.frames++;
            this.lastUpdate += (float)time.ElapsedGameTime.TotalSeconds;

            // :: If enough time has passed, update the FPS by dividing
            // :: the number of frames since then by the time that has
            // :: elapsed. Lastly, reset the frame counter and the elapsed
            // :: time.
            if (this.lastUpdate >= this.interval)
            {
                this.fps = this.frames / this.lastUpdate;

                this.frames = 0;
                this.lastUpdate -= this.interval;
            }
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::