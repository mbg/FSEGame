// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: $projectname$
// :: Copyright 2011 Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 2/22/2011 12:18:27 AM
// ::      by: MBG20102011\Michael Gale
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

        public void Update(Viewport vp)
        {
            this.offset = new Vector2((vp.Width / 2) - 32, (vp.Height / 2) - 32) 
                - this.TileWorldToScreen(FSEGame.Singleton.Character.Position);
        }

        public Vector2 TileWorldToScreen(Vector2 position)
        {
            return new Vector2(position.X * 64, position.Y * 64);
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::