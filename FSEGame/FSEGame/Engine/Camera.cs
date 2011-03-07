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
        /// <summary>
        /// A value indicating whether the camera is attached to the player
        /// controller or not.
        /// </summary>
        private Boolean attachedToPlayer = true;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the camera is attached to the player
        /// controller.
        /// </summary>
        public Boolean IsAttachedToPlayer
        {
            get
            {
                return this.attachedToPlayer;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public Camera()
        {
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
                FSEGame.Singleton.Offset = 
                    new Vector2((vp.Width / 2) - 32, (vp.Height / 2) - 32)
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