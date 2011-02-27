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
    public class Camera
    {
        #region Instance Members
        private Vector2 offset;
        private Boolean attachedToPlayer = true;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the camera offset.
        /// </summary>
        public Vector2 Offset
        {
            get
            {
                return this.offset;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Camera()
        {
            this.offset = Vector2.Zero;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the camera object.
        /// </summary>
        /// <param name="vp">The main viewport.</param>
        public void Update(Viewport vp)
        {
            // :: Update the camera so that the player character is in the centre
            // :: of the screen if it is attached to the player.
            if (this.attachedToPlayer)
            {
                this.offset = new Vector2((vp.Width / 2) - 32, (vp.Height / 2) - 32)
                    - FSEGame.Singleton.Character.AbsolutePosition;
            }
        }
        #endregion

        #region AttachToPlayer
        /// <summary>
        /// Attaches the camera to the player character.
        /// </summary>
        public void AttachToPlayer()
        {
            this.attachedToPlayer = true;
        }
        #endregion

        #region DetachFromPlayer
        /// <summary>
        /// Detaches the camera from the player character.
        /// </summary>
        public void DetachFromPlayer()
        {
            this.attachedToPlayer = false;
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::