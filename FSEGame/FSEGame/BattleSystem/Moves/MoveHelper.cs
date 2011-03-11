// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: $projectname$
// :: Copyright 2011 Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: Created: 3/11/2011 6:08:53 AM
// ::      by: MBG20102011\Michael Gale
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

#region References
using System;
#endregion

namespace FSEGame.BattleSystem.Moves
{
    /// <summary>
    /// 
    /// </summary>
    public static class MoveHelper
    {
        public static IMove CreateFromName(String name)
        {
            switch (name)
            {
                case "BasicAttack":
                    return new BasicAttack();
                case "BridgeGuard":
                    return new BridgeGuard();
            }

            return null;
        }
    }
}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :: End of File
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::