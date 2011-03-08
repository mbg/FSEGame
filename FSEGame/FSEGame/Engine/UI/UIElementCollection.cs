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
#endregion

namespace FSEGame.Engine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class UIElementCollection : List<IUIElement>
    {
        #region Instance Members
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        public UIElementCollection()
        {
            
        }
        #endregion

        public new void Add(IUIElement element)
        {
            base.Add(element);
            this.Sort();
        }

        private new void Sort()
        {
            base.Sort(new Comparison<IUIElement>(this.Sort));
        }

        private Int32 Sort(IUIElement a, IUIElement b)
        {
            if (a.Layer == b.Layer)
                return 0;
            else if (a.Layer < b.Layer)
                return -1;
            else
                return 1;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::