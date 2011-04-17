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
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class UIHealthBar : UIProgressBar
    {
        #region Instance Members
        private Texture2D yellowTexture;
        private Texture2D redTexture;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIHealthBar(String backgroundResource, String greenResource, 
            String yellowResource, String redResource)
            : base(backgroundResource, greenResource)
        {
            this.yellowTexture = GameBase.Singleton.Content.Load<Texture2D>(yellowResource);
            this.redTexture = GameBase.Singleton.Content.Load<Texture2D>(redResource);
        }
        #endregion

        protected override Texture2D GetForegroundTexture()
        {
            Double currentPercentage = (Double)this.Value / (Double)this.Maximum;

            if (currentPercentage < 0.25d)
                return this.redTexture;
            else if (currentPercentage < 0.75d)
                return this.yellowTexture;
            else
                return base.GetForegroundTexture();
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::