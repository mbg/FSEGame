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
using FSEGame.Engine.UI;
#endregion

namespace FSEGame
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadScreen : MenuScreen
    {
        #region Instance Members
        private UIButtonList buttonList;
        private UIButton slot1;
        private UIButton slot2;
        private UIButton slot3;
        private UIButton slot4;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public LoadScreen()
            : base("MainMenu")
        {
            this.buttonList = new UIButtonList();
            this.buttonList.ScrollThrough = true;

            this.Children.Add(this.buttonList);
        }
        #endregion
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::