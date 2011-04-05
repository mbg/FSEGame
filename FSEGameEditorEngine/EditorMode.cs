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
#endregion

namespace FSEGameEditorEngine
{
    /// <summary>
    /// Enumerates different editor modes.
    /// </summary>
    public enum EditorMode
    {
        /// <summary>
        /// The tile mode in which tiles may be placed in the level or
        /// existing tiles may be edited.
        /// </summary>
        Tiles,
        /// <summary>
        /// The actors mode in which actors may be placed in the level or
        /// existing actors may be edited.
        /// </summary>
        Actors,
        /// <summary>
        /// The entry point mode in which entry points may be placed in the
        /// level or existing entry points may be edited.
        /// </summary>
        EntryPoints
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::